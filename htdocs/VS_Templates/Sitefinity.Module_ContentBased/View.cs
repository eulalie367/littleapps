using Telerik.Cms.Engine;
using Telerik.Cms.Web.UI;
using Telerik.Cms.Engine.WebControls.Admin;
using $rootnamespace$.$fileinputname$.Resources;
using $rootnamespace$.$fileinputname$.ViewControls;


namespace $rootnamespace$.$fileinputname$
{
    public class $fileinputname$View : ViewModeControl<$fileinputname$ControlPanel>, IGenericContentHost
    {
        /// <summary>
        /// Content Manager used by the control
        /// </summary>
        public ContentManager Manager
        {
            get
            {
                return this.Host.Manager;
            }
        }

        /// <summary>
        /// Gets the $fileinputname$ manager.
        /// </summary>
        /// <value>The $fileinputname$ manager.</value>
        public $fileinputname$Manager $fileinputname$Manager
        {
            get
            {
                return this.Host.$fileinputname$Manager;
            }
        }

        /// <summary>
        /// Loads configured views.
        /// </summary>
        protected override void CreateViews()
        {
            AddView<$fileinputname$ItemList>("$fileinputname$ItemList", "$fileinputname$ItemListView_Title", "$fileinputname$ItemListView_Description", null, Messages.ResourceManager);
            AddView<$fileinputname$Edit>("$fileinputname$Edit", "$fileinputname$EditView_Title", "$fileinputname$EditView_Description", null, Messages.ResourceManager);
            AddView<$fileinputname$History>("$fileinputname$History", "$fileinputname$HistoryView_Title", "$fileinputname$HistoryView_Description", null, Messages.ResourceManager);
            AddView<$fileinputname$New>("$fileinputname$New", "$fileinputname$NewView_Title", "$fileinputname$NewView_Description", null, Messages.ResourceManager);
            AddView<$fileinputname$Preview>("$fileinputname$Preview", "$fileinputname$PreviewView_Title", "$fileinputname$PreviewView_Description", null, Messages.ResourceManager);
        }
    }
}
