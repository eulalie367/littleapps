<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.aspx.cs" Inherits="ProductRecomendation.CustomTrees.ProductMaintenance.ProductControl"    MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:TabView ID="TabView1" runat="server" Width="552px" Height="392px"></cc1:TabView>
    <cc1:Pane ID="pane_Details" runat="server">
        <div class="product">
            <h2>Product</h2>
            <div class="name">
                <asp:Label runat="server" Text="Name:" />
                <input type="text" id="tbName" runat="server" />
            </div>
        </div>
    </cc1:Pane>
</asp:Content>
