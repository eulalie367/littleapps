﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using ProjectsModule.Web.UI.Public.Designers;

namespace ProjectsModule.Web.UI.Public
{
	[RequireScriptManager]
	[ControlDesigner(typeof(ProjectsViewDesigner))]
	public class ProjectsView : ContentView
	{
		/// <summary>
		/// Gets or sets the name of the module which initialization should be ensured prior to rendering this control.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public override string ModuleName
		{
			get
			{
				if (String.IsNullOrEmpty(base.ControlDefinitionName)) return ProjectsModule.ModuleName;
				return base.ModuleName;
			}
			set
			{
				base.ModuleName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the configuration definition for the whole control. From this definition
		/// control can find out all other configurations needed in order to construct views.
		/// </summary>
		/// <value>The name of the control definition.</value>
		public override string ControlDefinitionName
		{
			get
			{
				if (String.IsNullOrEmpty(base.ControlDefinitionName))
					return ProjectsDefinitions.FrontendDefinitionName;
				return base.ControlDefinitionName;
			}
			set
			{
				base.ControlDefinitionName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the master view to be loaded when
		/// control is in the ContentViewDisplayMode.Master
		/// </summary>
		/// <value></value>
		public override string MasterViewName
		{
			get
			{
				if (!String.IsNullOrEmpty(base.MasterViewName))
					return base.MasterViewName;

				return ProjectsDefinitions.FrontendListViewName;
			}
			set
			{
				base.MasterViewName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the detail view to be loaded when
		/// control is in the ContentViewDisplayMode.Detail
		/// </summary>
		/// <value></value>
		public override string DetailViewName
		{
			get
			{
				if (!String.IsNullOrEmpty(base.DetailViewName))
					return base.DetailViewName;

				return ProjectsDefinitions.FrontendDetailViewName;
			}
			set
			{
				base.DetailViewName = value;
			}
		}

		/// <summary>
		/// Gets or sets the text to be shown when the box in the designer is empty
		/// </summary>
		/// <value></value>
		public override string EmptyLinkText
		{
			get
			{
				return "Edit";
			}
		}
	}
}
