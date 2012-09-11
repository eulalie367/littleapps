using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Data.OA;

namespace ProjectsModule.Data.OpenAccess
{
	class ProjectsFluentMetadataSource : ContentBaseMetadataSource
	{
		public ProjectsFluentMetadataSource() : base(null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectsFluentMetadataSource"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ProjectsFluentMetadataSource(IDatabaseMappingContext context) : base(context) { }

		/// <summary>
		/// Builds the custom mappings for the data provider.
		/// </summary>
		/// <returns></returns>
		protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
		{
			var sitefinityMappings = base.BuildCustomMappings();
			sitefinityMappings.Add(new ProjectsFluentMapping(this.Context));
			return sitefinityMappings;
		}
	}
}
