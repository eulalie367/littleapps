using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.GenericContent.Model;
using ProjectsModule.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules;

namespace ProjectsModule.Web.Services.Data
{
	public class ProjectItemViewModel : ContentViewModelBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectItemViewModel"/> class.
		/// </summary>
		public ProjectItemViewModel() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectItemViewModel"/> class.
		/// </summary>
		/// <param name="Project">The Project item.</param>
		/// <param name="provider">The provider.</param>
		public ProjectItemViewModel(ProjectItem Project, ContentDataProviderBase provider)
			: base(Project, provider)
		{
			this.Address = Project.Address;
			this.City = Project.City;
			this.Region = Project.Region;
			this.PostalCode = Project.PostalCode;
		}

		#endregion

		#region Public Methods and Overrides

		/// <summary>
		/// Get live version of this.ContentItem using this.provider
		/// </summary>
		/// <returns>
		/// Live version of this.ContentItem
		/// </returns>
		protected override Content GetLive()
		{
			return this.provider.GetLiveBase<ProjectItem>((ProjectItem)this.ContentItem);
		}

		/// <summary>
		/// Get temp version of this.ContentItem using this.provider
		/// </summary>
		/// <returns>
		/// Temp version of this.ContentItem
		/// </returns>
		protected override Content GetTemp()
		{
			return this.provider.GetTempBase<ProjectItem>((ProjectItem)this.ContentItem);
		}

		#endregion

		#region Public Properties

		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }

		#endregion
	}
}
