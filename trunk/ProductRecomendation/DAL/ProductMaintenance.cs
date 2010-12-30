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
    public class ProductTypeAttribute : ProductRecomendation.DAL.View_ProductTypeAttribute
    {
        public ProductTypeAttribute()
        { }
        public ProductTypeAttribute(int attributeID) : this()
        {
            this.AttributeID = attributeID;
            Fill();
        }

        private void Fill()
        {
            ProductTypeAttribute tmp = SqlHelper.FillEntity<ProductTypeAttribute>("SELECT * FROM view_ProductTypeAttribute WHERE AttributeID = @AttributeID", new SqlParameter[] { new SqlParameter("@AttributeID", this.AttributeID) }, CommandType.Text);
            if (tmp != null)
            {
                this.AttributeTypeID = tmp.AttributeTypeID;
                this.AttributeTypeName = tmp.AttributeTypeName;
                this.Name = tmp.Name;
                this.ParentAttributeID = tmp.ParentAttributeID;
                this.ProductTypeID = tmp.ProductTypeID;
            }
        }

        public static List<ProductTypeAttribute> GetAll(int productTypeID)
        {
            return SqlHelper.FillEntities<ProductTypeAttribute>("SELECT * FROM view_ProductTypeAttribute WHERE ProductTypeID = @ProductTypeID", new SqlParameter[] { new SqlParameter("@ProductTypeID", productTypeID) }, CommandType.Text);
        }

        internal static List<ProductTypeAttribute> GetAllChildren(int parentAttributeID)
        {
            return SqlHelper.FillEntities<ProductTypeAttribute>("SELECT * FROM view_ProductTypeAttribute WHERE ParentAttributeID = @ParentAttributeID", new SqlParameter[] { new SqlParameter("@ParentAttributeID", parentAttributeID) }, CommandType.Text);
        }

        internal void Save()
        {
            if (this.AttributeID > 0 && !string.IsNullOrEmpty(this.Name))
            {
                SqlParameter[] p = new SqlParameter[] 
                {
                    new SqlParameter("@Name", this.Name),
                    new SqlParameter("@AttributeTypeID", this.AttributeTypeID),
                    new SqlParameter("@ParentAttributeID", this.ParentAttributeID),
                    new SqlParameter("@AttributeID", this.AttributeID),
                };

                this.AttributeID = SqlHelper.FillEntity<int>("UPDATE Attribute SET Name = @Name, AttributeTypeID = @AttributeTypeID, ParentAttributeID = @ParentAttributeID WHERE AttributeID = @AttributeID SELECT SCOPE_IDENTITY()", p, CommandType.Text);
            }
        }
    }
    public class AttributeType : ProductRecomendation.DAL.AttributeType
    {
        internal static List<AttributeType> Getall()
        {
            return SqlHelper.FillEntities<AttributeType>("SELECT * FROM AttributeType");
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

        internal void Save()
        {
            if (this.ProductID > 0 && !string.IsNullOrEmpty(this.ProductName))
            {
                SqlParameter[] p = new SqlParameter[] 
                {
                    new SqlParameter("@ProductID", this.ProductID),
                    new SqlParameter("@ProductName", this.ProductName)
                };

                SqlHelper.ExecuteScalar("UPDATE Product SET Name = @ProductName WHERE ProductID = @ProductID SELECT SCOPE_IDENTITY()", p, CommandType.Text);
            }
        }

        internal void Add()
        {
            if (this.ProductTypeID > 0 && !string.IsNullOrEmpty(this.ProductName))
            {
                SqlParameter[] p = new SqlParameter[] 
                {
                    new SqlParameter("@ProductTypeID", this.ProductTypeID),
                    new SqlParameter("@ProductName", this.ProductName)
                };

                this.ProductID = SqlHelper.FillEntity<int>("INSERT INTO Product (Name, ProductTypeID) VALUES( @ProductName, @ProductTypeID ) SELECT SCOPE_IDENTITY()", p, CommandType.Text);
            }
        }
    }

}
