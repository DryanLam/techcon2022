using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechCon.Tests.Data;
using TechCon.Tests.Utils.Helpers;

namespace TechCon.Tests.Scripts
{
    public class UploadFixture : PageTest
    {
        [Test]
        public async Task Upload_ByPath()
        {
            // 1. Navigate to 'Add Employee' page
            await Page.GotoAsync("/web/index.php/pim/addEmployee");

            // 2. Add an image
            await Page.Locator("input[type=file]").SetInputFilesAsync(@"Data\Img\playwright.png");

            // VP: Verify that the 'src' attribute is changed default value 'user-default'
            await Expect(Page.Locator("img.employee-image")).Not.ToHaveAttributeAsync("src", new Regex("user-default"));
        }


        [Test]
        public async Task Upload_ByFileChooser()
        {
            // 1. Navigate to 'Add Employee' page
            await Page.GotoAsync("/web/index.php/pim/addEmployee");

            // 2. Add an image
            await Page.Locator("input[type=file]").SetInputFilesAsync(@"Data\Img\playwright.png");

            var fileChooser = await Page.RunAndWaitForFileChooserAsync(async () =>
            {
                await Page.Locator("button.employee-image-action").ClickAsync();
            });

            await fileChooser.SetFilesAsync(@"Data\Img\playwright.png");


            // VP: Verify that the 'src' attribute is changed default value 'user-default'
            await Expect(Page.Locator("img.employee-image")).Not.ToHaveAttributeAsync("src", new Regex("user-default"));
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
