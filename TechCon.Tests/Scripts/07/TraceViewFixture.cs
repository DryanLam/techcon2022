using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using TechCon.Tests.Data;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts
{
    public class TraceViewFixture : PageTest
    {
        [SetUp]
        public async Task SetUp_TracingAsync()
        {
            await Context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown]
        public async Task BaseTearDownAsync() 
        {
            var traceName = TestContext.CurrentContext.Test.Name;
            string filePath = $"{IOHelpers.ProjectPath}/TraceView/{traceName}.zip";
            await Context.Tracing.StopAsync(new TracingStopOptions()
            {
                Path = filePath
            });
        }


        [Test]
        public async Task TraceViewTest()
        {
            // 1. Navigate to the portal
            await Page.GotoAsync("/");

            // 2. Login with valid credentials
            await Page.Locator("input[name='username']").FillAsync(Params.Username);
            await Page.Locator("input[name='password']").FillAsync(Params.Password);
            await Page.Locator("button:has-text('Login')").ClickAsync();

            // 3. Navigate to 'View Candidates' page
            await Page.GotoAsync("/web/index.php/recruitment/viewCandidates");

            // 4. Select 'QA Lead' under Job Title
            await Page.Locator(".oxd-input-group:has-text('Job Title') .oxd-select-text-input").ClickAsync();
            await Page.Locator(".oxd-select-option:has-text('QA Lead')").ClickAsync();

            // 5. Select 'Rejected' under Status
            await Page.Locator(".oxd-input-group:has-text('Status') .oxd-select-text-input").ClickAsync();
            await Page.Locator(".oxd-select-option:has-text('Rejected')").ClickAsync();

            // 6. Click 'Search' button
            await Page.Locator("button:has-text('Search')").ClickAsync();

            // 7. Click 'Download' button to save as "Techcon.pdf"
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
                BaseURL = Params.PortalUrl
            };
        }






        // ./playwright.ps1 show-trace trace.zip
    }
}
