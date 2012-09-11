using System;
using Telerik.Cms.Engine.WebControls.Admin;
using Telerik.Web;

namespace $rootnamespace$.$fileinputname$.ViewControls
{
    public partial class $fileinputname$History : ContentItemHistory<$fileinputname$View>
    {
        #region View layout and template

        /// <summary>
        /// Gets or sets the path to a custom layout template for the control.
        /// </summary>
        /// <value></value>
        [EmbeddedTemplateAttribute($fileinputname$History.layoutTemplateName, "$fileinputname$_History_Template_Desc", "/$fileinputname$", false, "2009-04-10")]
        public override string LayoutTemplatePath
        {
            get
            {
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        protected override string LayoutTemplateName
        {
            get
            {
                return $fileinputname$History.layoutTemplateName;
            }
        }

        private const string layoutTemplateName =
            "$rootnamespace$.$fileinputname$.ViewControls.History.ascx";

        #endregion

        #region Command overrides

        /* by overriding the commands we can make the base module behave differently, while
         * keeping all of its base functionality */

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        protected override string GetEditCommand(Guid itemId)
        {
            return CreateHostViewCommand<$fileinputname$Edit>(itemId.ToString());
        }

        /// <summary>
        /// Gets the list command.
        /// </summary>
        /// <returns></returns>
        protected override string GetListCommand()
        {
            return CreateHostViewCommand<$fileinputname$ItemList>();
        }

        /// <summary>
        /// Gets the preview command.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        protected override string GetPreviewCommand(Guid itemId)
        {
            return CreateHostViewCommand<$fileinputname$Preview>(itemId.ToString());
        }

        #endregion
    }
}