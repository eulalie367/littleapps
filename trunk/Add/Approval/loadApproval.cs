using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.cms.presentation.Trees;
using System.Text;
using Hershey.DataLayer.Recipes;

namespace UmbracoAddons
{
    public class loadApproval : BaseTree
    {
        public loadApproval(string application)
            : base(application)
        { }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.Icon = FolderIcon;
            rootNode.OpenIcon = FolderIconOpen;
            rootNode.NodeType = "init" + TreeAlias;
            rootNode.NodeID = "init";
        }

        /// 
        /// Override the render method to create the newsletter tree
        /// 
        /// 
        public override void Render(ref XmlTree Tree)
        {

            RecipeService rs = new RecipeService();

            foreach (Recipe re in rs.GetAllRecipes().OrderBy(r => r.RecipeName))
            {
                XmlTreeNode node = XmlTreeNode.Create(this);
                node.Text = re.RecipeName;

                if(re.active.HasValue && re.active.Value == 'Y')
                    node.Icon = "green_light.png";
                else
                    node.Icon = "red_light.png";

                node.Action = string.Format("javascript:openApproveRecipe({0})", re.recipe_ID);
                // Add the node to the tree
                Tree.Add(node);
 
            }
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(@"
                function openApproveRecipe(recipeID) {
                    parent.right.document.location.href = '/UmbracoAddons/Approval/ApproveRecipe.aspx?ID=' + recipeID;
                }
			");

        }
    }
}
