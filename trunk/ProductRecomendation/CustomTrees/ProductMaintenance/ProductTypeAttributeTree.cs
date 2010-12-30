using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using umbraco.cms.presentation.Trees;
using umbraco.BusinessLogic.Actions;
using ProductRecomendation.ProductMaintenance;
using System.Collections.Generic;
using umbraco.interfaces;
using System.Text;

namespace ProductRecomendation.CustomTrees.ProductMaintenance
{
    public class ProductTypeAttributeTree : BaseTree
    {
        public ProductTypeAttributeTree(string application)
            : base(application)
        {
        }

        protected override void CreateAllowedActions(ref List<IAction> actions)
        {
            actions.Clear();
            //actions.Add(Actions.AddProduct.Instance);
            ////actions.Add(ActionDelete.Instance);
            //actions.Add(ContextMenuSeperator.Instance);
            actions.Add(ActionRefresh.Instance);
        }

        protected override void CreateRootNodeActions(ref List<umbraco.interfaces.IAction> actions)
        {
            actions.Clear();
            actions.Add(ActionNew.Instance);
            //actions.Add(ActionDelete.Instance);
            actions.Add(ContextMenuSeperator.Instance);
            actions.Add(ActionRefresh.Instance);
        }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.Icon = FolderIcon;
            rootNode.OpenIcon = FolderIconOpen;
            rootNode.NodeType = "init" + TreeAlias;
            rootNode.NodeID = "init";
        }

        public override void Render(ref XmlTree Tree)
        {
            List<ProductTypeAttribute> attribs = new List<ProductTypeAttribute>();
            if (!this.NodeKey.Contains("parentAttribute"))
            {
                attribs = ProductTypeAttribute.GetAll(this.id);
                attribs = attribs.Where(at => !at.ParentAttributeID.HasValue).ToList();
            }
            else
            {
                int? id = this.NodeKey.Replace("parentAttribute_", "").ToInt();
                this.id = id.HasValue ? id.Value : -1;
                attribs = ProductTypeAttribute.GetAllChildren(this.id);
            }

            foreach (ProductTypeAttribute a in attribs)
            {
                XmlTreeNode node = XmlTreeNode.Create(this);
                node.Text = a.Name;
                node.Action = "javascript:openProductTypeAttributeMaintenance(" + a.AttributeID.ToString() + ")";
                node.Icon = "doc.gif";
                node.NodeID = a.AttributeID.ToString();
                node.Source = this.GetTreeServiceUrl("parentAttribute_" + a.AttributeID.ToString());

                OnBeforeNodeRender(ref Tree, ref node, EventArgs.Empty);
                Tree.Add(node);
                OnAfterNodeRender(ref Tree, ref node, EventArgs.Empty);
            }
        }


        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(@"
                function openProductTypeAttributeMaintenance(attributeID) {
                    parent.right.document.location.href = ""/CustomTrees/ProductMaintenance/AttributeControl.aspx?a="" + attributeID;
                }
			");
        }
    }
    namespace Actions
    {
        public class AddProductTypeAttribute : IAction
        {
            //create singleton
            private static readonly AddProductTypeAttribute m_instance = new AddProductTypeAttribute();
            private AddProductTypeAttribute() { }
            public static AddProductTypeAttribute Instance
            {
                get { return m_instance; }
            }
            #region IAction Members

            string IAction.Alias
            {
                get { return "newProduct"; }
            }

            bool IAction.CanBePermissionAssigned
            {
                get { return ActionNew.Instance.CanBePermissionAssigned; }
            }

            string IAction.Icon
            {
                get { return ActionNew.Instance.Icon; }
            }

            string IAction.JsFunctionName
            {
                get { return "createProduct()"; }
            }

            string IAction.JsSource
            {
                get
                {
                    return @"function createProduct(){ 
                                var actionNode = UmbClientMgr.mainTree().getActionNode();
                                UmbClientMgr.openModalWindow(""create.aspx?nodeId=""+ actionNode.nodeId 
                                + ""&nodeType=newProduct"" 
                                + ""&nodeName="" + actionNode.nodeName
                                + ""&rnd=75.4&rndo=84.8""
                                , ""Add a product to "" + actionNode.nodeName, true, 420, 270) 
                            }";
                }
            }
            char IAction.Letter
            {
                get { return '.'; }
            }

            bool IAction.ShowInNotifier
            {
                get { return ActionNew.Instance.ShowInNotifier; }
            }

            #endregion
        }
    }
}
