using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectsModule.Model;
using ProjectsModule.Data;
using ProjectsModule.Web.Services.Data;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules;

namespace ProjectsModule.Web.Services
{
	public class ProjectsBackendService : ContentServiceBase<ProjectItem, ProjectItemViewModel, ProjectsManager>
	{
		/// <summary>
		/// Gets the content items.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override IQueryable<ProjectItem> GetContentItems(string providerName)
		{
			return this.GetManager(providerName).GetProjects();
		}

		/// <summary>
		/// Gets the child content items.
		/// </summary>
		/// <param name="parentId">The parent id.</param>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override IQueryable<ProjectItem> GetChildContentItems(Guid parentId, string providerName)
		{
			// TODO: Implement this method
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the content item.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override ProjectItem GetContentItem(Guid id, string providerName)
		{
			return this.GetManager(providerName).GetProject(id);
		}

		/// <summary>
		/// Gets the parent content item.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override ProjectItem GetParentContentItem(Guid id, string providerName)
		{
			// TODO: Implement this method
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the manager.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public override ProjectsManager GetManager(string providerName)
		{
			return ProjectsManager.GetManager(providerName);
		}

		/// <summary>
		/// Gets the view model list.
		/// </summary>
		/// <param name="contentList">The content list.</param>
		/// <param name="dataProvider">The data provider.</param>
		/// <returns></returns>
		public override IEnumerable<ProjectItemViewModel> GetViewModelList(IEnumerable<ProjectItem> contentList, ContentDataProviderBase dataProvider)
		{
			var list = new List<ProjectItemViewModel>();

			foreach (var Project in contentList)
				list.Add(new ProjectItemViewModel(Project, dataProvider));

			return list;
		}
	}
}
