<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductAdd.ascx.cs"
    Inherits="ProductRecomendation.CustomTrees.ProductMaintenance.ProductAdd" %>
<div class="product">
    <h2>Product</h2>
    <div class="name">
        <asp:Label runat="server" Text="Name:" />
        <input type="text" id="tbName" runat="server" />
    </div>
    <div class="submit">
        <input type="button" runat="server" id="btnSubmit" value="Add Product" />
    </div>
</div>
