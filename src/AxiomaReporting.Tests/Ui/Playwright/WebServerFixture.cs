using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// xUnit collection fixture that starts the AxiomaReporting.Web application
/// in a child process on a fixed port and keeps it running for the duration
/// of all Playwright tests. The process is killed when the fixture is disposed.
///
/// Usage: add [Collection("PlaywrightSuite")] to every Playwright test class
/// and declare the fixture in PlaywrightCollectionDefinition.
/// </summary>
public sealed class WebServerFixture : IAsyncLifetime
{
    public const string BaseUrl = "http://localhost:5021";
    public static IReadOnlyDictionary<string, string> ClientWorkbookFiles { get; private set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    private Process? _process;

    public async Task InitializeAsync()
    {
        // Kill any stale process on the port before starting
        await KillProcessOnPortAsync(5021);

        var solutionRoot = FindSolutionRoot();
        var webProject = Path.Combine(solutionRoot, "src", "AxiomaReporting.Web", "AxiomaReporting.Web.csproj");
        ClientWorkbookFiles = DiscoverClientWorkbookFiles(solutionRoot);

        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{webProject}\" --no-build --urls \"{BaseUrl}\"",
            WorkingDirectory = solutionRoot,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        psi.Environment["AXIOMA_DEMO_INMEMORY"] = "true";
        psi.Environment["ASPNETCORE_ENVIRONMENT"] = "Development";
        psi.Environment["Security__LoginIpLimit"] = "5000";
        psi.Environment["Security__LoginUserLimit"] = "100";
        if (ClientWorkbookFiles.Count > 0)
            psi.Environment["AXIOMA_TEST_WORKBOOK_FILES"] =
                string.Join(Path.PathSeparator, ClientWorkbookFiles.Values);

        _process = Process.Start(psi)
            ?? throw new InvalidOperationException("Failed to start web server process.");

        // Both streams are redirected so the child process does not pollute the
        // test runner output. They still have to be drained continuously: after
        // a long Playwright suite an unread OS pipe buffer can fill and suspend
        // the web server, making an otherwise healthy late-running test time out.
        _process.OutputDataReceived += static (_, _) => { };
        _process.ErrorDataReceived += static (_, _) => { };
        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();

        // Wait until the server accepts connections (up to 30 seconds)
        var client = new HttpClient();
        var deadline = DateTime.UtcNow.AddSeconds(30);
        while (DateTime.UtcNow < deadline)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                var resp = await client.GetAsync(BaseUrl + "/Account/Login", cts.Token);
                if (resp.StatusCode != HttpStatusCode.ServiceUnavailable)
                    return; // server is up
            }
            catch { /* not ready yet */ }
            await Task.Delay(500);
        }

        throw new TimeoutException($"Web server did not start within 30 seconds at {BaseUrl}");
    }

    public Task DisposeAsync()
    {
        try
        {
            if (_process != null && !_process.HasExited)
            {
                _process.Kill(entireProcessTree: true);
                _process.WaitForExit(5000);
            }
        }
        catch { /* ignore cleanup errors */ }
        return Task.CompletedTask;
    }

    private static string FindSolutionRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (dir.GetFiles("*.sln").Length > 0)
                return dir.FullName;
            dir = dir.Parent;
        }
        // Fallback to hard-coded path if solution file not found
        return @"F:\דווח עובדים אקסיומא";
    }

    private static IReadOnlyDictionary<string, string> DiscoverClientWorkbookFiles(string solutionRoot)
    {
        var roots = new List<string>();
        var configuredRoots = Environment.GetEnvironmentVariable("AXIOMA_TEST_ATTACHMENT_ROOTS");
        if (!string.IsNullOrWhiteSpace(configuredRoots))
        {
            roots.AddRange(configuredRoots.Split(
                Path.PathSeparator,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (!string.IsNullOrWhiteSpace(userProfile))
            roots.Add(Path.Combine(userProfile, "Downloads"));
        roots.Add(Path.Combine(solutionRoot, "attachments"));
        roots.Add(Path.Combine(solutionRoot, "src", "AxiomaReporting.Tests", "TestData"));

        var candidates = new List<string>();
        foreach (var root in roots.Where(Directory.Exists).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            try
            {
                candidates.AddRange(Directory.EnumerateFiles(root, "*.xlsx", SearchOption.AllDirectories));
            }
            catch (UnauthorizedAccessException)
            {
                // Continue with the remaining configured attachment roots.
            }
        }

        string? Find(params string[] fragments) => candidates.FirstOrDefault(path =>
            fragments.All(fragment => Path.GetFileName(path)
                .Contains(fragment, StringComparison.OrdinalIgnoreCase)));

        var fixtures = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var argwan = Find("ארגואן", "דוח אשי");
        var yosef = Find("יוסף", "כיתות שחר", "אפריל 2026");
        if (argwan != null) fixtures["תוכנית שמיים"] = argwan;
        if (yosef != null) fixtures["כיתות שח\"ר"] = yosef;
        return fixtures;
    }

    private static async Task KillProcessOnPortAsync(int port)
    {
        try
        {
            // Windows: use netstat + taskkill
            var psi = new ProcessStartInfo("cmd", $"/c FOR /F \"tokens=5\" %P IN ('netstat -ano ^| findstr :{port} ^| findstr LISTENING') DO @taskkill /F /PID %P")
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var p = Process.Start(psi);
            if (p != null) await p.WaitForExitAsync();
        }
        catch { /* ignore */ }

        // Wait briefly for port to be released
        await Task.Delay(1000);
    }
}

/// <summary>
/// xUnit collection definition that wires up WebServerFixture as a shared
/// class-level fixture for all Playwright tests. DisableParallelization keeps
/// Playwright tests sequential so the single server isn't overwhelmed.
/// </summary>
[CollectionDefinition("Playwright", DisableParallelization = true)]
public class PlaywrightCollection : ICollectionFixture<WebServerFixture> { }
