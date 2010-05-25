<%@ Page Language="C#" MasterPageFile="~/MasterPages/HomePage.master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="PotBroker.CreateAccount" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
<div class="createAccount">
    <div class="username">
        <asp:Label ID="lblUserName" AssociatedControlID="tbUserName" runat="server">User Name:</asp:Label>
        <input type="text" id="tbUserName" runat="server" />
    </div>
    <div class="pass">
        <asp:Label AssociatedControlID="tbPassword" runat="server">Password:</asp:Label>
        <input type="password" id="tbPassWord" runat="server" />
    </div>
    <div class="pass">
        <asp:Label ID="lblConfirmPassword" AssociatedControlID="tbConfirmPass" runat="server">Confirm Password:</asp:Label>
        <input type="password" id="tbConfirmPass" runat="server" />
    </div>
    <div class="pass">
        <asp:Label AssociatedControlID="tbEmailAddress" runat="server">Email Address:</asp:Label>
        <input type="text" id="tbEmailAddress" runat="server" />
    </div>
    <div class="pass">
        <asp:Label ID="lblConfirmEmail" AssociatedControlID="tbConfirmEmailAddress" runat="server">Confirm Email Address:</asp:Label>
        <input type="text" id="tbConfirmEmailAddress" runat="server" />
    </div>
    <div class="submit">
        <input type="submit" id="subCreateAccount" runat="server" value="Create Your Account" />
    </div>
    <div class="login">
        Already a Member <a href="Login.aspx" title="Login to The Pot Broker">Login.</a>
    </div>
</div>
</asp:Content>
