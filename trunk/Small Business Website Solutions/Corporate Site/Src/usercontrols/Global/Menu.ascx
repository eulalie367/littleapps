<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="SBWS.usercontrols.Global.Menu" %>
<ul>
    <li runat="server" id="liHome" visible="false"><a runat="server" id="aHome"></a></li>
    <li class="spacer">|</li>
    <asp:Repeater ID="menuItems" runat="server">
        <ItemTemplate>
            <li><a <%# selected(Container) %> href="<%# DataBinder.Eval(Container.DataItem, "Url")%>"><%# DataBinder.Eval(Container.DataItem, "Name")%></a></li>
            <%# seperator(Container) %>
        </ItemTemplate>
    </asp:Repeater>
</ul>
