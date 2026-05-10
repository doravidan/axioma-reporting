using Microsoft.Playwright;

namespace AxiomaReporting.Tests.UI.Playwright;

/// <summary>
/// E2E tests for the login page: RTL layout, Hebrew content, form elements,
/// accessibility attributes, and login flow behaviour.
/// </summary>
[Collection("Playwright")]
public class LoginPagePlaywrightTests : PlaywrightTestBase
{
    // ─── Page Rendering ──────────────────────────────────────────────────────

    [Fact]
    public async Task LoginPage_HasHebrewTitle()
    {
        await Page.GotoAsync("/Account/Login");
        var title = await Page.TitleAsync();
        title.Should().Contain("סייט&סאונד חינוך");
        title.Should().Contain("כניסה למערכת");
    }

    [Fact]
    public async Task LoginPage_HtmlElement_IsRtl()
    {
        await Page.GotoAsync("/Account/Login");
        var dir = await Page.Locator("html").GetAttributeAsync("dir");
        dir.Should().Be("rtl");
    }

    [Fact]
    public async Task LoginPage_HtmlElement_LangIsHe()
    {
        await Page.GotoAsync("/Account/Login");
        var lang = await Page.Locator("html").GetAttributeAsync("lang");
        lang.Should().Be("he");
    }

    [Fact]
    public async Task LoginPage_HasIdNumberField_WithCorrectAutocomplete()
    {
        await Page.GotoAsync("/Account/Login");
        var input = Page.Locator("input[name='IdNumber']");
        (await input.IsVisibleAsync()).Should().BeTrue();
        (await input.GetAttributeAsync("autocomplete")).Should().Be("username");
    }

    [Fact]
    public async Task LoginPage_HasPasswordField_WithCorrectType()
    {
        await Page.GotoAsync("/Account/Login");
        var input = Page.Locator("input[name='Password']");
        (await input.IsVisibleAsync()).Should().BeTrue();
        (await input.GetAttributeAsync("type")).Should().Be("password");
        (await input.GetAttributeAsync("autocomplete")).Should().Be("current-password");
    }

    [Fact]
    public async Task LoginPage_HasRememberMeCheckbox()
    {
        await Page.GotoAsync("/Account/Login");
        // ASP.NET MVC renders a hidden + visible checkbox; target the checkbox explicitly
        var checkbox = Page.Locator("input[type='checkbox'][name='RememberMe']");
        (await checkbox.IsVisibleAsync()).Should().BeTrue();
    }

    [Fact]
    public async Task LoginPage_HasForgotPasswordLink()
    {
        await Page.GotoAsync("/Account/Login");
        var link = Page.Locator("a[href*='ForgotPassword']");
        (await link.IsVisibleAsync()).Should().BeTrue();
    }

    [Fact]
    public async Task LoginPage_HasAntiForgeryToken()
    {
        await Page.GotoAsync("/Account/Login");
        var token = Page.Locator("input[name='__RequestVerificationToken']");
        (await token.CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task LoginPage_HasAriaRequiredOnRequiredInputs()
    {
        await Page.GotoAsync("/Account/Login");
        var idInput = Page.Locator("input[name='IdNumber']");
        var pwInput = Page.Locator("input[name='Password']");
        (await idInput.GetAttributeAsync("aria-required")).Should().Be("true");
        (await pwInput.GetAttributeAsync("aria-required")).Should().Be("true");
    }

    [Fact]
    public async Task LoginPage_HasLogoImage()
    {
        await Page.GotoAsync("/Account/Login");
        var img = Page.Locator("img").First;
        (await img.IsVisibleAsync()).Should().BeTrue();
    }

    // ─── Login Flow ───────────────────────────────────────────────────────────

    [Fact]
    public async Task Login_WithWrongPassword_ShowsHebrewError()
    {
        await Page.GotoAsync("/Account/Login");
        // Use a non-existent ID to avoid locking real accounts across test runs
        await Page.Locator("input[name='IdNumber']").FillAsync("nonexistent99999");
        await Page.Locator("input[name='Password']").FillAsync("WrongPassword1!");
        await Page.Locator("button[type='submit']").ClickAsync();

        // Stay on login page with an error
        Page.Url.Should().Contain("/Account/Login");
        var body = await GetPageTextAsync();
        // Error message should contain Hebrew characters
        body.Should().MatchRegex("[\u0590-\u05ff]");
    }

    [Fact]
    public async Task Login_WithValidCredentials_LeavesLoginPage()
    {
        await LoginAsync();
        Page.Url.Should().NotContain("/Account/Login");
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShowsLogoutButton()
    {
        await LoginAsync();
        // The layout renders a logout form
        var logoutForm = Page.Locator("form[action*='Logout']");
        (await logoutForm.CountAsync()).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task AnonymousAccess_ToProtectedRoute_RedirectsToLogin()
    {
        await Page.GotoAsync("/Employee/Index");
        Page.Url.Should().Contain("/Account/Login");
    }

    [Fact]
    public async Task AnonymousAccess_ToDashboard_RedirectsToLogin()
    {
        await Page.GotoAsync("/Dashboard/Index");
        Page.Url.Should().Contain("/Account/Login");
    }

    // ─── Forgot Password Page ─────────────────────────────────────────────────

    [Fact]
    public async Task ForgotPasswordPage_RendersIdNumberField()
    {
        await Page.GotoAsync("/Account/ForgotPassword");
        // ForgotPassword asks for ID number (not email) — field name is "idNumber"
        var idInput = Page.Locator("input[name='idNumber']");
        (await idInput.IsVisibleAsync()).Should().BeTrue();
    }

    [Fact]
    public async Task ForgotPasswordPage_HasSubmitButton()
    {
        await Page.GotoAsync("/Account/ForgotPassword");
        var btn = Page.Locator("button[type='submit']");
        (await btn.IsVisibleAsync()).Should().BeTrue();
    }
}
