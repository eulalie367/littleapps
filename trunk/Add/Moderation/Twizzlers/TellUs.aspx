<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TellUs.aspx.cs" Inherits="Hershey.Web.UmbracoAddons.Moderation.Twizzlers.TellUs"MasterPageFile="/Umbraco/masterpages/umbracoPage.Master" %>

<%@ Register Src="~/UmbracoAddons/Moderation/ModerationStateFilter.ascx" TagPrefix="ucMod" TagName="ModerationStateFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('iframe', window.parent.document).css('overflow', "scroll");
        });
    </script>
    
    <ucMod:ModerationStateFilter ID="stateFilter" runat="server" />

    <asp:GridView ID="gridView" runat="server" CellPadding="4" AutoGenerateColumns="False" DataKeyNames="Id"
        GridLines="None" EnableModelValidation="True" EmptyDataText="no rows found" AllowPaging="true" PageSize="10">
        <AlternatingRowStyle BackColor="LightGray" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />

        <Columns>
            <asp:BoundField DataField="BirthDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Birthdate" ReadOnly="true" />

            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" Columns="20" runat="server" Text='<%# Bind("FirstName") %>'
                        Rows="10" TextMode="SingleLine"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="State" HeaderText="State" ReadOnly="true" />

            <asp:TemplateField HeaderText="Suggestion">
                <ItemTemplate>
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Suggestion") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSuggestion" Columns="60" runat="server" Text='<%# Bind("Suggestion") %>'
                        Rows="10" TextMode="SingleLine"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="InsertedDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Created" ReadOnly="true" />

            <asp:ButtonField CommandName="approve" Text="Approve" ButtonType="Button" HeaderText="" />
            <asp:ButtonField CommandName="reject" Text="Reject" ButtonType="Button" HeaderText="" />
            <asp:CommandField ShowEditButton="true" ButtonType="Button" HeaderText="" />
        </Columns>
    </asp:GridView>

</asp:Content>
