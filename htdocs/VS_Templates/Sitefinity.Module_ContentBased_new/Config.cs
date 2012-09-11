using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using ProjectsModule.Data.OpenAccess;
using System.Collections.Specialized;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using System.Configuration;
using ProjectsModule.Web.UI;

namespace ProjectsModule.Configuration
{
	class ProjectsConfig : ContentModuleConfigBase
	{
		/// <summary>
		/// Initializes the default providers.
		/// </summary>
		/// <param name="providers">The providers.</param>
		protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
		{
			// add default provider
			providers.Add(new DataProviderSettings(providers)
			{
				Name = "OpenAccessProjectsDataProvider",
				Description = "A provider that stores Projects data in database using OpenAccess ORM.",
				ProviderType = typeof(OpenAccessProjectsDataProvider),
				Parameters = new NameValueCollection() { { "applicationName", "/Projects" } }
			});
		}

		/// <summary>
		/// Initializes the default backend and frontend views.
		/// </summary>
		/// <param name="contentViewControls"></param>
		protected override void InitializeDefaultViews(ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
		{
			// add backend views to configuration
			contentViewControls.Add(ProjectsDefinitions.DefineProjectsBackendContentView(contentViewControls));

			// add frontend views to configuration
			contentViewControls.Add(ProjectsDefinitions.DefineProjectsFrontendContentView(contentViewControls));
		}

		/// <summary>
		/// Gets or sets the name of the default data provider that is used to manage security.
		/// </summary>
		[ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessProjectsDataProvider")]
		public override string DefaultProvider
		{
			get { return (string)this["defaultProvider"]; }
			set { this["defaultProvider"] = value; }
		}
	}
}
