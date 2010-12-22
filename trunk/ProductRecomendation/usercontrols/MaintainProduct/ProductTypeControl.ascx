<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTypeControl.ascx.cs" Inherits="ProductRecomendation.usercontrols.MaintainProduct.ProductTypeControl" %>


<form runat="server">
<div class="productType">
    <h2>
        Product Type</h2>
    <ul>
        <li class="name">
            <asp:Label runat="server" Text="Name:" />
            <input type="text" runat="server" id="tbName" />
        </li>
        <li class="parent">
            <asp:Label runat="server" Text="Parent Type:" />
            <asp:DropDownList runat="server" ID="selParentType" />
        </li>
    </ul>
    <div class="submit">
        <input type="button" runat="server" id="btnSubmit" value="Add Product Type" />
    </div>
</div>
</form>



