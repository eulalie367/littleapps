<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModerateBakingTips.aspx.cs"
    MasterPageFile="~/masterpages/RecipeAdmin.Master" Inherits="Hershey.Web.UmbracoAddons.Moderation.Recipes.ModerateBakingTips" %>

<%@ Register Src="~/UmbracoAddons/Moderation/ModerationStateFilter.ascx" TagPrefix="ucMod"
    TagName="ModerationStateFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ucMod:ModerationStateFilter ID="stateFilter" runat="server" />
    <div style="overflow: auto">
        <asp:gridview id="gridView" runat="server" cellpadding="4" autogeneratecolumns="False"
            datakeynames="SubmissionId" gridlines="None" enablemodelvalidation="True" emptydatatext="no rows found"
            allowpaging="true" pagesize="10" editrowstyle-verticalalign="Top">
            <AlternatingRowStyle BackColor="LightGray" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# GetShortenedString(Eval("CategoryName"), 5) %>'
                            ToolTip='<%# Eval("CategoryName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recipe Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# GetShortenedString(Eval("RecipeName"), 5) %>'
                            NavigateUrl='<%# "/recipes/" + Eval("RecipeId") + "/" + Eval("RecipeName") + ".aspx" %>'
                            ToolTip='<%# Eval("RecipeName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="true" />
                <asp:TemplateField HeaderText="Title">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTitle" Columns="25" runat="server" Text='<%# Bind("Title") %>'
                            Rows="10" TextMode="SingleLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tip">
                    <EditItemTemplate>
                        <br />
                        <br />
                        <asp:TextBox ID="txtTip" Columns="40" runat="server" Text='<%# Bind("Tip") %>' Rows="10"
                            TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTip" runat="server" Text='<%# Bind("Tip") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedOn" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Created"
                    ReadOnly="true" InsertVisible="False" />
                <asp:ButtonField CommandName="approve" Text="Approve" ButtonType="Button" HeaderText="" InsertVisible="False" />
                <asp:ButtonField CommandName="reject" Text="Reject" ButtonType="Button" HeaderText="" InsertVisible="False" />
                <asp:CommandField ShowEditButton="true" ButtonType="Button" HeaderText="" />
            </Columns>
        </asp:gridview>
    </div>
</asp:Content>
