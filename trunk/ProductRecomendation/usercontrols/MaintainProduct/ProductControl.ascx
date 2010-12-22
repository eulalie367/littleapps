<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.ascx.cs" Inherits="ProductRecomendation.usercontrols.MaintainProduct.ProductControl" %>


<div class="product">
    <h2 runat="server" id="productName"></h2>
    <h3 runat="server" id="productTypeName"></h3>
<%--    <ul>
        <asp:Repeater runat="server" ID="rptAttributes">
            <ItemTemplate>
                <li>
                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
--%>
    <div runat="server" id="dvAttributes"></div>
</div>


