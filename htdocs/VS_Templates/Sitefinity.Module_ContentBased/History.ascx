<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="History.ascx.cs" Inherits="$rootnamespace$.$fileinputname$.ViewControls.History" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Versioning.WebControls" Assembly="Telerik.Versioning" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Cms.Web.UI" Assembly="Telerik.Cms.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Localization.WebControls" Assembly="Telerik.Localization" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Workflow.WebControls" Assembly="Telerik.Workflow" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="ToolsAll">
    <asp:Label ID="lockedWarning" runat="server">
    <p class="locked"><strong>{0}</strong> is editing $fileinputname$</p>
    </asp:Label>
    <div class="backWrapp">
        <asp:HyperLink runat="server" ID="backButton" CssClass="actions back">Back to all $fileinputname$</asp:HyperLink>
    </div>
    <telerik:RadTabStrip 
					id="tabStrip" 
					Align="Right" 
					runat="server"
					selectedindex="2" 
					causesvalidation="false"
					EnableEmbeddedSkins="false"
					Skin="SitefinityPages"
					>
						<Tabs>
							<telerik:RadTab Text="View" ></telerik:RadTab>
							<telerik:RadTab Text="Edit" ></telerik:RadTab>
							<telerik:RadTab Text="History" ></telerik:RadTab>
						</Tabs>
					</telerik:RadTabStrip>
    <div class="clear">
        <!-- -->
    </div>
</div>
<div id="divWorkArea" runat="server" class="workArea">
    <telerik:MessageControl runat="server" ID="message2">
        <ItemTemplate>
                <asp:Label runat="server" ID="messageText"></asp:Label>
         </ItemTemplate>
    </telerik:MessageControl>
    <telerik:VersionList ID="versionListControl" runat="server">
        <ListTemplate>
                    <h2 class="gridTitle">$fileinputname$ Versions</h2>
                    <asp:GridView ID="GridView1" AllowPaging="true" AllowSorting="true" PageSize="20" AutoGenerateColumns="false" GridLines="none" CssClass="listItems" runat="server">
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="gridActions">
                                <ItemTemplate>
                                    <asp:HyperLink ID="view" Text="View" runat="server"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" CommandName="Delete" Text="Rollback" accessibleheadertext="Rollback"><ItemStyle CssClass="gridActions" /></asp:ButtonField>
                            <asp:BoundField DataField="Version" HeaderText="Version" HeaderStyle-CssClass="GridHeader_SiteFinity"><ItemStyle CssClass="gridContentTitle" /></asp:BoundField>
                            <asp:BoundField DataField="TimeStamp" HeaderText="Date>" HeaderStyle-CssClass="GridHeader_SiteFinity" DataFormatString="{0:dd MMM yyyy, hh:mm}" HtmlEncode="false" />
                            <asp:BoundField DataField="Modifier" HeaderText="Modifier" HeaderStyle-CssClass="GridHeader_SiteFinity" />
                            <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-CssClass="GridHeader_SiteFinity" />
                        </Columns>
                    </asp:GridView>
        </ListTemplate>
    </telerik:VersionList>
   <div class="clear">
        <!-- -->
    </div>
</div>


