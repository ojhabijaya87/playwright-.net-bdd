using Microsoft.Playwright;
using PlaywrightPOC.Hooks;
using System;
using System.Threading.Tasks;
using WebLibrary;

namespace PlaywrightPOC.Utilities
{
    class ObjectFactory
    {
        private readonly Context _context;

        // Lazy initialization of page objects for efficient resource management
        public Lazy<PlanJourney> PlanJourney => new Lazy<PlanJourney>(() => new PlanJourney(_context.Page));
        public Lazy<JourneyResults> JourneyResults => new Lazy<JourneyResults>(() => new JourneyResults(_context.Page));
        public Lazy<PlaywrightDriver> Driver => new Lazy<PlaywrightDriver>(() => new PlaywrightDriver());

        public ObjectFactory(Context context)
        {
            _context = context;
        }

        public Context Context => _context;

        // Initialization method to set up the driver and page objects
        public async Task InitializeObjectsAsync()
        {
            await Driver.Value.InitializePlaywright(TestConfigHelper.Browser, new BrowserTypeLaunchOptions { Headless = true });
        }
    }
}
