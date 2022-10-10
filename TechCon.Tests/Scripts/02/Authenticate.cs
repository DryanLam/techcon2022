using Microsoft.Playwright;
using NUnit.Framework;
using TechCon.Tests.Data;

namespace TechCon.Tests.Scripts
{
    public class TwoFixture
    {
        [Test]
        public async Task Login_BasicAuth()
        {
            // CONFIGURATION
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = false,
                SlowMo = 2000
            });

            var context = await browser.NewContextAsync(new()
            {
                ViewportSize = new ViewportSize()
                {
                    Width = 1920,
                    Height = 1080
                },
                HttpCredentials = new HttpCredentials()
                {
                    Username = "admin",
                    Password = "admin"
                }
            });

            var page = await context.NewPageAsync();
            page.SetDefaultNavigationTimeout(Params.ActionTimeout);
            page.SetDefaultTimeout(Params.ActionTimeout);

            // STEPS
            // 1. Navigate to the portal by basic authentication configuration
            await page.GotoAsync("https://the-internet.herokuapp.com/basic_auth");

            // VP: Verify that the result return 'Congratulations! You must have the proper credentials.'
            await Assertions.Expect(page.Locator("#content")).ToContainTextAsync("Congratulations! You must have the proper credentials.");
        }
    }
}
