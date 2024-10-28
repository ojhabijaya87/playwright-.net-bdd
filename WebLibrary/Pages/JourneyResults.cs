using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLibrary.Extensions;

namespace WebLibrary
{
    public partial class JourneyResults
    {
        // Clicks on the "View Details" button for the first result.
        public async Task ClickOnViewDetails() => await ViewDetails.First.ClickAsync();

        // Waits until the loading indicator is hidden, then clicks the "Edit Preferences" button.
        public async Task EditPreferences()
        {
            await Loader.WaitUntilHiddenAsync();
            await EditPreferencesButton.ClickAsync();
        }

        // Selects the "Routes with Least Walking" option.
        public async Task SelectRoutesWithLeastWalking() => await RoutesWithLeastWalking.ClickAsync();

        // Clicks the "Update Journey" button to apply any updated preferences.
        public async Task UpdateTheJourney() => await UpdateJourneyButton.ClickAsync();

        // Waits until journey options (Cycling and Walking) are visible on the page.
        public async Task WaitUntilJourneyOptionsVisible()
        {
            // Ensures the loader is hidden before checking visibility of the journey options
            await Loader.WaitUntilHiddenAsync();

            // Waits concurrently for both Cycling and Walking options to become visible
            await Task.WhenAll(
                CyclingOption.WaitUntilVisibleAsync(),
                WalkingOption.WaitUntilVisibleAsync()
            );
        }

        // Waits until the results for "Routes with Least Walking" are visible.
        public async Task WaitUntilRoutesWithLeastWalkingResultsVisible()
        {
            // Ensures the loader is hidden before checking visibility of the results
            await Loader.WaitUntilHiddenAsync();

            // Waits until the first result for "Routes with Least Walking" becomes visible
            await RoutesWithLeastWalkingResults.First.WaitUntilVisibleAsync();
        }

        // Retrieves access information based on a specified location name.
        public async Task<List<string>> GetAccessInformation(string location)
        {
            // Finds the parent element containing details for the specified location
            var detailsParent = JourneyOptions
                .GetByText(location)
                .Nth(1)
                .Locator("..")
                .Locator("..")
                .Locator("..");

            // Selects all elements with access information links within the details parent
            var accessInformationElements = await detailsParent.Locator(".access-information a").AllAsync();

            // Retrieves the "data-title" attribute values from each access information link
            var dataTitleValues = await Task.WhenAll(
                accessInformationElements
                    .Select(async element => await element.GetAttributeAsync("data-title"))
            );

            // Returns a list of non-empty data-title values
            return dataTitleValues
                .Where(title => !string.IsNullOrEmpty(title))
                .ToList();
        }
    }
}
