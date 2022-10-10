using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using TechCon.Tests.Data;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts
{
    public class DebuggingView : PageTest
    {
        // Enable in setting file
        [Test]
        public async Task DebugView()
        {
            // 1. Navigate to 'Shadow DOM' page
            await Page.GotoAsync("chrome://downloads/");

            // 2. Locate an element under 'my-paragraph' shadow root
            await Page.Locator("#searchInput").FillAsync("Techcon");

            // VP: Verify that the iframe is containing 'Let's have some different text!' value
            await Expect(Page.Locator("#no-downloads")).ToContainTextAsync("No search results found");
        }


        // Selectors
        // https://playwright.dev/docs/selectors
        // https://www.w3.org/TR/wai-aria-1.2/#role_definitions
        // https://datatables.net/
        // role=textbox
        // role=searchbox
        // .dtr-data:below(td:text-is('Airi Satou')) >> nth=0
        // td:right-of(td:text-is('Airi Satou')) >> nth = 0
        // css=#example_wrapper >> role=searchbox
        // css=#example_wrapper >> xpath=//tbody >> css=td:nth-child(5)




    }
}
