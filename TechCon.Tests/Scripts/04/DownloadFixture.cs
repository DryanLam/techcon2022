using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using TechCon.Tests.Data;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts._04
{
    public class DownloadFixture : PageTest
    {
        [Test]
        public async Task Download_ByPath()
        {
            // 1. Navigate to 'Add Employee' page
            await Page.GotoAsync("/web/index.php/recruitment/viewCandidates");

            // 2. Select 'QA Lead' under Job Title
            await Page.Locator(".oxd-input-group:has-text('Job Title') .oxd-select-text-input").ClickAsync();
            await Page.Locator(".oxd-select-option:has-text('QA Lead')").ClickAsync();

            // 3. Select 'Rejected' under Status
            await Page.Locator(".oxd-input-group:has-text('Status') .oxd-select-text-input").ClickAsync();
            await Page.Locator(".oxd-select-option:has-text('Rejected')").ClickAsync();

            // 4. Click 'Search' button
            await Page.Locator("button:has-text('Search')").ClickAsync();

            // 5. Click 'Download' button to save as "Techcon.pdf"
            var fileName = "Techcon.pdf";
            var downloadFile = Path.Combine(IOHelpers.DownloadPath, fileName);

            var exportFile = await Page.RunAndWaitForDownloadAsync(async () =>
            {
                await Page.Locator(".bi-download").ClickAsync();

            });

            await exportFile.SaveAsAsync(downloadFile);

            // VP: Verify that "Techcon.pdf" file is downloaded successfully
            FileAssert.Exists(downloadFile, "File did NOT download successfully!");

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
                BaseURL = Params.PortalUrl,
                StorageStatePath = $"{IOHelpers.ProjectPath}/Settings/login.json"
            };
        }
    }
}
