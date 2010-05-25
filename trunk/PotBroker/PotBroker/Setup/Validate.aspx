<%@ Page Language="C#" MasterPageFile="~/MasterPages/HomePage.master" AutoEventWireup="true" CodeBehind="Validate.aspx.cs" Inherits="PotBroker.Validate" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
<div class="login">
    <div class="username">
        <asp:Label ID="Label1" AssociatedControlID="tbUserName" runat="server">User Name:</asp:Label>
        <input type="text" id="tbUserName" runat="server" />
    </div>
    <div class="pass">
        <asp:Label ID="Label2" AssociatedControlID="tbPassword" runat="server">Password:</asp:Label>
        <input type="password" id="tbPassWord" runat="server" />
    </div>
    <div class="submit">
        <input type="submit" id="subLogin" runat="server" value="Login" />
    </div>
</div>
</asp:Content>
