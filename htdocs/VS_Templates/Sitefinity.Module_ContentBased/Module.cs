using System.Collections.Generic;
using System.Web.UI;
using Telerik.Security.Permissions;
using Telerik.Web;
using $rootnamespace$.$fileinputname$.Resources;
using Telerik.Cms.Engine;

namespace $rootnamespace$.$fileinputname$
{
    /// <summary>
    /// Main module class for the $fileinputname$ module
    /// </summary>
    public class $fileinputname$Module : SecuredModule
    {
        public override string Name
        {
            get
            {
                return "$fileinputname$";
            }
        }

        public override string Title
        {
            get
            {
                return "$fileinputname$";
            }
        }

        public override string Description
        {
            get
            {
                return "Module for managing $fileinputname$";
            }
        }

        public override IList<IToolboxItem> Controls
        {
            get
            {
                return new List<IToolboxItem> { new ToolboxItem { DisplayName = Messages.$fileinputname$PublicView_DisplayName, Description = Messages.$fileinputname$PublicView_Description } };
            }
        }

        public override Control CreateControlPanel(TemplateControl parent)
        {
            return new $fileinputname$ControlPanel();
        }

        public override ISecured SecurityRoot
        {
            get
            {
                return this.SecurityRoots[ConfigurationHelpers.ConfigHelper.Handler("telerik/$fileinputname$").DefaultProvider];
            }
        }

        public override IDictionary<string, ISecured> SecurityRoots
        {
            get
            {
                return $rootnamespace$.$fileinputname$.$fileinputname$Manager.SecurityRoots;
            }
        }
    }
}
