using System.Configuration;

namespace $rootnamespace$.ConfigurationHelpers
{
    /// <summary>
    /// Helper class used to get a SectionHandler for the $fileinputname$s module in web.config
    /// </summary>
    public class ConfigHelper
    {
        public static SectionHandler Handler(string sectionName)
        {
            return (SectionHandler)ConfigurationManager.GetSection(sectionName); ;
        }
    }
}
