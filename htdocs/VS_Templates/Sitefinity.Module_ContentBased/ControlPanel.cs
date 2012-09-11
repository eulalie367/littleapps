using Telerik.Cms.Engine;
using Telerik.Cms.Engine.WebControls.Admin;
using Telerik.Cms.Web.UI.Backend;
using $rootnamespace$.$fileinputname$.Resources;

namespace $rootnamespace$.$fileinputname$
{
    public class $fileinputname$ControlPanel : ProviderControlPanel<$fileinputname$ControlPanel>, IGenericContentHost
    {
        /// <summary>
        /// Gets or sets the title of the panel containing the data provider selector.
        /// Usually this is the first command panel on the top right side of the control panel.
        /// </summary>
        /// <value></value>
        public override string ProviderSelectorPanelTitle
        {
            get
            {
                return "Explore $fileinputname$";
            }
            set
            {
                base.ProviderSelectorPanelTitle = value;
            }
        }

        /// <summary>
        /// Loads configured views.
        /// </summary>
        protected override void CreateViews()
        {
            AddView<$fileinputname$View>("$fileinputname$View", "$fileinputname$View_Title", "$fileinputname$View_Description", "all", Messages.ResourceManager);
            AddView<CategoriesView<$fileinputname$ControlPanel>>("$fileinputname$CategoriesView", "CategoriesView_Title", "CategoriesView_Description", "all", Messages.ResourceManager);
            AddView<TagsView<$fileinputname$ControlPanel>>("$fileinputname$TagsView", "TagsView_Title", "TagsView_Description", "all", Messages.ResourceManager);
            AddView<PermissionsView<$fileinputname$ControlPanel>>("$fileinputname$PermissionsView", "PermissionsView_Title", "PermissionsView_Description", "globalPerm", Messages.ResourceManager);
        }

        /// <summary>
        /// Content Manager used by the control
        /// </summary>
        public ContentManager Manager
        {
            get
            {
                return this.$fileinputname$Manager.Content;
            }
        }


        private $fileinputname$Manager _$fileinputname$Manager;
        /// <summary>
        /// Gets the $fileinputname$ manager.
        /// </summary>
        /// <value>The $fileinputname$ manager.</value>
        public $fileinputname$Manager $fileinputname$Manager
        {
            get
            {
                if (this._$fileinputname$Manager == null)
                    this._$fileinputname$Manager = new $fileinputname$Manager(this.ProviderName);
                return this._$fileinputname$Manager;
            }
        }

        /// <summary>
        /// When overridden this method returns the name of the default data provider for the module.
        /// Default data provider is used when no current provider is selected.
        /// The default implementation returns empty string.
        /// </summary>
        /// <returns>
        /// A string representing the name of the default data provider for the module.
        /// </returns>
        protected override string GetDefaultProviderName()
        {
            return $fileinputname$Manager.DefaultContentProvider;
        }

    }
}
