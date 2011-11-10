using Telerik.Sitefinity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Abstractions;
using ProjectsModule.Data;
using ProjectsModule.Configuration;
using ProjectsModule.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using ProjectsModule.Web.Services;
using ProjectsModule.Web.UI;
using Telerik.Sitefinity.Modules.ControlTemplates;
using ProjectsModule.Web.UI.Public;

namespace ProjectsModule
{
	public class ProjectsModule : ContentModuleBase
	{
		/// <summary>
		/// Initializes the service with specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public override void Initialize(ModuleSettings settings)
		{
			base.Initialize(settings);

			// initialize configuration file
			Config.RegisterSection<ProjectsConfig>();

			// register web services
			ObjectFactory.RegisterWebService(typeof(ProjectsBackendService), "Sitefinity/Services/Content/Projects.svc");
		}

		/// <summary>
		/// Installs this module in Sitefinity system for the first time.
		/// </summary>
		/// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
		public override void Install(SiteInitializer initializer)
		{
			base.Install(initializer);

			// register module ?
			IModule ProjectsModule;
			SystemManager.ApplicationModules.TryGetValue(ProjectsModule.ModuleName, out ProjectsModule);

			initializer.Context.SaveMetaData(true);
		}

		/// <summary>
		/// Installs the pages.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		protected override void InstallPages(SiteInitializer initializer)
		{
			// code to install admin page nodes and pages
			// get page manager
			var pageManager = initializer.PageManager;
			var modulesPageNode = pageManager.GetPageNode(SiteInitializer.ModulesNodeId);

			// Create PageNode if doesn't exist
			var ProjectsModulePageGroupNode = pageManager.GetPageNodes().Where(t => t.Id == ProjectsPageGroupID).SingleOrDefault();
			if (ProjectsModulePageGroupNode == null)
			{
				// create page node under Modules node
				ProjectsModulePageGroupNode = initializer.CreatePageNode(ProjectsPageGroupID, modulesPageNode, Telerik.Sitefinity.Pages.Model.NodeType.Group);
				ProjectsModulePageGroupNode.Name = ProjectsModule.ModuleName;
				ProjectsModulePageGroupNode.ShowInNavigation = true;
				ProjectsModulePageGroupNode.Attributes["ModuleName"] = ProjectsModule.ModuleName;

				// hard-code names for now, will eventually be localized
				ProjectsModulePageGroupNode.Title = "Projects";
				ProjectsModulePageGroupNode.UrlName = "Projects";
				ProjectsModulePageGroupNode.Description = "Module for managing a list of Projects";
			}

			// create Landing Page if doesn't exist
			var landingPage = pageManager.GetPageNodes().SingleOrDefault(p => p.Id == LandingPageId);
			if (landingPage == null)
			{
				// create page
				var pageInfo = new PageDataElement()
				{
					PageId = LandingPageId,
					IncludeScriptManager = true,
					ShowInNavigation = false,
					EnableViewState = false,
					TemplateName = SiteInitializer.BackendTemplateName,

					// hard-code names for now, will eventually be localized
					Name = ProjectsModule.ModuleName,
					MenuName = "Projects Module",
					UrlName = "Projects",
					Description = "Landing page for the Projects Module",
					HtmlTitle = "Projects Module"
				};

				pageInfo.Parameters["ModuleName"] = ProjectsModule.ModuleName;

				// create control panel
				var backendView = new BackendContentView()
				{
					ModuleName = ProjectsModule.ModuleName,
					ControlDefinitionName = ProjectsDefinitions.BackendDefinitionName
				};

				// add page
				initializer.CreatePageFromConfiguration(pageInfo, ProjectsModulePageGroupNode, backendView);
			}
		}

		public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
		{
			// not needed
		}

		/// <summary>
		/// Registers the module data item type into the taxonomy system
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		protected override void InstallTaxonomies(SiteInitializer initializer)
		{
			this.InstallTaxonomy(initializer, typeof(ProjectItem));
		}

		/// <summary>
		/// Gets the module config.
		/// </summary>
		/// <returns></returns>
		protected override ConfigSection GetModuleConfig()
		{
			// code to return Module configuration
			return Config.Get<ProjectsConfig>();
		}

		/// <summary>
		/// Installs module's toolbox configuration.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		protected override void InstallConfiguration(SiteInitializer initializer)
		{
			// get section from toolbox
			var config = initializer.Context.GetConfig<ToolboxesConfig>();
			var pageControls = config.Toolboxes["PageControls"];
			var section = pageControls
				.Sections
				.Where<ToolboxSection>(e => e.Name == ToolboxesConfig.ContentToolboxSectionName)
				.FirstOrDefault();

			// create it if it doesn't exist
			if (section == null)
			{
				section = new ToolboxSection(pageControls.Sections)
				{
					Name = ToolboxesConfig.ContentToolboxSectionName,
					Title = "ContentToolboxSectionTitle",
					Description = "ContentToolboxSectionDescription",
					ResourceClassId = typeof(PageResources).Name
				};
				pageControls.Sections.Add(section);
			}

			// add Projects view if it doesn't exist
			if (!section.Tools.Any<ToolboxItem>(e => e.Name == "ProjectsView"))
			{
				var tool = new ToolboxItem(section.Tools)
				{
					Name = "ProjectsView",
					Title = "Projects View",
					Description = "Public control from the Projects module",
					ModuleName = ProjectsModule.ModuleName,
					CssClass = "sfProjectsViewIcn",
					ControlType = typeof(ProjectsView).AssemblyQualifiedName
				};
				section.Tools.Add(tool);
			}
		}

		#region Public Properties

		/// <summary>
		/// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase"/> class.
		/// </summary>
		/// <value>
		/// The landing page id.
		/// </value>
		public override Guid LandingPageId
		{
			get { return ProjectsModuleLandingPage; }
		}

		public override Type[] Managers
		{
			get { return new[] { typeof(ProjectsManager) }; }
		}

		#endregion


		#region Constants

		/// <summary>
		/// The name of the Projects Module
		/// </summary>
		public const string ModuleName = "Projects";

		// Page IDs
		public static readonly Guid ProjectsPageGroupID = new Guid("000262BF-E8EA-4BE3-8C67-E1C2486A57BE");
		public static readonly Guid ProjectsModuleLandingPage = new Guid("7A0F43CE-064A-4E09-A3B9-59CA2E1640A6");

		#endregion
	}
}