<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductTypeControl.aspx.cs" Inherits="ProductRecomendation.CustomTrees.ProductMaintenance.ProductTypeControl"
    MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:TabView ID="TabView1" runat="server" Width="552px" Height="392px"></cc1:TabView>
    <cc1:Pane ID="pane_Details" runat="server">
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
        </div>
    </cc1:Pane>
</asp:Content>
