#region

using System;
using System.Collections.Generic;
using System.Text;
using umbraco.BusinessLogic.Actions;
using umbraco.cms.presentation.Trees;
using umbraco.interfaces;

#endregion

namespace UmbracoAddons.Moderation.Recipes
{
    public class loadRecipeModeration : BaseTree
    {
        public loadRecipeModeration(string application)
            : base(application)
        {
        }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.Icon = FolderIcon;
            rootNode.OpenIcon = FolderIconOpen;
            rootNode.NodeType = "init" + TreeAlias;
            rootNode.NodeID = "init";
        }

        protected override void CreateRootNodeActions(ref List<IAction> actions)
        {
            actions.Clear();
            actions.Add(ActionRefresh.Instance);
        }

        protected override void CreateAllowedActions(ref List<IAction> actions)
        {
            actions.Clear();
            actions.Add(ActionRefresh.Instance);
        }

        public override void Render(ref XmlTree Tree)
        {
            CreateAndAddNode(ref Tree, "Baking Tips", "javascript:moderateBakingTips()", "moderateBakingTips");
            CreateAndAddNode(ref Tree, "RecipeReviews", "javascript:moderateRecipeReviews()", "moderateRecipeReviews");
        }

        private void CreateAndAddNode(ref XmlTree Tree, string text, string jsCommand, string nodeId)
        {
            XmlTreeNode node = XmlTreeNode.Create(this);

            node.Text = text;
            node.Action = jsCommand;
            node.Icon = "doc.gif";
            node.IconClass = "newComments";
            node.NodeID = nodeId;

            OnBeforeNodeRender(ref Tree, ref node, EventArgs.Empty);
            Tree.Add(node);
            OnAfterNodeRender(ref Tree, ref node, EventArgs.Empty);

        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
                @"
                function moderateRecipeReviews() {
                    parent.right.document.location.href = '/UmbracoAddons/Moderation/Recipes/ModerateRecipeReviews.aspx';
                }
			");

            Javascript.Append(
                @"
                function moderateBakingTips() {
                    parent.right.document.location.href = '/UmbracoAddons/Moderation/Recipes/BakingTips.aspx';
                }
			");
        }
    }
}