using FluentAssertions;
using PlaywrightPOC.Hooks;
using PlaywrightPOC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TestLibrary.Steps
{
    /// <summary>
    /// Defines the step bindings for the BDD test scenarios in the Journey Planner.
    /// </summary>

    [Binding]
    internal class WebSteps : ObjectFactory
    {
        private readonly Context TestContext;

        public WebSteps(Context context) : base(context)
        {
            TestContext = context;
        }

        [When(@"I accept privacy")]
        public async Task WhenIAcceptPrivacy()
        {
            await PlanJourney.Value.AcceptPrivacy();
        }

        [When(@"I click on Plan journey")]
        public async Task WhenIClickOnPlanJourney()
        {
            await PlanJourney.Value.ClickJourneyPlanner();
        }

        [When(@"I enter ""(.*)"" in the from field")]
        public async Task WhenIEnterInTheFromField(string from)
        {
            from = from switch
            {
                "Random" => Guid.NewGuid().ToString("N").Substring(0, 10),
                "Null" => string.Empty,
                _ => from
            };

            await PlanJourney.Value.EnterFromLocation(from);
        }

        [When(@"I select ""(.*)"" from the from autocomplete suggestions")]
        public async Task WhenISelectFromTheFromAutocompleteSuggestions(string from)
        {
            await PlanJourney.Value.SelectFromDropdownOption(from);
        }

        [When(@"I enter ""(.*)"" in the to field")]
        public async Task WhenIEnterInTheToField(string to)
        {
            to = to switch
            {
                "Random" => Guid.NewGuid().ToString("N").Substring(0, 10),
                "Null" => string.Empty,
                _ => to
            };

            await PlanJourney.Value.EnterToLocation(to);
        }

        [When(@"I select ""(.*)"" from the to autocomplete suggestions")]
        public async Task WhenISelectFromTheToAutocompleteSuggestions(string to)
        {
            await PlanJourney.Value.SelectToDropdownOption(to);
        }

        [Then(@"I should see both walking and cycling times for the journey")]
        public async Task ThenIShouldSeeBothWalkingAndCyclingTimesForTheJourney()
        {
            await JourneyResults.Value.WaitUntilJourneyOptionsVisible();

            bool isCyclingVisible = await JourneyResults.Value.CyclingOption.IsVisibleAsync();
            isCyclingVisible.Should().BeTrue("the 'Cycling' option should be visible on the page.");

            bool isWalkingVisible = await JourneyResults.Value.WalkingOption.IsVisibleAsync();
            isWalkingVisible.Should().BeTrue("the 'Walking' option should be visible on the page.");
        }

        [When(@"I edit preferences")]
        public async Task WhenIEditPreferences()
        {
            await JourneyResults.Value.EditPreferences();
        }

        [When(@"I select routes with least walking")]
        public async Task WhenISelectRoutesWithLeastWalking()
        {
            await JourneyResults.Value.SelectRoutesWithLeastWalking();
        }

        [When(@"I update the journey")]
        public async Task WhenIUpdateTheJourney()
        {
            await JourneyResults.Value.UpdateTheJourney();
        }

        [Then(@"I validate the journey time is displayed")]
        public async Task ThenIValidateTheJourneyTimeIsDisplayed()
        {
            await JourneyResults.Value.WaitUntilRoutesWithLeastWalkingResultsVisible();

            var journeyTimes = await JourneyResults.Value.RoutesWithLeastWalkingResults.AllTextContentsAsync();

            // Assert there are elements and each element contains "min"
            journeyTimes.Should().NotBeEmpty("at least one journey time element should be displayed");
            journeyTimes.Should().AllSatisfy(text => text.Should().Contain("min", "each journey time element should contain 'min'"));
        }

        [When(@"I click on View Details")]
        public async Task WhenIClickOnViewDetails()
        {
            await JourneyResults.Value.ClickOnViewDetails();
        }

        [Then(@"I verify complete access information at ""(.*)""")]
        public async Task ThenIVerifyCompleteAccessInformationAt(string location, Table table)
        {
            // Extract access information from the DataTable
            var accessInformation = table.Rows.Select(row => row["Access Information"]).ToList();

            // Get the actual access information from the location
            List<string> accessInfos = await JourneyResults.Value.GetAccessInformation(location);

            // Compare the two lists using Fluent Assertions
            accessInformation.Should().BeEquivalentTo(accessInfos);
        }

        [When(@"I verify there are no results in the autocomplete suggestions")]
        public async Task WhenIVerifyThereAreNoResultsInTheAutocompleteSuggestionsForFrom()
        {
            var count = await PlanJourney.Value.LocationSuggestions.CountAsync();
            count.Should().Be(0, because: "no location suggestions should be displayed");
        }

        [Then(@"I verify that the message ""(.*)"" appears in the From field\.")]
        public async Task ThenIVerifyThatTheMessageAppearsInTheFromField_(string message)
        {
            // Get the inner text asynchronously
            string actualMessage = await PlanJourney.Value.FromErrorMessage.InnerTextAsync();

            // Use Fluent Assertions to verify the message
            actualMessage.Should().Be(message, "the error message in the 'From' field should match the expected message.");
        }

        [Then(@"I verify that the message ""(.*)"" appears in the To field\.")]
        public async Task ThenIVerifyThatTheMessageAppearsInTheToField_(string message)
        {
            // Get the inner text asynchronously
            string actualMessage = await PlanJourney.Value.ToErrorMessage.InnerTextAsync();

            // Use Fluent Assertions to verify the message
            actualMessage.Should().Be(message, "the error message in the 'To' field should match the expected message.");
        }
    }
}
