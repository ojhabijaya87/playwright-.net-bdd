using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace WebLibrary.Extensions
{
    public static class PlaywrightExtensions
    {
        // Waits until an element is visible on the page
        public static async Task WaitUntilVisibleAsync(this ILocator locator, int timeout = 60000)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = timeout });
        }

        // Waits until an element is hidden on the page
        public static async Task WaitUntilHiddenAsync(this ILocator locator, int timeout = 60000)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden, Timeout = timeout });
        }

        // Clicks an element by its role and name (e.g., selecting a dropdown option by name)
        public static async Task ClickByRoleAsync(this IPage page, AriaRole role, string name)
        {
            await page.GetByRole(role, new() { Name = name }).ClickAsync();
        }

        // Clicks on an element, finds an input by placeholder, and fills in the specified location
        public static async Task ClickAndFillByPlaceholderAsync(this IPage page, ILocator locator, string placeholderText, string inputText)
        {
            await locator.ClickAsync();
            await page.GetByPlaceholder(placeholderText).FillAsync(inputText);
        }

        // Selects an option from a dropdown or list by option name
        public static async Task SelectOptionByNameAsync(this IPage page, string optionName)
        {
            await page.GetByRole(AriaRole.Option, new() { Name = optionName }).ClickAsync();
        }
    }
}
