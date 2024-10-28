using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary
{
    public partial class PlanJourney
    {
        private IPage Page { get; }

        // Define locators as properties for elements on the page
        private ILocator PrivacyAcceptButton => Page.Locator("#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
        private ILocator FromLocationInput => Page.GetByPlaceholder("From");
        public ILocator LocationSuggestions => Page.Locator(".tt-suggestion");
        public ILocator ToErrorMessage => Page.Locator("#InputTo-error");
        public ILocator FromErrorMessage => Page.Locator("#InputFrom-error");
        private ILocator ToLocationInput => Page.GetByPlaceholder("To", new() { Exact = true });
        private ILocator JourneyPlannerButton => Page.Locator("#plan-journey-button");
      
        private string PlaceInput => "Place or address";
      

        // Constructor
        public PlanJourney(IPage page)
        {
            Page = page ?? throw new ArgumentNullException(nameof(page)); // Check for null
        }
    }
}
