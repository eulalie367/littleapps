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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProductRecomendation.ProductMaintenance
{
    public class ProductType : ProductRecomendation.DAL.ProductType
    {
        public ProductType():base()
        {
        }
        public ProductType(int ID) : this()
        {
            ProductType pt = GetAll().Where(p => p.ProductTypeID == ID).FirstOrDefault();
            this.ProductTypeID = pt.ProductTypeID;
            this.ParentProductTypeID = pt.ParentProductTypeID;
            this.Name = pt.Name;
        }
        public static List<ProductType> GetAll()
        {
            return SqlHelper.FillEntities<ProductType>("SELECT Name, ProductTypeId, ParentProductTypeId FROM ProductType");
        }
        public void Save()
        {
            SqlHelper.ExecuteNonQuery("UPDATE ProductType SET Name = @Name, ParentProductTypeID = @ParentProductTypeID WHERE ProductTypeID = @ProductTypeID",
                new SqlParameter[] 
                    { 
                        new SqlParameter("@Name", this.Name), 
                        new SqlParameter("@ParentProductTypeID", this.ParentProductTypeID),
                        new SqlParameter("@ProductTypeID", this.ProductTypeID) 
                    },
                CommandType.Text);
        }
        public void Insert()
        {
            if (this.ParentProductTypeID.HasValue)
            {
                this.ProductTypeID = SqlHelper.FillEntity<int>("INSERT INTO ProductType(Name, ParentProductTypeID) VALUES(@Name, @ParentProductTypeID) SELECT Scope_Identity()",
                    new SqlParameter[] { new SqlParameter("@Name", this.Name), new SqlParameter("@ParentProductTypeID", this.ParentProductTypeID) },
                    CommandType.Text);
            }
            else
            {
                this.ProductTypeID = SqlHelper.FillEntity<int>("INSERT INTO ProductType(Name) VALUES(@Name) SELECT Scope_Identity()",
                    new SqlParameter[] { new SqlParameter("@Name", this.Name) },
                    CommandType.Text);
            }
        }
    }
    public class Product : ProductRecomendation.DAL.View_Product
    {
        public List<ProductRecomendation.DAL.View_ProductAttribute> Attributes { get; set; }
        public Product() : base()
        {
            this.Attributes = new List<ProductRecomendation.DAL.View_ProductAttribute>();
        }
        public Product(int productID) : this()
        {
            this.ProductID = productID;
            Fill();
        }

        private void Fill()
        {
            ProductRecomendation.DAL.View_Product tmpProduct = SqlHelper.FillEntity<ProductRecomendation.DAL.View_Product>("SELECT * FROM view_Product WHERE ProductID = @ProductID", new SqlParameter[] { new SqlParameter("@ProductID", this.ProductID) }, CommandType.Text);
            this.ProductID = tmpProduct.ProductID;
            this.ProductName = tmpProduct.ProductName;
            this.ProductTypeID = tmpProduct.ProductTypeID;
            this.ProductTypeName = tmpProduct.ProductTypeName;

            this.Attributes = SqlHelper.FillEntities<ProductRecomendation.DAL.View_ProductAttribute>("SELECT * FROM view_ProductAttribute WHERE ProductTypeID = @ProductTypeID", new SqlParameter[] { new SqlParameter("@ProductTypeID", this.ProductTypeID) }, CommandType.Text);
        }
        public static List<ProductRecomendation.ProductMaintenance.Product> GetAll(int productTypeID)
        {
            List<ProductRecomendation.ProductMaintenance.Product> p = null;

            p = SqlHelper.FillEntities<Product>("SELECT ProductID, ProductTypeID, Name AS ProductName FROM Product WHERE ProductTypeID = @ProductType", new SqlParameter[] { new SqlParameter("@ProductType", productTypeID) }, CommandType.Text);

            return p;
        }
    }

}
