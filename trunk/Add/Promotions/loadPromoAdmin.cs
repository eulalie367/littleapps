#region

using System;
using System.Collections.Generic;
using System.Text;
using umbraco.BusinessLogic.Actions;
using umbraco.cms.presentation.Trees;
using umbraco.interfaces;
using Hershey.DataLayer;
using Hershey.DataLayer.Promotions;
using Spring.Context.Support;

#endregion

namespace UmbracoAddons.Promotions
{
    public class loadPromoAdmin : BaseTree
    {
        public PromotionService PromotionObject { get; set; }

        public loadPromoAdmin(string application)
            : base(application)
        {
            var appContext = ContextRegistry.GetContext();
            PromotionObject = appContext.GetObject("PromotionService") as PromotionService;
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
            if (string.IsNullOrEmpty(this.NodeKey))
            {
                Tree.Add(CreateAndAddNode("Active Promos", null, "ActivePromos", true));
                Tree.Add(CreateAndAddNode("Inactive Promos", null, "InactivePromos", true));
            }
            else if (this.NodeKey == "ActivePromos")
            {
                foreach (promotion_Promotion promo in PromotionObject.GetActivePromotions())
                {
                    Tree.Add(CreateAndAddNode(promo.Name, "javascript:editPromo('" + promo.Id.ToString() + "')", promo.Name, false));
                }
            }
            else if (this.NodeKey == "InactivePromos")
            {
                foreach (promotion_Promotion promo in PromotionObject.GetInactivePromotions())
                {
                    Tree.Add(CreateAndAddNode(promo.Name, "javascript:editPromo('" + promo.Id.ToString() + "')", promo.Name, false));
                }
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
                node.Source = this.GetTreeServiceUrl(strNodeId);
            }

            return node;
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
                @"

                function editPromo(promoID) {
                    parent.right.document.location.href = '/UmbracoAddons/Promotions/EditPromo.aspx?id='+promoID;
                }

			");
        }
    }
}