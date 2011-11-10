﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using ProjectsModule.Model;
using Telerik.OpenAccess;

namespace ProjectsModule.Data.OpenAccess
{
	public class ProjectsFluentMapping : OpenAccessFluentMappingBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectsFluentMapping"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ProjectsFluentMapping(IDatabaseMappingContext context) : base(context) { }

		/// <summary>
		/// Creates and returns a collection of OpenAccess mappings
		/// </summary>
		public override IList<MappingConfiguration> GetMapping()
		{
			// initialize and return mappings
			var mappings = new List<MappingConfiguration>();
			MapItem(mappings);
			MapUrlData(mappings);
			return mappings;
		}

		/// <summary>
		/// Maps the ProjectItem class.
		/// </summary>
		/// <param name="mappings">The ProjectItem class mappings.</param>
		private void MapItem(IList<MappingConfiguration> mappings)
		{
			// initialize mapping
			var itemMapping = new MappingConfiguration<ProjectItem>();
			itemMapping.HasProperty(p => p.Id).IsIdentity();
			itemMapping.MapType(p => new { }).ToTable("sf_Projects");

			// add properties
			itemMapping.HasProperty(p => p.Address);
			itemMapping.HasProperty(p => p.City);
			itemMapping.HasProperty(p => p.Region).IsNullable();
			itemMapping.HasProperty(p => p.PostalCode);
			itemMapping.HasProperty(p => p.Country);

			// map urls table association
			itemMapping.HasAssociation(p => p.Urls).WithOppositeMember("parent", "Parent").ToColumn("content_id").IsDependent().IsManaged();
			mappings.Add(itemMapping);
		}

		/// <summary>
		/// Maps the ProjectItemUrlData class
		/// </summary>
		/// <param name="mappings">The LocatoinItemUrlData class mappings.</param>
		private void MapUrlData(IList<MappingConfiguration> mappings)
		{
			// map the Url data type
			var urlDataMapping = new MappingConfiguration<ProjectItemUrlData>();
			urlDataMapping.MapType(p => new { }).Inheritance(InheritanceStrategy.Flat).ToTable("sf_url_data");
			mappings.Add(urlDataMapping);
		}
	}
}
