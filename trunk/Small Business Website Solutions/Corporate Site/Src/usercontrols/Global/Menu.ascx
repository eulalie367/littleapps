<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="SBWS.usercontrols.Global.Menu" %>
<ul>
    <li runat="server" id="liHome" visible="false"><a runat="server" id="aHome"><span id="spHome" runat="server"></span></a></li>
    <% if(!string.IsNullOrEmpty(this.Seperator)){ %>
    <li class="spacer"><%= this.Seperator %></li>
    <% } %>
    <asp:Repeater ID="menuItems" runat="server">
        <ItemTemplate>
            <li><a <%# selected(Container) %> href="<%# DataBinder.Eval(Container.DataItem, "Url")%>"><span><%# DataBinder.Eval(Container.DataItem, "Name")%></span></a></li>
            <%# seperator(Container) %>
        </ItemTemplate>
    </asp:Repeater>
</ul>




