using Microsoft.Playwright;
using PlaywrightPOC.Hooks;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightPOC.Utilities
{
    /// <summary>
    /// Driver class to initialize and manage Playwright browser interactions.
    /// </summary>
    public class PlaywrightDriver
    {
        public IPage Page { get; private set; }
        public IBrowserContext Context { get; private set; }
        public EnvironmentConfigSettings Config { get; private set; }

        private IBrowser _browserInstance = null;
        private static readonly string _workingDirectory = Environment.CurrentDirectory;
        private readonly string _projectPath = Directory.GetParent(_workingDirectory).Parent.Parent.FullName;

        /// <summary>
        /// Constructor to initialize configuration settings.
        /// </summary>
        public PlaywrightDriver()
        {
            Config = TestConfigHelper.GetApplicationConfiguration();
        }

        /// <summary>
        /// Initializes the Playwright browser with specified browser type and launch options.
        /// </summary>
        /// <param name="browser">The type of browser to initialize (e.g., Chrome, Firefox, WebKit).</param>
        /// <param name="launchOptions">Options for launching the browser.</param>
        /// <returns>Initialized IPage instance.</returns>
        public async Task<IPage> InitializePlaywright(BrowserType browser, BrowserTypeLaunchOptions launchOptions)
        {
            try
            {
                var playwright = await Playwright.CreateAsync();

                // Select the appropriate browser based on the type specified
                _browserInstance = browser switch
                {
                    BrowserType.Chrome => await playwright.Chromium.LaunchAsync(launchOptions),
                    BrowserType.Firefox => await playwright.Firefox.LaunchAsync(launchOptions),
                    BrowserType.WebKit => await playwright.Webkit.LaunchAsync(launchOptions),
                    _ => throw new ArgumentException("Unsupported browser type.")
                };

                // Set up browser context with specific options
                Context = await _browserInstance.NewContextAsync(new BrowserNewContextOptions
                {
                    AcceptDownloads = true,
                    RecordVideoDir = Path.Combine(_projectPath, "TestResults", "Vid"),
                    RecordVideoSize = new RecordVideoSize { Width = 1920, Height = 1080 },
                    HttpCredentials = new HttpCredentials
                    {
                        Username = Config.UserName,
                        Password = Config.Password
                    },
                    JavaScriptEnabled = true,
                    Locale = "en-GB",
                });

                // Create a new page within the context and set timeouts
                Page = await Context.NewPageAsync();
                
                Page.SetDefaultNavigationTimeout(30000);
                Page.SetDefaultTimeout(30000);

                return Page;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to initialize Playwright.", ex);
            }
        }
    }
}
