using System;

namespace PlaywrightPOC.Utilities
{
    /// <summary>
    /// Holds environment-specific configuration settings for the application.
    /// </summary>
    public class SystemConfiguration
    {
        /// <summary>
        /// Configuration settings for the development environment.
        /// </summary>
        public EnvironmentConfigSettings DevelopmentEnvironmentConfigSettings { get; set; }

        /// <summary>
        /// Configuration settings for the staging (QA) environment.
        /// </summary>
        public EnvironmentConfigSettings StagingEnvironmentConfigSettings { get; set; }

        /// <summary>
        /// Configuration settings for the remote environment.
        /// </summary>
        public EnvironmentConfigSettings RemoteEnvironmentConfigSettings { get; set; }
    }
}
