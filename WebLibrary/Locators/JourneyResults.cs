using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary
{
    public partial class JourneyResults
    {
        private IPage Page { get; }

        // Define locators as properties for elements on the page
        
       
        private ILocator Loader => Page.Locator("#loader-window");
        public ILocator CyclingOption => Page.Locator("[data-tracking-value='JP: Cycling']");
        public ILocator WalkingOption => Page.Locator("[data-tracking-value='JP: WalkingOnly']");
        private ILocator EditPreferencesButton => Page.GetByRole(AriaRole.Button, new() { Name = "Edit preferences" });
        private ILocator RoutesWithLeastWalking => Page.GetByText("Routes with least walking");
        public ILocator RoutesWithLeastWalkingResults => Page.Locator("div[class='journey-time no-map']");
        private ILocator UpdateJourneyButton => Page.GetByRole(AriaRole.Button, new() { Name = "Update journey" });
        private ILocator ViewDetails => Page.GetByText("View details");
        private ILocator JourneyOptions => Page.Locator("#option-1-content");

        // Constructor
        public JourneyResults(IPage page)
        {
            Page = page ?? throw new ArgumentNullException(nameof(page)); // Check for null
        }
    }
}
