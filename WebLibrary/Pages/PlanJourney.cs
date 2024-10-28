using Microsoft.Playwright;
using System.Threading.Tasks;
using WebLibrary.Extensions;

namespace WebLibrary
{
    public partial class PlanJourney
    {
        // Accepts privacy policy and reloads the page
        public async Task AcceptPrivacy()
        {
            await PrivacyAcceptButton.ClickAsync();
            await Page.ReloadAsync();
        }

        // Initiates journey search by clicking the Journey Planner button
        public async Task ClickJourneyPlanner() => await JourneyPlannerButton.ClickAsync();

        // Fills in the "From" location in the search field
        public async Task EnterFromLocation(string location) =>
            await Page.ClickAndFillByPlaceholderAsync(FromLocationInput, PlaceInput, location);

        // Fills in the "To" location in the search field
        public async Task EnterToLocation(string location) =>
            await Page.ClickAndFillByPlaceholderAsync(ToLocationInput, PlaceInput, location);

        // Selects a specified option from the "From" dropdown
        public async Task SelectFromDropdownOption(string option) =>
            await Page.SelectOptionByNameAsync(option);

        // Selects a specified option from the "To" dropdown
        public async Task SelectToDropdownOption(string option) =>
            await Page.SelectOptionByNameAsync(option);
    }
}
