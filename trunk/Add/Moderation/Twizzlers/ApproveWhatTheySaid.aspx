<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveWhatTheySaid.aspx.cs" Inherits="Hershey.Web.UmbracoAddons.Moderation.Twizzlers.ApproveWhatTheySaid"  MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register Src="~/usercontrols/recipes/Details/Kitchens.ascx" TagName="Details"
    TagPrefix="Recipe" %>
<%@ Import Namespace="Hershey.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:TabView ID="TabView1" runat="server" Width="552px" Height="392px"></cc1:TabView>
    <cc1:Pane ID="pane_Details" runat="server">
        Pane 1
    </cc1:Pane>
    <cc1:Pane ID="pane_Comments" runat="server">
        Pane 2
    </cc1:Pane>
</asp:Content>
