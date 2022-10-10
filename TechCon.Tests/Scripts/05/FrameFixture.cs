using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts
{
    public class FrameFixture : PageTest
    {
        [Test]
        public async Task Frame_IFrame()
        {
            // 1. Navigate to 'Add Employee' page
            await Page.GotoAsync("/iframe");

            // 2. Locate an iframe
            var val = Page.FrameLocator("iframe[title='Rich Text Area']").Locator("#tinymce");

            // VP: Verify that the iframe is containing 'Your content goes here.' value
            await Expect(val).ToContainTextAsync("Your content goes here.");
        }

        [Test]
        public async Task Frame_NestedFrame()
        {
            // 1. Navigate to 'Add Employee' page
            await Page.GotoAsync("/nested_frames");

            // 2. Locate a frame in the middle
            var val = Page.FrameLocator("frame[name='frame-top']").FrameLocator("frame[name='frame-middle']").Locator("#content");

            // VP: Verify that the middle frame is containing 'MIDDLE' value
            await Expect(val).ToContainTextAsync("MIDDLE");
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
                BaseURL = "https://the-internet.herokuapp.com",
                StorageStatePath = $"{IOHelpers.ProjectPath}/Settings/login.json"
            };
        }
    }
}
