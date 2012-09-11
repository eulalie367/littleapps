using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;

namespace ProjectsModule.Model
{
	/// <summary>
	/// Project Data Item class
	/// </summary>
	[DataContract(Namespace = "http://sitefinity.com/samples/Projectmodule", Name = "ProjectItem")]
	[ManagerType("ProjectsModule.Data.ProjectsManager")]
	public class ProjectItem : Content, ILocatable
	{
		#region Overrides

		/// <summary>
		/// Indicate whether content lifecycle is supported or not
		/// </summary>
		public override bool SupportsContentLifecycle
		{
			get { return false; }
		}

		#endregion

		#region Project Properties

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>
		/// The content.
		/// </value>
		[DataMember]
		[MetadataMapping(true, true)]
		[UserFriendlyDataType(UserFriendlyDataType.LongText)]
		public Lstring Content
		{
			get
			{
				if (this.content == null)
					this.content = this.GetString("Content");
				return this.ApplyContentFilters(this.content);
			}
			set
			{
				this.content = value;
				this.SetString("Content", this.content);
			}
		}

		/// <summary>
		/// Project address
		/// </summary>
		[DataMember]
		public string Address
		{
			get { return this.address; }
			set { this.address = value; }
		}

		/// <summary>
		/// Project city
		/// </summary>
		[DataMember]
		public string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		/// <summary>
		/// Project region (state)
		/// </summary>
		[DataMember]
		public string Region
		{
			get { return this.region; }
			set { this.region = value; }
		}

		/// <summary>
		/// Project postal code
		/// </summary>
		[DataMember]
		public string PostalCode
		{
			get { return this.postalCode; }
			set { this.postalCode = value; }
		}

		/// <summary>
		/// Project region (state)
		/// </summary>
		[DataMember]
		public string Country
		{
			get { return this.country; }
			set { this.country = value; }
		}

		#endregion

		#region ILocatable

		/// <summary>
		/// Gets or sets a value indicating whether to auto generate an unique URL.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if to auto generate an unique URL otherwise, <c>false</c>.
		/// </value>
		[NonSerializableProperty]
		public bool AutoGenerateUniqueUrl
		{
			get { return false; }
		}

		/// <summary>
		/// Gets a collection of URL data for this item.
		/// </summary>
		/// <value> The collection of URL data.</value>
		[NonSerializableProperty]
		public virtual IList<ProjectItemUrlData> Urls
		{
			get
			{
				if (this.urls == null)
					this.urls = new ProviderTrackedList<ProjectItemUrlData>(this, "Urls");
				this.urls.SetCollectionParent(this);
				return this.urls;
			}
		}

		/// <summary>
		/// Gets a collection of URL data for this item.
		/// </summary>
		/// <value>The collection of URL data.</value>
		[NonSerializableProperty]
		IEnumerable<UrlData> ILocatable.Urls
		{
			get
			{
				return this.Urls.Cast<UrlData>();
			}
		}

		/// <summary>
		/// Clears the Urls collection for this item.
		/// </summary>
		/// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
		public void ClearUrls(bool excludeDefault)
		{
			if (this.urls != null)
				this.urls.ClearUrls(excludeDefault);
		}

		/// <summary>
		/// Removes all urls that satisfy the condition that is checked in the predicate function.
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition.</param>
		public void RemoveUrls(Func<UrlData, bool> predicate)
		{
			if (this.urls != null)
				this.urls.RemoveUrls(predicate);
		}

		#endregion

		#region Private Properties

		private Lstring content;
		private string address;
		private string city;
		private string region;
		private string postalCode;
		private string country;

		// ILocatable
		private ProviderTrackedList<ProjectItemUrlData> urls;

		#endregion
	}
}
