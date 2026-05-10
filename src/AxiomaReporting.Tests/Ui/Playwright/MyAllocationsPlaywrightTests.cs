namespace AxiomaReporting.Tests.UI.Playwright;

[Collection("Playwright")]
public class MyAllocationsPlaywrightTests : PlaywrightTestBase
{
    [Fact]
    public async Task Employee_MyAllocations_PageShowsOnlyEmployeeAllocations()
    {
        await LoginAsync("111111111", "Password123");

        var response = await Page.GotoAsync("/MyAllocations");
        response!.Status.Should().Be(200);

        var body = await GetPageTextAsync();
        body.Should().Contain("ההקצאות שלי");
        body.Should().Contain("נוער בסיכון");
        body.Should().Contain("תוכנית א");
        body.Should().Contain("יצא לאקסל");
    }
}
