<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SBWS.ECOM.Manage.Product.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "ProductID") %>
                            </td>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "CategoryName") %>
                            </td>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "UnitTypeName") %>
                            </td>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "CostPerUnit") %>
                            </td>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "PricePerUnit") %>
                            </td>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "Name") %>
                            </td>
                            <td>
                                <%# DataBinder.GetPropertyValue(Container.DataItem, "Description") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
