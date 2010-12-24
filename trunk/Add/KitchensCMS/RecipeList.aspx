<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="RecipeList.aspx.cs"
    MasterPageFile="~/masterpages/RecipeAdmin.Master" Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.RecipeList" %>

<%@ Register Src="RecipeSearch.ascx" TagName="RecipeSearch" TagPrefix="uc1" %>
<%@ Register Src="RecipeList.ascx" TagName="RecipeList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    	
    <script language="javascript" type="text/javascript">

        function hideSearch() {
            $(".divSearchPanel").hide(500);
            $("#searchRecipes").show();
            return false;
        }

        function showSearch() {
            $(".divSearchPanel").show(500);
            $("#searchRecipes").hide();
            return false;
        }

        function DoSearch() {
            $(".gvRecipes").hide(500);
        }

        function ShowResults() {
            $(".gvRecipes").show(500);
        }   


    </script>
    <style type="text/css">
        body    {width:100%;height:100%;overflow-y:scroll;overflow-x:hidden;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="button" onclick="javascript:showSearch()" value="Filter Recipe List >>"
        id="searchRecipes" style="display: none" />
    <asp:updatepanel runat="server" id="updContact">
<ContentTemplate>
    <table id="table-container">
        <tbody>
            <tr>
                <td style="width: 1px;">
                    <div id="divSearchPanel" class="divSearchPanel" runat="server">                        
                        <uc1:RecipeSearch ID="RecipeSearch1" runat="server" />
                    </div>
                </td>
                <td>
                    <div id="divRecipePanel">
                        <uc1:RecipeList ID="RecipeList1" runat="server" />
                    </div>
                    <div style="clear:both;height:0;overflow:hidden"></div>
                </td>
            </tr>
        </tbody>
    </table>

</ContentTemplate>
</asp:updatepanel>
</asp:Content>
