using System.Configuration;
using Telerik.Cms.Engine;

namespace $rootnamespace$.ConfigurationHelpers
{
    /// <summary>
    /// Represents a generic content element within a configuration file, which allows us to
    /// access generic content properties of the s module
    /// </summary>
    public class GenericContentElement : ConfigurationElement
    {
        /// <summary>
        /// Creates a new instance of the GenericContentElement class with the default provider.
        /// </summary>
        public GenericContentElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of GenericContentElement class based on the passed provider name.
        /// </summary>
        /// <param name="providerName">name of the provider</param>
        public GenericContentElement(string providerName)
        {
            this.ProviderName = providerName;
        }

        /// <summary>
        /// Gets or sets name of the provider.
        /// </summary>
        [ConfigurationProperty("providerName",
           DefaultValue = "",
           IsRequired = true)]
        public string ProviderName
        {
            get
            {
                return (string)this["providerName"];
            }
            set
            {
                this["providerName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets rewritten format.
        /// </summary>
        [ConfigurationProperty("urlRewriteFormat",
          DefaultValue = "Thumbnail",
          IsRequired = true)]
        public string UrlRewriteFormat
        {
            get
            {
                return (string)this["urlRewriteFormat"];
            }
            set
            {
                this["urlRewriteFormat"] = value;
            }
        }

        /// <summary>
        /// Gets or sets date format.
        /// </summary>
        [ConfigurationProperty("urlDateTimeFormat",
          DefaultValue = "Thumbnail",
          IsRequired = false)]
        public string UrlDateTimeFormat
        {
            get
            {
                return (string)this["urlDateTimeFormat"];
            }
            set
            {
                this["urlDateTimeFormat"] = value;
            }
        }

        /// <summary>
        /// Gets or sets char for whitespace.
        /// </summary>
        [ConfigurationProperty("urlWhitespaceChar",
          DefaultValue = "Thumbnail",
          IsRequired = true)]
        public string UrlWhitespaceChar
        {
            get
            {
                return (string)this["urlWhitespaceChar"];
            }
            set
            {
                this["urlWhitespaceChar"] = value;
            }
        }

        /// <summary>
        /// Gets or sets cache mode of url.
        /// </summary>
        [ConfigurationProperty("urlCacheMode",
            DefaultValue = "All",
            IsRequired = false)]
        public Telerik.Cms.Engine.UrlCacheMode UrlCacheMode
        {
            get
            {
                return (UrlCacheMode)this["urlCacheMode"];
            }
            set
            {
                this["urlCacheMode"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the order of left side menu of the My Preference Page.
        /// </summary>
        /// <value>The setting order.</value>
        [ConfigurationProperty("settingOrder", DefaultValue = 4, IsRequired = false)]
        public int SettingOrder
        {
            get
            {
                return (int)this["settingOrder"];
            }
            set
            {
                this["settingOrder"] = value;
            }
        }
    }
}
