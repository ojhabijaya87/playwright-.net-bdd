Feature: Journey Planner1
 As a user, I want to use the TfL Journey Planner to plan journeys between various locations

Background:
	When I accept privacy

Scenario Outline: Plan a valid journey between two stations
	When I enter "<from>" in the from field
	And I select "<fromLocation>" from the from autocomplete suggestions
	And I enter "<to>" in the to field
	And I select "<toLocation>" from the to autocomplete suggestions
	And I click on Plan journey
	Then I should see both walking and cycling times for the journey

Examples:
	| from  | fromLocation                         | to   | toLocation                        |
	| Leice | Leicester Square Underground Station | Cove | Covent Garden Underground Station |
     
Scenario Outline: Plan a journey and validate journey time with least walking preference
	When I enter "<from>" in the from field
	And I select "<fromLocation>" from the from autocomplete suggestions
	And I enter "<to>" in the to field
	And I select "<toLocation>" from the to autocomplete suggestions
	And I click on Plan journey
	And I edit preferences
	And I select routes with least walking
	And I update the journey
	Then I validate the journey time is displayed
     
Examples:
	| from  | fromLocation                         | to   | toLocation                        |
	| Leice | Leicester Square Underground Station | Cove | Covent Garden Underground Station |

Scenario Outline: Plan a journey and validate complete access information
	When I enter "<from>" in the from field
	And I select "<fromLocation>" from the from autocomplete suggestions
	And I enter "<to>" in the to field
	And I select "<toLocation>" from the to autocomplete suggestions
	And I click on Plan journey
	And I edit preferences
	And I select routes with least walking
	And I update the journey
	Then I validate the journey time is displayed
	When I click on View Details
	Then I verify complete access information at "Covent Garden Underground Station"
		| Access Information |
		| Up stairs          |
		| Up lift            |
		| Level walkway      |

Examples:
	| from  | fromLocation                         | to   | toLocation                        |
	| Leice | Leicester Square Underground Station | Cove | Covent Garden Underground Station |
     

Scenario: No results are provided for an invalid journey
	When I enter "<from>" in the from field
	And I verify there are no results in the autocomplete suggestions
	And I enter "<to>" in the to field
	And I verify there are no results in the autocomplete suggestions

Examples:
	| from   | to     |
	| Random | Random |

Scenario: Journey cannot be planned with no locations
	When I enter "<from>" in the from field
	And I click on Plan journey
	Then I verify that the message "The From field is required." appears in the From field.
	Then I verify that the message "The To field is required." appears in the To field.
Examples:
	| from | to   |
	| Null | Null |