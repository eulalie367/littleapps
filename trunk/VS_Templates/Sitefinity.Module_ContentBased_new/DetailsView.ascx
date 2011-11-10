<%@ Control Language="C#" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="Projects-single content">

    <telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="column-small">
                <div class="block">
                    <p id="photo" runat="server"></p>
				    <h3><sf:FieldListView ID="Title" runat="server" Text="{0}" Properties="Title" /></h3>
				    <address>
					    <sf:FieldListView ID="Address" runat="server" Text="{0}" Properties="Address" /><br />
						<sf:FieldListView ID="City" runat="server" Text="{0}" Properties="City" /> 
						<sf:FieldListView ID="Region" runat="server" Text="{0}" Properties="Region" /><br />
					    <sf:FieldListView ID="PostalCode" runat="server" Text="{0}" Properties="PostalCode" /><br />
					    
					    
				    </address>
			    </div>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
</div>