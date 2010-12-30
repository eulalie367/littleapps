<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTypeAdd.ascx.cs" Inherits="ProductRecomendation.CustomTrees.ProductMaintenance.ProductTypeAdd" %>
<div class="productType">
    <h2>Product Type</h2>
    <ul>
        <li class="name">
            <asp:Label runat="server" Text="Name:" />
            <input type="text" runat="server" id="tbName" />
        </li>
    </ul>
    <div class="submit">
        <input type="button" runat="server" id="btnSubmit" value="Add Product Type" />
    </div>
</div>
