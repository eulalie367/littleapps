<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubmittedRecipeList.ascx.cs"
    Inherits="Hershey.Web.UmbracoAddons.KitchensCMS.SubmittedRecipeList" %>
<asp:GridView ID="gvRecipes" runat="server" CellPadding="4" EnableModelValidation="True"
    ForeColor="#333333" GridLines="None" AllowPaging="False" PageSize="10" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="recipe_Id" CssClass="gvRecipes" PagerStyle-CssClass="hideOnClick">
    <PagerSettings PageButtonCount="20" Position="TopAndBottom" Mode="NumericFirstLast" FirstPageText="First Page" />
    
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:HyperLinkField DataNavigateUrlFields="recipe_ID" DataNavigateUrlFormatString="/recipes/{0}/preview.aspx"
            ControlStyle-CssClass="preview" Target="_blank" DataTextField="recipe_ID" HeaderText="# (Preview)"
            SortExpression="recipe_Id" />
        <asp:TemplateField HeaderText="Active" SortExpression="Active">
            <HeaderTemplate>
                Active<br />
                <asp:Button ID="Button2" runat="server" Text="Update" OnClick="UpdateRecipeStatus" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkVisible" runat="server" Checked='<%# Eval("IsActive") %>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:CheckBox ID="chkVisible" runat="server" Checked='<%# Eval("IsActive") %>' />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:ImageField DataImageUrlField="recipe_ID" DataImageUrlFormatString="/Image.ashx?type=submittedphoto&amp;id={0}&amp;s=sm&amp;missing=1"
            HeaderText="Image" SortExpression="Image">
            <ItemStyle Height="40px" Width="60px" />
        </asp:ImageField>
        <asp:HyperLinkField DataNavigateUrlFields="recipe_ID" DataNavigateUrlFormatString="SubmittedRecipeReview.aspx?Id={0}"
            ControlStyle-CssClass="edit" DataTextField="RecipeName" HeaderText="Recipe Name (Click to edit)"
            SortExpression="RecipeName" />
        <asp:BoundField DataField="DateUpdated" DataFormatString="{0:d}" HeaderText="Updated"
            SortExpression="DateUpdated" />
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerSettings PageButtonCount="50" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <EmptyDataTemplate>
        <h3>
            Sorry, there are no recipes for this filter.</h3>
    </EmptyDataTemplate>
</asp:GridView>
