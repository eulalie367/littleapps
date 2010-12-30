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
    public class ProductTypeTree : BaseTree
    {
        ProductTree pt;
        ProductTypeAttributeTree ptat;
        public ProductTypeTree(string application)
            : base(application)
        {
            pt = new ProductTree(application);
            ptat = new ProductTypeAttributeTree(application);
        }

        protected override void CreateAllowedActions(ref List<IAction> actions)
        {
            actions.Clear();
            actions.Add(Actions.AddProductType.Instance);
            actions.Add(Actions.AddProduct.Instance);
            actions.Add(ContextMenuSeperator.Instance);
            //actions.Add(ActionNew.Instance);
            //actions.Add(ActionDelete.Instance);
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
            List<ProductType> allTypes = ProductType.GetAll();
            List<ProductType> types = ProductType.GetAll();

            if (this.id == this.StartNodeID)//parent types
            {
                types = allTypes.Where(t => !t.ParentProductTypeID.HasValue).OrderBy(t => t.Name).ToList();
            }
            else
            {
                types = allTypes.Where(t => t.ParentProductTypeID.HasValue && t.ParentProductTypeID.Value == this.id).OrderBy(t => t.Name).ToList();
            }

            string source = "";
            List<Product> products = null;

            List<ProductType> children = null;
            foreach (ProductType type in types)
            {
                children = allTypes.Where(t => t.ParentProductTypeID.HasValue && t.ParentProductTypeID.Value == type.ProductTypeID).ToList();
                products = Product.GetAll(type.ProductTypeID);

                if (products.Count > 0)
                    source = this.GetTreeServiceUrl(type.ProductTypeID);

                if (children.Count > 0)
                    source = this.GetTreeServiceUrl(type.ProductTypeID);

                CreateAndAddNode(ref Tree, type.Name, "javascript:openProductTypeMaintenance(" + type.ProductTypeID.ToString() + ")", type.ProductTypeID.ToString(), source);

                source = "";
            }

            source = "";

            List<ProductTypeAttribute> pta = ProductTypeAttribute.GetAll(this.id);
            if(pta.Count>0)
            {
                ptat.id = this.id;
                XmlTreeNode folder = XmlTreeNode.Create(ptat);
                folder.Text = "Attributes";
                folder.Action = "javascript:void(0);";
                folder.Icon = FolderIcon;
                folder.NodeID = ptat.id.ToString();
                folder.Source = ptat.GetTreeServiceUrl(ptat.id);

                OnBeforeNodeRender(ref Tree, ref folder, EventArgs.Empty);
                Tree.Add(folder);
                OnAfterNodeRender(ref Tree, ref folder, EventArgs.Empty);
            }

            products = Product.GetAll(this.id);
            if (products.Count > 0)
            {
                pt.id = this.id;
                pt.Render(ref Tree);
            }
        }

        private void CreateAndAddNode(ref XmlTree Tree, string text, string jsCommand, string nodeId, string source)
        {
            XmlTreeNode node = XmlTreeNode.Create(this);
            CreateAndAddNode(ref Tree, ref node, text, jsCommand, nodeId, source);
        }
        private void CreateAndAddNode(ref XmlTree Tree, ref XmlTreeNode node, string text, string jsCommand, string nodeId, string source)
        {
            node.Text = text;
            node.Action = jsCommand;
            node.Icon = FolderIcon;
            node.NodeID = nodeId;
            node.Source = source;

            OnBeforeNodeRender(ref Tree, ref node, EventArgs.Empty);
            Tree.Add(node);
            OnAfterNodeRender(ref Tree, ref node, EventArgs.Empty);

        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(@"
                function openProductTypeMaintenance(productTypeID) {
                    parent.right.document.location.href = ""/CustomTrees/ProductMaintenance/ProductTypeControl.aspx?pt="" + productTypeID ;
                }
			");
            pt.RenderJS(ref Javascript);
            ptat.RenderJS(ref Javascript);
        }
    }
    namespace Actions
    {
        public class AddProductType : IAction 
        {
            //create singleton
            private static readonly AddProductType m_instance = new AddProductType();
            private AddProductType() { }
            public static AddProductType Instance
            {
                get { return m_instance; }
            }
            #region IAction Members

            string IAction.Alias
            {
                get { return "newProductType"; }
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
                get { return "createProductType()"; }
            }

            string IAction.JsSource
            {
                get { return @"function createProductType(){ 
                                var actionNode = UmbClientMgr.mainTree().getActionNode();
                                UmbClientMgr.openModalWindow(""create.aspx?nodeId=""+ actionNode.nodeId 
                                + ""&nodeType=newProductType"" 
                                + ""&nodeName="" + actionNode.nodeName
                                + ""&rnd=75.4&rndo=84.8""
                                , ""Add a child type to "" + actionNode.nodeName, true, 420, 270) 
                            }"; }
            }
            char IAction.Letter
            {
                get { return ';'; }
            }

            bool IAction.ShowInNotifier
            {
                get { return ActionNew.Instance.ShowInNotifier; }
            }

            #endregion
        }
    }
}
