using Microsoft.Playwright;
using NUnit.Framework;
using TechCon.Tests.Data;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts
{
    public class LoginFixture
    {
        [Test]
        public async Task Login_StoreLoginState()
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
                }
            });

            var page = await context.NewPageAsync();
            page.SetDefaultNavigationTimeout(Params.ActionTimeout);
            page.SetDefaultTimeout(Params.ActionTimeout);

            // STEPS
            // 1. Navigate to the portal
            await page.GotoAsync(Params.PortalUrl);

            // 2. Login with valid credentials
            await page.Locator("input[name='username']").FillAsync(Params.Username);
            await page.Locator("input[name='password']").FillAsync(Params.Password);
            await page.Locator("button:has-text('Login')").ClickAsync();

            await context.StorageStateAsync(new()
            {
                Path = $"{IOHelpers.ProjectPath}/Settings/login.json"
            });
        }


        [Test]
        public async Task Login_ByStoredState()
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
                StorageStatePath = $"{IOHelpers.ProjectPath}/Settings/login.json"
            });

            var page = await context.NewPageAsync();
            page.SetDefaultNavigationTimeout(Params.ActionTimeout);
            page.SetDefaultTimeout(Params.ActionTimeout);

            // STEPS
            // 1. Navigate to the portal
            await page.GotoAsync(Params.PortalUrl);

            // 2. Click 'Admin' on left side panel to go to 'Admin/User Management' page
            await page.Locator(".oxd-main-menu-item:has-text('Admin')").ClickAsync();

            // 3. Search Username and then click 'Search' button
            await page.Locator(".oxd-input-group:has-text('Username') input").FillAsync("Admin");
            await page.Locator("button:has-text('Search')").ClickAsync();

            // VP: Verify that the result return '(1) Record Found'
            await Assertions.Expect(page.Locator(".orangehrm-horizontal-padding")).ToContainTextAsync("(1) Record Found");
        }
    }
}
