using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace PlaywrightPOC.Utilities
{
    /// <summary>
    /// Helper class to handle configuration settings and environment variables for the application.
    /// </summary>
    public class TestConfigHelper
    {
        public static BrowserType Browser { get; private set; }
        private static readonly string WorkingDirectory = Environment.CurrentDirectory;
        private static readonly string ProjectPath = Directory.GetParent(WorkingDirectory).Parent.Parent.FullName;

        /// <summary>
        /// Loads the base configuration from appsettings.json and environment variables.
        /// </summary>
        /// <returns>An IConfigurationRoot object containing configuration settings.</returns>
        public static IConfigurationRoot GetIConfigurationBase()
        {
            return new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(ProjectPath, "appsettings.json"), optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Retrieves environment-specific configuration settings based on the "ENVIRONMENT" environment variable.
        /// </summary>
        /// <returns>An EnvironmentConfigSettings object with configuration settings for the specified environment.</returns>
        public static EnvironmentConfigSettings GetApplicationConfiguration()
        {
            // Load environment variables from launchSettings.json
            LoadLaunchSettings();

            // Retrieve environment and browser type from environment variables
            string environment = Environment.GetEnvironmentVariable("ENVIRONMENT")?.ToLower();
            Browser = Enum.TryParse(Environment.GetEnvironmentVariable("BROWSER"), out BrowserType browserType)
                ? browserType
                : throw new ArgumentException("Invalid browser type specified in environment variables.");

            var systemConfiguration = new SystemConfiguration();
            var configRoot = GetIConfigurationBase();
            configRoot.GetSection("SystemConfiguration").Bind(systemConfiguration);

            // Return configuration settings based on the environment
            return environment switch
            {
                "development" => systemConfiguration.DevelopmentEnvironmentConfigSettings,
                "qa" => systemConfiguration.StagingEnvironmentConfigSettings,
                "remote" => systemConfiguration.RemoteEnvironmentConfigSettings,
                _ => throw new ArgumentException("Unknown environment specified in environment variables.")
            };
        }

        /// <summary>
        /// Loads environment variables from launchSettings.json for setting up the testing environment.
        /// </summary>
        private static void LoadLaunchSettings()
        {
            string launchSettingsPath = Path.Combine(ProjectPath, "Properties", "launchSettings.json");

            // Load and parse launchSettings.json to set environment variables
            using var file = File.OpenText(launchSettingsPath);
            var reader = new JsonTextReader(file);
            var jObject = JObject.Load(reader);
          
            var environmentVariables = jObject
                .GetValue("profiles")
                .SelectMany(profiles => profiles.Children())
                .SelectMany(profile => profile.Children<JProperty>())
                .Where(prop => prop.Name == "environmentVariables")
                .SelectMany(prop => prop.Value.Children<JProperty>())
                .ToList();

            if (environmentVariables == null)
                throw new FileNotFoundException("Could not find environment variables in launchSettings.json.");

            foreach (var variable in environmentVariables)
            {
                Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
            }
        }
    }
}
