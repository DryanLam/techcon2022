using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCon.Tests.Data;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts._03
{
    public class UploadFixture : PageTest
    {
        [Test]
        public async Task Upload_ByPath()
        {
            await Page.GotoAsync(Params.PortalUrl);

            // 2. Click 'Admin' on left side panel to go to 'Admin/User Management' page
            await Page.Locator(".oxd-main-menu-item:has-text('Admin')").ClickAsync();

            // 3. Search Username and then click 'Search' button
            await Page.Locator(".oxd-input-group:has-text('Username') input").FillAsync("Admin");
            await Page.Locator("button:has-text('Search')").ClickAsync();

            // VP: Verify that the result return '(1) Record Found'
            await Expect(Page.Locator(".orangehrm-horizontal-padding")).ToContainTextAsync("(1) Record Found");

        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ViewportSize = new()
                {
                    Width = 1920,
                    Height = 1080
                },
                //BaseURL = Params.PortalUrl,
                StorageStatePath = $"{IOHelpers.ProjectPath}/Settings/login.json"
            };
        }
    }
}
