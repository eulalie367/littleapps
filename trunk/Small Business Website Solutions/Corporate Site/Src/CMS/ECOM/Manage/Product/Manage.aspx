<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="SBWS.ECOM.Manage.Product.Manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="productid">
            <asp:Label ID="lblProductID" runat="server" Text="ProductID" AssociatedControlID="txtProductID"></asp:Label>
            <asp:TextBox ID="txtProductID" runat="server"></asp:TextBox>
        </div>
        <div class="categoryid">
            <asp:Label ID="lblCategoryID" runat="server" Text="CategoryID" AssociatedControlID="txtCategoryID"></asp:Label>
            <asp:TextBox ID="txtCategoryID" runat="server"></asp:TextBox>
        </div>
        <div class="unittypeid">
            <asp:Label ID="lblUnitTypeID" runat="server" Text="UnitTypeID" AssociatedControlID="txtUnitTypeID"></asp:Label>
            <asp:TextBox ID="txtUnitTypeID" runat="server"></asp:TextBox>
        </div>
        <div class="costperunit">
            <asp:Label ID="lblCostPerUnit" runat="server" Text="CostPerUnit" AssociatedControlID="txtCostPerUnit"></asp:Label>
            <asp:TextBox ID="txtCostPerUnit" runat="server"></asp:TextBox>
        </div>
        <div class="priceperunit">
            <asp:Label ID="lblPricePerUnit" runat="server" Text="PricePerUnit" AssociatedControlID="txtPricePerUnit"></asp:Label>
            <asp:TextBox ID="txtPricePerUnit" runat="server"></asp:TextBox>
        </div>
        <div class="name">
            <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div class="description">
            <asp:Label ID="lblDescription" runat="server" Text="Description" AssociatedControlID="txtDescription"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        </div>
        <div class="categoryname">
            <asp:Label ID="lblCategoryName" runat="server" Text="CategoryName" AssociatedControlID="txtCategoryName"></asp:Label>
            <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox>
        </div>
        <div class="unittypename">
            <asp:Label ID="lblUnitTypeName" runat="server" Text="UnitTypeName" AssociatedControlID="txtUnitTypeName"></asp:Label>
            <asp:TextBox ID="txtUnitTypeName" runat="server"></asp:TextBox>
        </div>
        <div class="buttons">
            <input type="button" id="btnSave" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
