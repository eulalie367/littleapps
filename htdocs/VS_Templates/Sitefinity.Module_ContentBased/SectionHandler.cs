using System.Configuration;

namespace $rootnamespace$.ConfigurationHelpers
{
    /// <summary>
    /// Represents a handler for the section within the configuration file.
    /// </summary>
    public class SectionHandler : ConfigurationSection
    {
        /// <summary>
        /// Initializes a new instance of the ArticlesSectionHandler class.
        /// </summary>
        public SectionHandler()
        {
        }

        /// <summary>
        /// Gets or sets the name of the default provider that is used to manage users and roles.
        /// </summary>
        [ConfigurationProperty("defaultProvider"), StringValidator]
        public string DefaultProvider
        {
            get
            {
                return (string)base["defaultProvider"];
            }
            set
            {
                base["defaultProvider"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the default provider that is used to manage users and roles.
        /// </summary>
        [ConfigurationProperty("defaultGenericProvider"), StringValidator]
        public string DefaultContentProvider
        {
            get
            {
                return (string)base["defaultGenericProvider"];
            }
            set
            {
                base["defaultGenericProvider"] = value;
            }
        }


        /// <summary>
        /// Gets a ProviderSettingsCollection object of ProviderSettings objects.
        /// </summary>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base["providers"];
            }
        }

        /// <summary>
        /// Gets a MetaFieldCollection object of MetaFieldElement objects.
        /// </summary>
        [ConfigurationProperty("genericContentProviders")]
        public GenericContentSettingsCollection GenericContentProviders
        {
            get
            {
                return (GenericContentSettingsCollection)base["genericContentProviders"];
            }
        }
    }
}
