<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TellUsControl.ascx.cs"
    Inherits="Hershey.Web.UmbracoAddons.Moderation.Twizzlers.TellUsControl" EnableViewState="true" %>
<div>
    <input type="hidden" runat="server" id="editIndex" />
    <table cellpadding="2px" cellspacing="0px" width="100%">
        <thead style="border-bottom: solid 5px #ccc; font-weight: bold;">
            <tr>
                <td>
                    Date of Birth
                </td>
                <td>
                    Name
                </td>
                <td>
                    State
                </td>
                <td>
                    Message
                </td>
                <td>
                    Date Created
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </thead>
        <asp:ListView runat="server" ID="lvTellUs">
            <LayoutTemplate>
                <tr runat="server" id="itemPlaceholder">
                </tr>
            </LayoutTemplate>
            <ItemTemplate>
                <tr runat="server">
                    <td>
                        <%# ParseDate(DataBinder.Eval(Container.DataItem, "BirthDate"))%>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "State")%>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "Suggestion")%>
                    </td>
                    <td>
                        <%# ParseDate(DataBinder.Eval(Container.DataItem, "InsertedDate")) %>
                    </td>
                    <td>
                        <input type="hidden" runat="server" id="hidID" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />
                        <asp:Button runat="server" CommandName="Approve" Text="Approve" />
                    </td>
                    <td>
                        <asp:Button runat="server" CommandName="Reject" Text="Reject" />
                    </td>
                    <td>
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr runat="server">
                    <td>
                        <input type="hidden" runat="server" id="hidID" value='<%# Bind("Id")%>' />
                        <input type="text" runat="server" id="tbDOB" value='<%# Bind("BirthDate")%>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="tbName" value='<%# Bind("FirstName")%>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="tbState" value='<%# Bind("State")%>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="tbMessage" value='<%# Bind("Suggestion")%>' />
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "InsertedDate")%>
                    </td>
                    <td>
                        <asp:Button runat="server" CommandName="Update" ID="btnUpdate" Text="Update" />
                    </td>
                    <td>
                        <asp:Button ID="cancel" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView>
    </table>
</div>
