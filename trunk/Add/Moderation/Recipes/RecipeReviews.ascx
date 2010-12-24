<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecipeReviews.ascx.cs"
    EnableViewState="true" Inherits="Hershey.Web.UmbracoAddons.Moderation.Recipes.RecipeReviews" %>
<asp:GridView ID="gvPendingRatings" runat="server" CellPadding="4" AutoGenerateColumns="False"
    DataKeyNames="RecipeID" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Approved">
            <EditItemTemplate>
                <asp:CheckBox ID="chkApproved" runat="server" Checked='<%# Eval("Approved") ?? false %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkApproved" runat="server" Checked='<%# Eval("Approved") ?? false %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="UserName" HeaderText="Username" />
        <asp:TemplateField HeaderText="Comment">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="RecipeID" DataNavigateUrlFormatString="/recipes/{0}/.aspx"
            DataTextField="RecipeName" HeaderText="Recipe" />
        <asp:BoundField DataField="Created" HeaderText="Created" DataFormatString="{0:d}" />
        <asp:CommandField ShowEditButton="True" ButtonType="Button" HeaderText="Edit" />
        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Delete" />
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
