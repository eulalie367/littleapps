<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/RecipeAdmin.Master" AutoEventWireup="true" CodeBehind="SubmittedRecipes.aspx.cs" Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.SubmittedRecipes" %>
<%@ Register src="SubmittedRecipeList.ascx" tagname="SubmittedRecipeList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SubmittedRecipeList ID="SubmittedRecipeList1" runat="server" />
</asp:Content>
