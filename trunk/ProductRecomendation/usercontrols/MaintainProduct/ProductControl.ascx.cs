using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ProductRecomendation.ProductMaintenance;
using System.Collections.Generic;
using System.Text;
using umbraco.cms.presentation.Trees;

namespace ProductRecomendation.usercontrols.MaintainProduct
{
    public partial class ProductControl : System.Web.UI.UserControl
    {
        private int? _productID;
        public int? ProductID
        {
            get
            {
                if (_productID.HasValue)
                    return _productID.Value;

                _productID = Request["p"].ToInt();
                return _productID;
            }
            set
            {
                _productID = value;
            }
        }
        protected Product product;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ProductID.HasValue)
            {
                product = new Product(ProductID.Value);
                productName.InnerText = product.ProductName;
                productTypeName.InnerText = product.ProductTypeName;

                dvAttributes.InnerHtml = FillAttributes(product.Attributes);
            }
        }
        private List<int> usedAttribs = new List<int>();
        private string FillAttributes(List<ProductRecomendation.DAL.View_ProductAttribute> attributes)
        {
            string html = "<ul>";
            foreach (var attrib in attributes.Where(at => !usedAttribs.Contains(at.AttributeID)))
            {
                usedAttribs.Add(attrib.AttributeID);
                html += "<li>";
                html += attrib.Name;
                html += FillAttributes(product.Attributes.Where(at => at.ParentAttributeID == attrib.AttributeID).ToList());
                html += "</li>";
            }
            html += "</ul>";
            return html;
        }

    }
    public class ProductTypeControl_Initializer : BaseTree
    {
        public ProductTypeControl_Initializer(string application) : base(application)
        {
        }

        protected override void CreateAllowedActions(ref List<umbraco.interfaces.IAction> actions)
        {
            actions.Clear();
        }
        protected override void CreateRootNodeActions(ref List<umbraco.interfaces.IAction> actions)
        {
            base.CreateRootNodeActions(ref actions);
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

                CreateAndAddNode(ref Tree, type.Name, "javascript:void(0)", type.ProductTypeID.ToString(), source);

                source = "";
            }

            source = "";
            products = Product.GetAll(this.id);

            foreach (Product p in products)
                CreateAndAddNode(ref Tree, p.ProductName, "javascript:void(0)", p.ProductID.ToString(), source);
        }

        private void CreateAndAddNode(ref XmlTree Tree, string text, string jsCommand, string nodeId, string source)
        {
            XmlTreeNode node = XmlTreeNode.Create(this);

            node.Text = text;
            node.Action = jsCommand;
            node.Icon = "doc.gif";
            node.IconClass = "newComments";
            node.NodeID = nodeId;
            node.Source = source;

            OnBeforeNodeRender(ref Tree, ref node, EventArgs.Empty);
            Tree.Add(node);
            OnAfterNodeRender(ref Tree, ref node, EventArgs.Empty);

        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(@"
                function openSendNewsletter() {
                    parent.right.document.location.href = 'newsletter/sendNewsletter.aspx';
                }
			");

            Javascript.Append(@"
                function openPreviousNewsletters() {
                    parent.right.document.location.href = 'newsletter/previousNewsletters.aspx';
                }
			");
        }
    }
}