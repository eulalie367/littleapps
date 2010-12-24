<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecipeSearch.ascx.cs"
    Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.RecipeSearch" %>
<h3>
    Filter Recipes:<input type="button" onclick="javascript:hideSearch()" value="<<< Close" /></h3>
<strong>"Search Words" include any part of the recipe name, lead line, instructions,ingredient,
    or recipe #.</strong>
<table class="style1">
    <tr>
        <td>
            Search Words
        </td>
        <td>
            <asp:TextBox ID="txtRecipeSearch" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Recipe Type
        </td>
        <td>
            <asp:DropDownList ID="ddRecipeTypeFilter" runat="server" DataTextField="category_name"
                DataValueField="category_ID">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Product
        </td>
        <td>
            <asp:DropDownList ID="ddFilterByProduct" runat="server" DataTextField="Name" DataValueField="Id"
                Width="200">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Occasion
        </td>
        <td>
            <asp:DropDownList ID="ddFilterByOccasion" runat="server" DataTextField="Occasion1"
                DataValueField="Occasion_ID">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Limit To:
        </td>
        <td>
            <asp:CheckBoxList ID="chkFilterByIdea" runat="server" DataTextField="category_name"
                RepeatLayout="Flow" DataValueField="category_ID">
            </asp:CheckBoxList>
            <ul>
                <li>
                    <input type="checkbox" id="chkBeginner" runat="server" />
                    <asp:Label ID="Label38" AssociatedControlID="chkBeginner" runat="server" Text="Beginner" />
                </li>
                <li>
                    <input type="checkbox" id="chkIntermediate" runat="server" />
                    <asp:Label ID="Label39" AssociatedControlID="chkIntermediate" runat="server" Text="Intermediate" />
                </li>
                <li>
                    <input type="checkbox" id="chkAdvanced" runat="server" />
                    <asp:Label ID="Label40" AssociatedControlID="chkAdvanced" runat="server" Text="Advanced" />
                </li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>
            Recipe Status
        </td>
        <td>
            <asp:RadioButton ID="rdoAny" runat="server" Text="Any" GroupName="status" />
            <asp:RadioButton ID="rdoInActive" runat="server" Text="Inactive Only" GroupName="status" />
            <asp:RadioButton ID="rdoActive" runat="server" Text="Active Only" GroupName="status" />
        </td>
    </tr>
    <%--<tr>
        <td>
            Created By
        </td>
        <td>
            <asp:DropDownList ID="ddCreatedBy" runat="server" DataTextField="Name" DataValueField="UserId">
            </asp:DropDownList>
        </td>
    </tr>--%>
    <tr>
        <td>
            Last Edited By
        </td>
        <td>
            <asp:DropDownList ID="ddEditedBy" runat="server" DataTextField="Name" DataValueField="Name">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                OnClientClick="javascript:DoSearch()" />
        </td>
        <td align="right">
            &nbsp;
        </td>
    </tr>
</table>
