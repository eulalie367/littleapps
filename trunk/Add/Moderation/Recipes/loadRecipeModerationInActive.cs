#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hershey.DataLayer.Recipes;
using umbraco.BusinessLogic.Actions;
using umbraco.cms.presentation.Trees;
using umbraco.interfaces;

#endregion

namespace UmbracoAddons.Moderation.Recipes
{
    public class loadRecipeModerationInActive : BaseTree
    {
        public loadRecipeModerationInActive(string application)
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
            var rs = new RecipeService();
            List<Recipe> recipes = rs.GetAllRecipes().ToList();


            foreach (
                Recipe re in recipes.Where(r => !r.active.HasValue || r.active.Value == 'N').OrderBy(r => r.RecipeName))
            {
                XmlTreeNode nd = XmlTreeNode.Create(this);
                nd.Text = re.RecipeName;
                nd.Action = string.Format("javascript:openApproveRecipe({0})", re.recipe_ID);
                //nd.NodeID = "unapprovedRecipe_" + re.recipe_ID.ToString();
                nd.Icon = "doc.gif";
                nd.IconClass = "file";

                OnBeforeNodeRender(ref Tree, ref nd, EventArgs.Empty);
                if (nd != null)
                {
                    Tree.Add(nd);
                }
                OnAfterNodeRender(ref Tree, ref nd, EventArgs.Empty);
            }
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
                @"
                function openApproveRecipe(recipeID) {
                    parent.right.document.location.href = '/UmbracoAddons/Moderation/Recipes/ApproveRecipe.aspx?ID=' + recipeID;
                }
			");
        }
    }
}