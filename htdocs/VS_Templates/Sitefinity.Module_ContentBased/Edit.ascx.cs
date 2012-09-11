using System;
using Telerik.Cms.Engine.WebControls.Admin;
using Telerik.Web;

namespace $rootnamespace$.$fileinputname$.ViewControls
{
    public partial class $fileinputname$Edit : ContentItemEdit<$fileinputname$View>
    {
        #region View layout and template

        /// <summary>
        /// Gets or sets the path to a custom layout template for the control.
        /// </summary>
        /// <value></value>
        [EmbeddedTemplateAttribute($fileinputname$Edit.layoutTemplateName, "$fileinputname$_Edit_Template_Desc", "/$fileinputname$", false, "2009-04-10")]
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
                return $fileinputname$Edit.layoutTemplateName;
            }
        }

        private const string layoutTemplateName =
            "$rootnamespace$.$fileinputname$.ViewControls.Edit.ascx";

        #endregion

        #region Command overrides

        /* by overriding the commands we can make the base module behave differently, while
         * keeping all of its base functionality */

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

        #region Security overrides

        /* by overriding the permissions methods, we can substitute the permissions for generic content
         * module with permissions for $fileinputname$ module, while we leave the base class to perform the
         * business logic based on these permissions */

        /// <summary>
        /// Checks the permission.
        /// </summary>
        /// <param name="contentOwnerId">The content owner id.</param>
        /// <param name="requestRights">The request rights.</param>
        /// <returns></returns>
        public override bool CheckPermission(Guid contentOwnerId, int requestRights)
        {
            return this.Host.$fileinputname$Manager.GetPermission(contentOwnerId, requestRights).CheckDemand();
        }

        /// <summary>
        /// Checks the permission.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <param name="currState">State of the curr.</param>
        /// <returns></returns>
        protected override bool CheckPermission(int right, Telerik.Cms.Engine.IContent currState)
        {
            return this.Host.$fileinputname$Manager.GetPermission(currState, right).CheckDemand();
        }

        /// <summary>
        /// Demands the permission.
        /// </summary>
        /// <param name="right">The right.</param>
        protected override void DemandPermission(int right)
        {
            this.Host.$fileinputname$Manager.GetPermission(right).Demand();
        }

        /// <summary>
        /// Demands the permission.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <param name="currState">State of the curr.</param>
        protected override void DemandPermission(int right, Telerik.Cms.Engine.IContent currState)
        {
            this.Host.$fileinputname$Manager.GetPermission(currState, right).Demand();
        }

        #endregion
    }
}