using System;
using System.Collections.Generic;
using Telerik.Cms.Engine;
using Telerik.Cms.Engine.Security;
using Telerik.Security.Permissions;
using $rootnamespace$.ConfigurationHelpers;

namespace $rootnamespace$.$fileinputname$
{
    /// <summary>
    /// Manager for the $fileinputname$ module
    /// </summary>
    public class $fileinputname$Manager
    {
        
        #region Constructors

        static $fileinputname$Manager()
		{
            foreach (GenericContentElement element in ConfigHelper.Handler("telerik/$fileinputname$").GenericContentProviders)
                $fileinputname$Manager.contentSettings.Add(element.ProviderName, element);
		}

        public $fileinputname$Manager(string providerName)
		{
            if (string.IsNullOrEmpty(providerName))
                providerName = DefaultContentProvider;
			this.settingsElement = $fileinputname$Manager.contentSettings[providerName];

        }

        #endregion

        #region Properties

		public static Dictionary<String, ISecured> SecurityRoots
		{
			get
			{
				if (securityRoots == null)
				{
					securityRoots = new Dictionary<String, ISecured>(ContentManager.Providers.Count);
					foreach (string name in contentSettings.Keys)
						securityRoots.Add(name, new GlobalPermissions(name));
				}
				return securityRoots;
			}
        }

        public static string DefaultContentProvider
        {
            get
            {
                return ConfigHelper.Handler("telerik/$fileinputname$").DefaultContentProvider;
            }
        }

        public ContentManager Content
        {
            get
            {
                if (this.contentManager == null)
                    this.contentManager = new ContentManager(this.settingsElement.ProviderName);
                return this.contentManager;
            }
        }

        #endregion

        #region Security Members

        public GlobalPermissions Permissions
		{
			get
			{
                return (GlobalPermissions)$fileinputname$Manager.SecurityRoots[this.settingsElement.ProviderName];
			}
		}

		public GlobalPermission GetPermission()
		{
			return new GlobalPermission(this.Permissions);
		}

		public GlobalPermission GetPermission(int requestRights)
		{
			return new GlobalPermission(this.Permissions, requestRights);
		}

		public GlobalPermission GetPermission(Guid contentOwnerId)
		{
			IContent cnt = this.Content.GetCurrentState(contentOwnerId);
			return this.GetPermission(cnt);
		}

		public GlobalPermission GetPermission(Guid contentOwnerId, int requestRights)
		{
			IContent cnt = this.Content.GetCurrentState(contentOwnerId);
			return this.GetPermission(cnt, requestRights);
		}

		public GlobalPermission GetPermission(IContent contentOwner)
		{
			return new GlobalPermission(this.Permissions, contentOwner);
		}

		public GlobalPermission GetPermission(IContent contentOwner, int requestRights)
		{
			return new GlobalPermission(this.Permissions, requestRights, contentOwner);
		}

		#endregion

		private readonly GenericContentElement settingsElement;
		private ContentManager contentManager;
		private static Dictionary<String, ISecured> securityRoots;

		private static readonly IDictionary<string, GenericContentElement> contentSettings = new Dictionary<string, GenericContentElement>();
    }
}
