<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModerateRecipeReviews.aspx.cs"
    MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" Inherits="Hershey.Web.UmbracoAddons.Moderation.Recipes.ModerateRecipeReviews" %>

<%@ Register Src="~/UmbracoAddons/Moderation/ModerationStateFilter.ascx" TagPrefix="ucMod"
    TagName="ModerationStateFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('iframe', window.parent.document).css('overflow', "scroll");
        });
    </script>
    <ucMod:ModerationStateFilter ID="stateFilter" runat="server" />
    <asp:GridView ID="gridView" runat="server" CellPadding="4" AutoGenerateColumns="False"
        GridLines="None" DataKeyNames="RecipeRatingsID" EnableModelValidation="True"
        EmptyDataText="no rows found" AllowPaging="true" PageSize="10" PagerSettings-Mode="NumericFirstLast"
        PagerSettings-PageButtonCount="20" EmptyDataRowStyle-VerticalAlign="NotSet" EditRowStyle-VerticalAlign="Top"
        PagerSettings-Position="TopAndBottom">
        <AlternatingRowStyle BackColor="LightGray" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="Recipe Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# GetShortenedString(Eval("RecipeName"), 5) %>'
                        NavigateUrl='<%# "/recipes/" + Eval("RecipeId") + "/" + Eval("RecipeName") + ".aspx" %>'
                        ToolTip='<%# Eval("RecipeName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User Name">
                <ItemTemplate>
                    <%# Eval("UserName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <%# Eval("UserName")%>
                    <br />
                    -----------------------<br />
                    Overall:
                    <%# Eval("Rating") %>/5
                    <br />
                    Taste:
                    <%# Eval("TasteRating") %>/5
                    <br />
                    Difficulty:
                    <%# Eval("DifficultyRating") %>/5
                    <br />
                    Appearance:
                    <%# Eval("AppearanceRating") %>/5
                    <br />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comment">
                <EditItemTemplate>
                    <br />
                    <br />
                    <asp:TextBox ID="txtComment" Columns="60" runat="server" Text='<%# Bind("Comment") %>'
                        Rows="10" TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblComment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Created" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Created"
                ReadOnly="true" />
            <asp:ButtonField CommandName="approve" Text="Approve" ButtonType="Button" HeaderText="" InsertVisible="False" />
            <asp:ButtonField CommandName="reject" Text="Reject" ButtonType="Button" HeaderText="" InsertVisible="False" />
            <asp:CommandField CausesValidation="false" ShowEditButton="True" ButtonType="Button"
                HeaderText="" />
        </Columns>
    </asp:GridView>
    <%--    <cc1:TabView ID="tabControl" runat="server" Width="552px" Visible="true" AutoResize="true" />
    <cc1:Pane ID="pane_PendingReviews" runat="server">
    blah<br />        
   </cc1:Pane>--%>
    <%--<uc1:RecipeReviews ID="RecipeReviews1" runat="server" Visible="true" />--%>
</asp:Content>
