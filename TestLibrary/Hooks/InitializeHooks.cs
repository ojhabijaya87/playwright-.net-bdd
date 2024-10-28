using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightPOC.Hooks;
using PlaywrightPOC.Utilities;
using System.IO;
using System.Threading.Tasks;
using System;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Gherkin.Model;

[Binding]
internal class InitializeHooks : ObjectFactory
{
    private readonly ISpecFlowOutputHelper _log;
    private readonly EnvironmentConfigSettings _config;
    private static ExtentTest _featureNode;

    public static string _featureName { get; private set; }

    private ExtentTest _scenarioNode;

    private static readonly string _projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    private static readonly string _reportPath = Path.Combine(_projectPath, "TestResults", "Report", "ExtentReport.html");
    private static ExtentReports _extentReports;

    private readonly ScenarioContext _scenarioContext;
    private readonly Context _context;

    public InitializeHooks(Context context, ISpecFlowOutputHelper log, ScenarioContext scenarioContext) : base(context)
    {
        _context = context;
        _log = log;
        _config = TestConfigHelper.GetApplicationConfiguration();
        _scenarioContext = scenarioContext;
    }


    [BeforeFeature]
    public static void InitializeFeature(FeatureContext featureContext)
    {
        // Initialize feature node for the current feature
      
         _featureName = featureContext.FeatureInfo.Title.Replace(" ", "_");
     
        var reporter = new ExtentSparkReporter(Path.Combine(_projectPath, "TestResults", "Report", $"{_featureName}.html"));
        _extentReports = new ExtentReports();
        _extentReports.AttachReporter(reporter);

        Directory.CreateDirectory(Path.Combine(_projectPath, "TestResults", "Report"));
        Directory.CreateDirectory(Path.Combine(_projectPath, "TestResults", "Img"));
        Directory.CreateDirectory(Path.Combine(_projectPath, "TestResults", "Vid"));

        _featureNode = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public async Task InitializeScenario()
    {
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = false,
            DownloadsPath = Path.Combine(_projectPath, "TestResults", "Download")
        };

        _context.Page = await Driver.Value.InitializePlaywright(TestConfigHelper.Browser, launchOptions);
        _context.ContextValue = _context.Page.Context;
        _scenarioNode = _featureNode.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        _scenarioNode.AssignCategory(_scenarioContext.ScenarioInfo.Tags);
        await _context.Page.GotoAsync(_config.URL, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
    }

    [AfterStep]
    public async Task LogStepResultsAsync()
    {
        if (_scenarioContext.TestError != null)
        {
            try
            {
                var screenshotPath = Path.Combine(_projectPath, "TestResults", "Img", $"{Guid.NewGuid()}.png");
                await Context.Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });

                var screenshotModel = MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build();

                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenarioNode.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, screenshotModel);
                        break;
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenarioNode.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, screenshotModel);
                        break;
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenarioNode.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, screenshotModel);
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.WriteLine("Error attaching media to report: " + ex.Message);
                throw;
            }
        }
        else
        {
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                    _scenarioNode.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass(string.Empty);
                    break;
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                    _scenarioNode.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass(string.Empty);
                    break;
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                    _scenarioNode.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass(string.Empty);
                    break;
            }
        }
    }

    [AfterScenario]
    public async Task CleanupScenario()
    {
        try
        {
            await _context.ContextValue.CloseAsync();
        }
        catch (Exception ex)
        {
            _log.WriteLine("Error closing context: " + ex.Message);
            throw;
        }
        finally
        {
            _extentReports.Flush();
        }
    }

}
