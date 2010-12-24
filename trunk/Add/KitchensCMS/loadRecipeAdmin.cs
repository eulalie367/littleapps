#region

using System.Collections.Generic;
using System.Text;
using umbraco.BusinessLogic.Actions;
using umbraco.cms.presentation.Trees;
using umbraco.interfaces;

#endregion

namespace UmbracoAddons.Recipes
{
    public class loadRecipeAdmin : BaseTree
    {
        public loadRecipeAdmin(string application)
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
            if (string.IsNullOrEmpty(NodeKey))
            {
                Tree.Add(CreateAndAddNode("Hershey's Recipes", null, "recipes", true));
                Tree.Add(CreateAndAddNode("Cookie Exchange Recipes", null, "cookies", true));
            }
            else if (NodeKey == "recipes")
            {
                Tree.Add(CreateAndAddNode("All Recipes", "javascript:recipeList()", "recipeslist", false));
                Tree.Add(CreateAndAddNode("My Recipes", "javascript:myRecipes()", "my", false));
                Tree.Add(CreateAndAddNode("Homepage Recipes", "javascript:homepageRecipes()", "homepage", false));
                Tree.Add(CreateAndAddNode("Inactive Recipes", "javascript:inActiveRecipes()", "inactive", false));
                Tree.Add(CreateAndAddNode("Add New Recipe", "javascript:addRecipe()", "new", false));
                Tree.Add(CreateAndAddNode("Manage Products", "javascript:recipeProducts()", "products", false));
                //Tree.Add(CreateAndAddNode("Add New Product", "javascript:recipeProducts()", "products", false));
                Tree.Add(CreateAndAddNode("Category Management", "javascript:recipeCategories()", "categories", false));
                Tree.Add(CreateAndAddNode("Baking Tips", "javascript:bakingTips()", "tips", false));
                Tree.Add(CreateAndAddNode("Submitted Recipes", "javascript:submittedRecipes()", "submitted", false));
            }
            else if (NodeKey == "cookies")
            {
                // Add Cookie Exchange Node:
                Tree.Add(CreateAndAddNode("Cookie Exchange Recipes", "", "cookies", false));
                Tree.Add(CreateAndAddNode("Cookie Exchange Images", "", "images", false));
            }
        }

        private XmlTreeNode CreateAndAddNode(string strNodeName, string jsCommand, string strNodeId, bool bHasChildren)
        {
            XmlTreeNode node = XmlTreeNode.Create(this);

            node.Text = strNodeName;
            node.Action = jsCommand;
            node.Icon = bHasChildren ? "folder.gif" : "doc.gif";
            node.IconClass = "newComments";
            node.NodeID = strNodeId;
            if (bHasChildren)
            {
                node.HasChildren = true;
                node.Source = GetTreeServiceUrl(strNodeId);
            }

            return node;
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
                @"

                function recipeList() {
                    parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/RecipeList.aspx';
                }

                function myRecipes() {
                    parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/RecipeList.aspx?myrecipes=1';
                }

                function homepageRecipes() {
                    parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/RecipeList.aspx?homepage=1';
                }

                function submittedRecipes() {
                    parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/SubmittedRecipes.aspx';
                }

                function inActiveRecipes() {
                    parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/RecipeList.aspx?active=0';
                }

                function bakingTips() {
                    parent.right.document.location.href = '/UmbracoAddons/Moderation/Recipes/BakingTips.aspx';
                }

                function addRecipe() {
                    parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/Recipe.aspx';
                }

                function recipeCategories() {
                   parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/CategoryManagement.aspx';
                }

                function recipeProducts() {
                   parent.right.document.location.href = '/UmbracoAddons/KitchensCMS/RecipeProductManagement.aspx';
                }




			");
        }
    }
}