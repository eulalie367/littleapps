<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/RecipeAdmin.Master"
    AutoEventWireup="true" CodeBehind="Recipe.aspx.cs" Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.RecipeEdit" %>

<%@ Register Src="RecipeEditForm.ascx" TagName="RecipeEditForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <script type="text/javascript">        
        $(document).ready(function () {
            $("#tab1").addClass('active');
        });
    
    </script>
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="tabnav">
        <li id="tab1" class="tab" onclick="toggleRecipeTabs('General');">General</li>
        <li id="tab2" class="tab" onclick="toggleRecipeTabs('Search');">Search</li>
        <li id="tab3" class="tab" onclick="toggleRecipeTabs('Nutrition');">Nutrition</li>
        <li id="tab4" class="tab" onclick="toggleRecipeTabs('Additional');">Additional</li>
    </ul>
    <%--<asp:updatepanel runat="server" id="updContact">
<ContentTemplate>--%>  
   
    <uc1:RecipeEditForm ID="RecipeEditForm1" runat="server" />

<%--</ContentTemplate>
</asp:updatepanel>--%>
</asp:Content>
