namespace PlaywrightPOC.Utilities
{
    /// <summary>
    /// Holds configuration settings for the test environment, such as URLs and credentials.
    /// </summary>
    public class EnvironmentConfigSettings
    {
        /// <summary>Gets or sets the base URL for the application under test.</summary>
        public string URL { get; set; }

        /// <summary>Gets or sets the database connection string.</summary>
        public string ConnectionString { get; set; }

        /// <summary>Gets or sets the username for authentication.</summary>
        public string UserName { get; set; }

        /// <summary>Gets or sets the password for authentication.</summary>
        public string Password { get; set; }

        /// <summary>Gets or sets the browser type to be used for testing.</summary>
        public string Browser { get; set; }

        /// <summary>Gets or sets the environment type (e.g., QA, Production).</summary>
        public string Environment { get; set; }

        // You can add more properties as needed for additional configuration values.
    }
}
