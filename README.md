
# PlaywrightDemo Project

## Overview

The **PlaywrightDemo** project is a .NET test automation framework using **Playwright** with **SpecFlow** for testing the journey planning functionality of the TfL (Transport for London) website. This framework is structured to support BDD (Behavior-Driven Development) testing, enabling clean separation between feature files, step definitions, and supporting classes.

### Key Features

1. **Playwright** - Provides powerful and reliable browser automation.
2. **SpecFlow** - Enables Behavior-Driven Development (BDD) with Gherkin syntax.
3. **ExtentReports** - Generates structured and visually appealing HTML reports with screenshots for failed steps.
4. **Parallel Execution** - Supports fixtures level parallel test execution to speed up testing and provide faster feedback.
1. **GitHub Action Integration** - Integrated with GitHub Action CI

### Project Structure

This solution contains two main projects:
1. **TestLibrary** - Contains the SpecFlow feature files, step definitions, hooks, test utilities, and configuration files.
2. **WebLibrary** - Contains page object models, locators, and extension methods for reusable web interactions.

## Folder Structure

### TestLibrary

- **Features**: Contains `.feature` files for BDD scenarios.  
  - `JourneyPlanner.feature`: The main feature file for testing journey planning functionality on the TfL website.
  
- **Hooks**: Defines global test setup and teardown logic.
  - `Context.cs`: Manages shared test data or states.
  - `InitializeHooks.cs`: Contains setup and teardown methods that execute before and after test runs.

- **Properties**: Contains assembly-level metadata attributes and launch configurations.
  - `AssemblyInfo.cs`: Contains assembly-level metadata attributes, including NUnit parallelization settings.
  - `launchSettings.json`: Defines launch configurations and environment settings for running the application.

- **Steps**: Contains SpecFlow step definitions for feature files.
  - `WebSteps.cs`: Implements the steps for interacting with web elements on the TfL website.

- **TestResults**: Stores test result outputs.
  - `Img`: Stores screenshots from test runs.
  - `Report`: Contains generated HTML reports using Extent Reports.
  - `Vid`: Stores any video recordings of test executions.

- **Utilities**: Contains helper classes for test configuration and browser setup.
  - `BrowserType.cs`: Defines supported browser types.
  - `EnvironmentConfigSettings.cs`: Manages environment-specific configurations.
  - `ObjectFactory.cs`: Manages lazy-loaded instances of page objects and the Playwright driver.
  - `PlaywrightDriver.cs`: Initializes and manages Playwright browser instances.
  - `SystemConfiguration.cs`: Manages system-level settings.
  - `TestConfigHelper.cs`: Provides helper methods for test configuration.

- **Configuration Files**:
  - `appsettings.json`: Stores environment and application-specific settings.

### WebLibrary

- **Extensions**: Contains helper methods that extend Playwright functionalities.
  - `Extensions.cs`: Adds reusable methods for interacting with web elements.

- **Locators**: Defines element locators for different pages.
  - `JourneyResults.cs`: Locators for journey results.
  - `PlanJourney.cs`: Locators for planning a journey.

- **Pages**: Defines page object classes for managing page interactions.
  - `JourneyResults.cs`: Page object for the journey results page.
  - `PlanJourney.cs`: Page object for the journey planning page.

## Getting Started

### Prerequisites

- .NET SDK
- Playwright for .NET
- SpecFlow
- Extent Reports (for reporting)

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ojhabijaya87/playwright-specflow.git
   ```
   ```bash
   cd playwright-specflow
   ```
   
2. **Navigate to the TestLibrary Directory**:
   ```bash
   cd TestLibrary
   ```

3. **Install .NET Tool Manifest**:
   ```bash
   dotnet new tool-manifest
   ```

4. **Install Playwright CLI**

 ```bash
  dotnet tool install Microsoft.Playwright.CLI
 ```


### Running Tests

1. **Execute All Tests**:
   ```bash
   dotnet test
   ```

2. **Generate Reports**:
   After test execution, an HTML report is generated in the `TestResults/Report/{_featureName}.html` file.

3. **View Screenshots and Videos**:
   Screenshots and videos are saved in the `TestResults/Img` and `TestResults/Vid` directories respectively.

### Configuration

- Update `appsettings.json` to modify environment-specific settings.
- Set browser configurations in `BrowserType.cs` and `PlaywrightDriver.cs`.

## Parallel Execution

- Enable Parallelism: The [assembly: Parallelizable()] attribute in AssemblyInfo.cs allows features to run concurrently, improving test execution time.
- Execution Control: The [assembly: LevelOfParallelism()] attribute in AssemblyInfo.cs allows to set the level of parallelism to control the number of concurrent threads

## Reporting

- This framework uses **Extent Reports** to generate detailed HTML reports after each test run. Reports can be found in the `TestResults/Report` directory.

## Contributing

Feel free to open a pull request if you'd like to contribute to this framework. Make sure your code adheres to the coding standards and includes necessary tests.

## License

This project is licensed under the MIT License.

# PlaywrightDemo Project

## Overview

The **PlaywrightDemo** project is a .NET test automation framework using **Playwright** with **SpecFlow** for testing the journey planning functionality of the TfL (Transport for London) website. This framework is structured to support BDD (Behavior-Driven Development) testing, enabling clean separation between feature files, step definitions, and supporting classes.

### Key Features

1. **Playwright** - Provides powerful and reliable browser automation.
2. **SpecFlow** - Enables Behavior-Driven Development (BDD) with Gherkin syntax.
3. **ExtentReports** - Generates structured and visually appealing HTML reports with screenshots for failed steps.
4. **Parallel Execution** - Supports fixtures level parallel test execution to speed up testing and provide faster feedback.
1. **GitHub Action Integration** - Integrated with GitHub Action CI

### Project Structure

This solution contains two main projects:
1. **TestLibrary** - Contains the SpecFlow feature files, step definitions, hooks, test utilities, and configuration files.
2. **WebLibrary** - Contains page object models, locators, and extension methods for reusable web interactions.

## Folder Structure

### TestLibrary

- **Features**: Contains `.feature` files for BDD scenarios.  
  - `JourneyPlanner.feature`: The main feature file for testing journey planning functionality on the TfL website.
  
- **Hooks**: Defines global test setup and teardown logic.
  - `Context.cs`: Manages shared test data or states.
  - `InitializeHooks.cs`: Contains setup and teardown methods that execute before and after test runs.

- **Properties**: Contains assembly-level metadata attributes and launch configurations.
  - `AssemblyInfo.cs`: Contains assembly-level metadata attributes, including NUnit parallelization settings.
  - `launchSettings.json`: Defines launch configurations and environment settings for running the application.

- **Steps**: Contains SpecFlow step definitions for feature files.
  - `WebSteps.cs`: Implements the steps for interacting with web elements on the TfL website.

- **TestResults**: Stores test result outputs.
  - `Img`: Stores screenshots from test runs.
  - `Report`: Contains generated HTML reports using Extent Reports.
  - `Vid`: Stores any video recordings of test executions.

- **Utilities**: Contains helper classes for test configuration and browser setup.
  - `BrowserType.cs`: Defines supported browser types.
  - `EnvironmentConfigSettings.cs`: Manages environment-specific configurations.
  - `ObjectFactory.cs`: Manages lazy-loaded instances of page objects and the Playwright driver.
  - `PlaywrightDriver.cs`: Initializes and manages Playwright browser instances.
  - `SystemConfiguration.cs`: Manages system-level settings.
  - `TestConfigHelper.cs`: Provides helper methods for test configuration.

- **Configuration Files**:
  - `appsettings.json`: Stores environment and application-specific settings.

### WebLibrary

- **Extensions**: Contains helper methods that extend Playwright functionalities.
  - `Extensions.cs`: Adds reusable methods for interacting with web elements.

- **Locators**: Defines element locators for different pages.
  - `JourneyResults.cs`: Locators for journey results.
  - `PlanJourney.cs`: Locators for planning a journey.

- **Pages**: Defines page object classes for managing page interactions.
  - `JourneyResults.cs`: Page object for the journey results page.
  - `PlanJourney.cs`: Page object for the journey planning page.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Playwright for .NET
- SpecFlow

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ojhabijaya87/playwright-specflow.git
   ```

   ```bash
   cd playwright-specflow
   ```
   
2. **Navigate to the TestLibrary Directory**:
   ```bash
   cd TestLibrary
   ```

3. **Install .NET Tool Manifest**:
   ```bash
   dotnet new tool-manifest
   ```

4. **Install Playwright CLI**:
   ```bash
   dotnet tool install Microsoft.Playwright.CLI
   ```

5. **Build the Solution**:
   ```bash
   dotnet build
   ```

6. **Install Playwright Browsers**:
   ```bash
   pwsh bin/Debug/net8.0/playwright.ps1 install
   ```

### Running Tests

1. **Execute All Tests**:
   ```bash
   dotnet test
   ```

2. **Generate Reports**:
   After test execution, an HTML report is generated in the `TestResults/Report/{_featureName}.html` file.

3. **View Screenshots and Videos**:
   Screenshots and videos are saved in the `TestResults/Img` and `TestResults/Vid` directories respectively.

### Configuration

- Update `appsettings.json` to modify environment-specific settings.
- Set browser configurations in `BrowserType.cs` and `PlaywrightDriver.cs`.

## Parallel Execution

- Enable Parallelism: The [assembly: Parallelizable()] attribute in AssemblyInfo.cs allows features to run concurrently, improving test execution time.
- Execution Control: The [assembly: LevelOfParallelism()] attribute in AssemblyInfo.cs allows to set the level of parallelism to control the number of concurrent threads

## Reporting

- This framework uses **Extent Reports** to generate detailed HTML reports after each test run. Reports can be found in the `TestResults/Report` directory.

## Contributing

Feel free to open a pull request if you'd like to contribute to this framework. Make sure your code adheres to the coding standards and includes necessary tests.

## License

This project is licensed under the MIT License.
