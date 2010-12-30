<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttributeControl.aspx.cs" Inherits="ProductRecomendation.CustomTrees.ProductMaintenance.AttributeControl"  MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <cc1:TabView ID="TabView1" runat="server" Width="552px" Height="392px"></cc1:TabView>
    <cc1:Pane ID="pane_Details" runat="server">
        <div class="product">
            <h2>Product Type Attribute</h2>
            <div class="name">
                <asp:Label runat="server" Text="Name:" />
                <input type="text" id="tbName" runat="server" />
            </div>
            <div class="attributeType">
                <asp:Label runat="server" Text="Attribute Type:" />
                <asp:DropDownList ID="selAttributeType" runat="server" />
            </div>
        </div>
    </cc1:Pane>
</asp:Content>
