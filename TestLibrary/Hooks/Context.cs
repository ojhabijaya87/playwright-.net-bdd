using Microsoft.Playwright;

namespace PlaywrightPOC.Hooks
{
    /// <summary>
    /// Holds shared context for Playwright operations, including browser, page, video, and configuration details.
    /// </summary>
    internal class Context
    {
        public Context() { }

        /// <summary>Represents the active page used for test interactions.</summary>
        public IPage Page { get; set; }

        /// <summary>Maintains the current browser context (e.g., session, cookies).</summary>
        public IBrowserContext ContextValue { get; set; }

        /// <summary>Handles video recording for tests.</summary>
        public IVideo Video { get; set; }

        /// <summary>Holds the database connection string for the test environment.</summary>
        public string ConnectionString { get; set; }

        /// <summary>Specifies the base URL for testing.</summary>
        public string URL { get; set; }

        /// <summary>Stores the username for authentication in tests.</summary>
        public string UserName { get; set; }

        /// <summary>Stores the password for authentication in tests.</summary>
        public string Password { get; set; }
    }
}
