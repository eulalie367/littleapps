<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SBWS.ECOM.Manage.Category.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ListView runat="server" ID="lvCategory" DataSourceID="dsCategory" DataKeyNames="CategoryID" InsertItemPosition="LastItem">
            <LayoutTemplate>
                <table cellpadding="2" runat="server" id="itemPlaceholder">
                </table>
                <asp:DataPager runat="server" ID="DataPager" PageSize="3">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="5" PreviousPageText="<--" NextPageText="-->" />
                    </Fields>
                </asp:DataPager>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="EditButton" runat="Server" Text="Edit" CommandName="Edit" />
                    </td>
                    <td>
                         <%#Eval("Name") %>
                    </td>
                    <td>
                         <%#Eval("Description") %>
                    <br />
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="Update" />
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td>
                        <asp:TextBox ID="tbName" runat="server" Text='<%#Bind("Name") %>' MaxLength="50" />
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%#Bind("Description") %>' MaxLength="250" />
                    <br />
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Add" />
                    </td>
                    <td>
                        <asp:TextBox ID="tbName" runat="server" Text='<%#Bind("Name") %>' MaxLength="50" />
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%#Bind("Description") %>' MaxLength="250" />
                    <br />
                    </td>
                </tr>
            </InsertItemTemplate>
        </asp:ListView>
              <asp:SqlDataSource ID="dsCategory" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ECOM %>"
        SelectCommand="SELECT CategoryID, ParentCategoryID, Name, Description FROM Category (NOLOCK)"
        UpdateCommand="UPDATE Category
                       SET Name = @Name, Description = @Description
                       WHERE CategoryID = @CategoryID"
        InsertCommand="INSERT INTO Category (Name, Description) VALUES(@Name, @Description)"
                       >
      </asp:SqlDataSource>
    </form>
</body>
</html>
