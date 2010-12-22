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
    public class ProductControl_Initializer : umbraco.cms.presentation.Trees.BaseTree
    {
        public ProductControl_Initializer(string application)
            : base(application)
        { }

        protected override void CreateRootNode(ref umbraco.cms.presentation.Trees.XmlTreeNode rootNode)
        {
            rootNode.Icon = FolderIcon;
            rootNode.OpenIcon = FolderIconOpen;
            rootNode.NodeType = "init" + TreeAlias;
            rootNode.NodeID = "init";
        }

        public override void Render(ref umbraco.cms.presentation.Trees.XmlTree Tree)
        {
            List<ProductType> types = ProductType.GetAll();

            foreach (ProductType type in types)
            {
                var tn = umbraco.cms.presentation.Trees.XmlTreeNode.Create(this);
                tn.Text = type.Name;
                tn.Icon = FolderIcon;
                tn.Source = this.GetTreeServiceUrl();
                tn.Action = "javascript:openSendNewsletter()";
                // Add the node to the tree
                Tree.Add(tn);
            }
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
        }    }
}