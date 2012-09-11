<%@ Control Language="C#" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<h1>Projects</h1>

<telerik:RadListView ID="ProjectsList" ItemPlaceholderID="ItemsContainer" GroupPlaceholderID="GroupContainer" GroupItemCount="3" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="Projects">
            <asp:PlaceHolder ID="GroupContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <GroupTemplate>
        <div class="row">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </div>
    </GroupTemplate>
    <ItemTemplate>
        <div class="column-small">
            <div class="block">
                <p><sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" runat="server"><div id="photoDiv" runat="server"></div></sf:DetailsViewHyperLink></p>
				<h3><sf:DetailsViewHyperLink ID="DetailsViewHyperLink" TextDataField="Title" ToolTipDataField="Description" runat="server" /></h3>
				<address>
					<sf:FieldListView ID="Address" runat="server" Text="{0}" Properties="Address" /><br />
					
					<sf:FieldListView ID="City" runat="server" Text="{0}" Properties="City" />
					<sf:FieldListView ID="Region" runat="server" Text="{0}" Properties="Region" /> 
					<sf:FieldListView ID="PostalCode" runat="server" Text="{0}" Properties="PostalCode" /> 
					<sf:FieldListView ID="Country" runat="server" Text="{0}" Properties="Country" />
				</address>
				
			</div>
        </div>
    </ItemTemplate>
</telerik:RadListView>

<sf:Pager id="pager" runat="server" />
