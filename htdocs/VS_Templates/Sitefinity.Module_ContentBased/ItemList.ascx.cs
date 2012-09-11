using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Cms.Engine.WebControls.Admin;
using Telerik.Web;

namespace $rootnamespace$.$fileinputname$.ViewControls
{
    public partial class $fileinputname$ItemList : ContentItemsList<$fileinputname$View>
    {
        #region View layout and template

        /// <summary>
        /// Gets or sets the path to a custom layout template for the control.
        /// </summary>
        /// <value></value>
        [EmbeddedTemplateAttribute($fileinputname$ItemList.layoutTemplateName, "$fileinputname$_ItemList_Template_Desc", "/$fileinputname$", false, "2009-04-10")]
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
                return $fileinputname$ItemList.layoutTemplateName;
            }
        }

        private const string layoutTemplateName =
            "$rootnamespace$.$fileinputname$.ViewControls.ItemList.ascx";

        #endregion

        #region Command overrides

        /* by overriding the commands we can make the base module behave differently, while
         * keeping all of its base functionality */

        /// <summary>
        /// Gets the item view URL.
        /// </summary>
        /// <value>The item view URL.</value>
        public override string ItemViewUrl
        {
            get
            {
                return CreateHostViewCommand<$fileinputname$Preview>("{#ID#}");
            }
        }

        /// <summary>
        /// Gets the item edit URL.
        /// </summary>
        /// <value>The item edit URL.</value>
        public override string ItemEditUrl
        {
            get
            {
                return CreateHostViewCommand<$fileinputname$Edit>("{#ID#}");
            }
        }

        /// <summary>
        /// Gets the new item command.
        /// </summary>
        /// <returns></returns>
        protected override string GetNewItemCommand()
        {
            return CreateHostViewCommand<$fileinputname$New>();
        }

        /// <summary>
        /// Gets the preview item command.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        protected override string GetPreviewItemCommand(Guid itemId)
        {
            return CreateHostViewCommand<$fileinputname$Preview>(itemId.ToString());
        }

        #endregion

        #region Security overrides

        /* by overriding the permissions methods, we can substitute the permissions for generic content
         * module with permissions for $fileinputname$ module, while we leave the base class to perform the
         * business logic based on these permissions */

        /// <summary>
        /// Checks the permission.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        protected override bool CheckPermission(int right)
        {
            return this.Host.$fileinputname$Manager.GetPermission(right).CheckDemand();
        }

        #endregion
    }
}