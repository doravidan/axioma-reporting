using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// Base class for all Playwright E2E tests. Each test instance gets its own
/// browser context (isolated cookies/sessions). The shared WebServerFixture
/// keeps the web application running for the entire Playwright collection.
/// </summary>
public abstract class PlaywrightTestBase : IAsyncLifetime
{
    // HTTP (not HTTPS) avoids self-signed certificate issues in CI/dev environments.
    protected string BaseUrl => WebServerFixture.BaseUrl;
    protected const string AdminId = "admin";
    protected const string AdminPassword = "admin1234";

    private IPlaywright _playwright = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;

    public virtual async Task InitializeAsync()
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = BaseUrl,
            IgnoreHTTPSErrors = true
        });
        Page = await Context.NewPageAsync();
    }

    public virtual async Task DisposeAsync()
    {
        await Context.CloseAsync();
        await Browser.CloseAsync();
        _playwright.Dispose();
    }

    /// <summary>
    /// Logs in and handles any post-login redirects (Terms of Use, mandatory
    /// password change). Returns when the user has landed on the home page.
    /// </summary>
    protected async Task LoginAsync(string idNumber = AdminId, string password = AdminPassword)
    {
        await Page.GotoAsync("/Account/Login");
        await Page.Locator("input[name='IdNumber']").FillAsync(idNumber);
        await Page.Locator("input[name='Password']").FillAsync(password);
        await Page.Locator("button[type='submit']").ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Handle TFA code page if TFA is enabled in this environment
        if (Page.Url.Contains("/Account/TwoFactor", StringComparison.OrdinalIgnoreCase))
            return;

        // Handle Terms of Use acceptance
        if (Page.Url.Contains("/Account/TermsOfUse", StringComparison.OrdinalIgnoreCase))
        {
            var acceptCheckbox = Page.Locator("#acceptCheckbox");
            if (await acceptCheckbox.IsVisibleAsync())
                await acceptCheckbox.CheckAsync();

            var acceptBtn = Page.Locator("button[type='submit'], input[type='submit']").First;
            if (await acceptBtn.IsVisibleAsync())
            {
                await acceptBtn.ClickAsync();
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            }
        }

        // Handle mandatory password change
        if (Page.Url.Contains("/Account/ChangePassword", StringComparison.OrdinalIgnoreCase))
        {
            var newPw = "AdminTest123!";
            await Page.Locator("input[name='CurrentPassword']").FillAsync(password);
            await Page.Locator("input[name='NewPassword']").FillAsync(newPw);
            await Page.Locator("input[name='ConfirmPassword']").FillAsync(newPw);
            await Page.Locator("main button[type='submit']").ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // Do not let callers inspect a transient post-login/interstitial DOM.
        if (await Page.Locator("form[action*='Logout']").CountAsync() == 0)
        {
            await Page.GotoAsync("/");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        if (await Page.Locator("form[action*='Logout']").CountAsync() == 0)
            throw new Xunit.Sdk.XunitException($"Login did not reach an authenticated page (url: {Page.Url}).");
    }

    protected async Task<string> GetPageTextAsync() =>
        await Page.Locator("body").InnerTextAsync();
}
