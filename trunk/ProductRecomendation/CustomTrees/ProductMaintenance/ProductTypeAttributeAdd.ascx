<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTypeAttributeAdd.ascx.cs" Inherits="ProductRecomendation.CustomTrees.ProductMaintenance.ProductTypeAttributeAdd" %>


<div class="productTypeAttribute">
    <h2>Product Type Attribute</h2>
    <div class="name">
        <asp:Label runat="server" Text="Name:" />
        <input type="text" id="tbName" runat="server" />
    </div>
    <div class="submit">
        <input type="button" runat="server" id="btnSubmit" value="Add Product" />
    </div>
</div>


