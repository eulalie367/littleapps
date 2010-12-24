<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModerationStateFilter.ascx.cs" Inherits="Hershey.Web.UmbracoAddons.Moderation.ModerationStateFilter" %>
    <asp:Menu ID="menuMain" runat="server" Orientation="Horizontal" OnMenuItemClick="menuMain_MenuItemClick">
        <StaticMenuItemStyle BorderWidth="1px" HorizontalPadding="7px" VerticalPadding="1px" BorderColor="Gray" BackColor="LightGray" />
        <StaticSelectedStyle ForeColor="Black" Font-Bold="true" BorderColor="Gray" BackColor="ControlLightLight"/>

        <Items>
            <asp:MenuItem Text="Pending" Value="0" Selected="true"/> 
            <asp:MenuItem Text="Approved" Value="1" Selected="false"/>  
            <asp:MenuItem Text="Rejected" Value="2" Selected="false" />
        </Items>
    </asp:Menu>