using System;
using Telerik.Cms.Engine.WebControls.Admin;
using $rootnamespace$.$fileinputname$.Resources;
using Telerik.Web;
namespace $rootnamespace$.$fileinputname$.ViewControls
{
    public partial class $fileinputname$Preview : ContentItemPreview<$fileinputname$View>
    {
        public override string Title
        {
            get
            {
                return Messages.$fileinputname$PreviewView_Title;
            }
            set
            {
                base.Title = value;
            }
        }

        #region View layout and template

        /// <summary>
        /// Gets or sets the path to a custom layout template for the control.
        /// </summary>
        /// <value></value>
        [EmbeddedTemplateAttribute($fileinputname$Preview.layoutTemplateName, "$fileinputname$_Preview_Template_Desc", "/$fileinputname$", false, "2009-04-10")]
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
        /// Gets the name of the embedded layout template. This property must be overridden to provide the path (key) to an embedded resource file.
        /// </summary>
        /// <value></value>
        protected override string LayoutTemplateName
        {
            get
            {
                return $fileinputname$Preview.layoutTemplateName;
            }
        }

        private const string layoutTemplateName =
            "$rootnamespace$.$fileinputname$.ViewControls.Preview.ascx";

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
        /// Gets the history command.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        protected override string GetHistoryCommand(Guid itemId)
        {
            return CreateHostViewCommand<$fileinputname$History>(itemId.ToString());
        }

        /// <summary>
        /// Gets the list command.
        /// </summary>
        /// <returns></returns>
        protected override string GetListCommand()
        {
            return CreateHostViewCommand<$fileinputname$ItemList>();
        }

        #endregion

    }
}