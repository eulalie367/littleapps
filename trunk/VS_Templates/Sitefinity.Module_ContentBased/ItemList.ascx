<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemList.ascx.cs" Inherits="$rootnamespace$.$fileinputname$.ViewControls.ItemList" %>

<%@ Register TagPrefix="sf" Namespace="Telerik.Cms.Web.UI" Assembly="Telerik.Cms.Web.UI" %>

<sf:JsFileLink id="jsLink" runat="server" ScriptType="jQuery"></sf:JsFileLink>

<div style="display:none;">
    <asp:TextBox ID="FilterExpressionField" runat="server"></asp:TextBox>
</div>
<div class="ToolsAll">
    <asp:HyperLink ID="createNewButton" runat="server" cssClass="CmsButLeft new">
		<strong class="CmsButRight light">
			<asp:Literal ID="createNewButtonText" runat="server" />
		</strong>
	</asp:HyperLink>
    <asp:PlaceHolder ID="fullWindow" runat="server">
    <div class="searchItems">
        <div class="searchInputs">
            <asp:Label ID="searchFieldLabel" AssociatedControlID="searchField" runat="server"></asp:Label>
            <asp:DropDownList ID="searchField" runat="server"></asp:DropDownList>
            <asp:Label ID="searchWordsLabel" AssociatedControlID="searchWords" runat="server"></asp:Label>
            <asp:TextBox ID="searchWords" CssClass="searchString" runat="server"></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" CssClass="searchButton"/>
        </div>
    </div>
    </asp:PlaceHolder>
    <div class="clear"><!-- --></div>
</div>
<div class="workArea">
	<telerik:MessageControl runat="server" ID="message">
		<ItemTemplate>
			<asp:Label runat="server" ID="messageText"></asp:Label>  
		</ItemTemplate>
    </telerik:MessageControl>
	<h2 class="gridTitle">
		All $fileinputname$
	</h2>
	<telerik:ClientTemplatesHolder ID="GridTemplates" runat="server">
		<telerik:ClientTemplate Name="Edit" runat="server">
			<a href="<%= Parent.Parent.ItemEditUrl %>">Edit</a>
		</telerik:ClientTemplate>
		<telerik:ClientTemplate Name="Delete" runat="server">
			<a href="javascript:if(confirm('Are you sure you want to delete this $fileinputname$?')) Delete$fileinputname$('{#ID#}')">Delete</a>
		</telerik:ClientTemplate>
		<telerik:ClientTemplate Name="Name" runat="server">
			<a href="<%= Parent.Parent.ItemViewUrl %>">{#MetaFields.Name#}</a>
		</telerik:ClientTemplate>
		<telerik:ClientTemplate Name="Status" runat="server">
			{#Status#}
		</telerik:ClientTemplate>
	</telerik:ClientTemplatesHolder>
	<div id="gridPlaceholder">
	<telerik:RadGrid ID="ItemsGrid" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" PageSize="50" EnableViewState="false" Skin="SitefinityItems" EnableEmbeddedSkins="false">
		<MasterTableView AllowMultiColumnSorting="false" CssClass="listItems listItemsBindOnClient" Width="98%">
			<Columns>
				<telerik:GridTemplateColumn ItemStyle-CssClass="gridActions edit" UniqueName="Edit">
                </telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn 
					UniqueName="Delete" ItemStyle-CssClass="gridActions delete">
                </telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn 
					UniqueName="Name"  
					SortExpression="Name" 
					ItemStyle-CssClass="gridContentTitle"
					HeaderText="Name">
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn 
					UniqueName="Status"  
					SortExpression="Status" 
					HeaderText="Status">
                </telerik:GridTemplateColumn>
			</Columns>
        </MasterTableView>
        <PagerStyle Mode="NumericPages" />
        <ClientSettings>
			<ClientEvents OnCommand="RadGrid_Command" OnRowDataBound="RadGrid_RowDataBound" />
        </ClientSettings>
	</telerik:RadGrid>
	</div>
	<asp:HiddenField ID="cultureInfoField" runat="server" />
	 <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
		<script type="text/javascript">
<!--
		    var dataProviderName = "<%= Parent.Parent.ProviderName %>";
		    var gridTemplates = ClientTemplates.GetSet("<%= GridTemplates.ClientID %>");
		    var allowDelete = "<%= Parent.Parent.AllowDelete %>";
		    var allowModify = "<%= Parent.Parent.AllowModify %>";

		    function loadData() {
		        var tableView = $find('<%= ItemsGrid.ClientID %>').get_masterTableView();
		        var currentPageIndex = tableView.get_currentPageIndex();
		        var pageSize = tableView.get_pageSize();
		        var filterExpressionField = document.getElementById('<%= FilterExpressionField.ClientID %>');
		        DataBindGrid(currentPageIndex, pageSize, "", filterExpressionField.value);
		    }
		    Sys.Application.add_load(loadData);
		    function DataBindGrid(currentPageIndex, pageSize, sortExpressionsAsSQL, filterExpressionsAsSQL) {
		        var requiredMetaFields = ['Name'];
		        var cultureInfoField = document.getElementById('<%= cultureInfoField.ClientID %>');
		        Telerik.Cms.Engine.Services.ContentService.GetContentItems(currentPageIndex * pageSize, pageSize,
    sortExpressionsAsSQL, filterExpressionsAsSQL, requiredMetaFields, cultureInfoField.value, dataProviderName, updateGrid, OnFailed);
		    }
		    function RadGrid_Command(sender, args) {
		        args.set_cancel(true);
		        var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
		        var pageSize = sender.get_masterTableView().get_pageSize();
		        var sortExpressions = sender.get_masterTableView().get_sortExpressions();
		        var filterExpressions = sender.get_masterTableView().get_filterExpressions();
		        if (sortExpressions.length > 0 && filterExpressions.length > 0 && currentPageIndex > 0) {
		            sender.get_masterTableView().set_currentPageIndex(0);
		        }
		        var filterExpressionField = document.getElementById('<%= FilterExpressionField.ClientID %>');
		        var sortExpressionsAsSQL = sortExpressions.toString();
		        var filterExpressionsAsSQL = filterExpressions.toString();
		        if (filterExpressionsAsSQL.length == 0)
		            filterExpressionsAsSQL = filterExpressionField.value;
		        DataBindGrid(currentPageIndex, pageSize, sortExpressionsAsSQL, filterExpressionsAsSQL);
		    }
		    function updateGrid(result) {
		        var tableView = $find("<%= ItemsGrid.ClientID %>").get_masterTableView();
		        tableView.set_virtualItemCount(result.Count);
		        tableView.set_dataSource(result.Data);
		        tableView.dataBind();

		        var emptyWindow = document.getElementById('empty');
		        var gridPlaceholder = document.getElementById('gridPlaceholder');
		        if (result.Count > 0) {
		            emptyWindow.style.display = 'none';
		            gridPlaceholder.style.display = '';
		        }
		        else {
		            emptyWindow.style.display = '';
		            gridPlaceholder.style.display = 'none';
		        }
		    }
		    function RadGrid_RowDataBound(sender, args) {
		        var dataItem = args.get_dataItem();
		        var item = args.get_item();
		        var columns = item.get_owner().get_columns();
		        var cells = args.get_item().get_element().cells;

		        if (dataItem['IsLocked'] == true) {
		            args.get_item().get_element().className = "cms_locked";
		        }
		        else {
		            args.get_item().get_element().className = "";
		        }

		        for (var i = 0; i < cells.length; i++) {
		            var cell = cells[i];
		            var html = gridTemplates.Replace(columns[i].get_element().UniqueName, dataItem);
		            if (html != null)
		                if (html != "")
		                    cell.innerHTML = html;
		                else
		                    cell.innerHTML = "&nbsp;";
		        }

		        // disable delete if needed
		        if (dataItem['IsDisabled'] == true || (allowDelete.toLowerCase() == 'false' && dataItem['IsOwner'] == false)) {
		            $(args.get_item().get_element()).children(".delete").children("a").removeAttr("href");
		            $(args.get_item().get_element()).children(".deleteDis").children("a").removeAttr("href");
		            $(args.get_item().get_element()).children(".delete").addClass("deleteDis");
		            $(args.get_item().get_element()).children(".delete").removeClass("delete");
		        }
		        else {
		            $(args.get_item().get_element()).children(".deleteDis").addClass("delete");
		            $(args.get_item().get_element()).children(".deleteDis").removeClass("deleteDis");
		        }

		        // disabled edit if needed
		        if (dataItem['IsDisabled'] == true || (allowModify.toLowerCase() == 'false' && dataItem['IsOwner'] == false)) {
		            $(args.get_item().get_element()).children(".edit").children("a").removeAttr("href");
		            $(args.get_item().get_element()).children(".editDis").children("a").removeAttr("href");
		            $(args.get_item().get_element()).children(".edit").addClass("editDis");
		            $(args.get_item().get_element()).children(".edit").removeClass("edit");
		        }
		        else {
		            $(args.get_item().get_element()).children(".editDis").addClass("edit");
		            $(args.get_item().get_element()).children(".editDis").removeClass("editDis");
		        }
		    }
		    function OnFailed(error) {
		        alert("Stack Trace: " + error.get_stackTrace() + "/r/n" +
		"Error: " + error.get_message() + "/r/n" +
		"Status Code: " + error.get_statusCode() + "/r/n" +
		"Exception Type: " + error.get_exceptionType() + "/r/n" +
		"Timed Out: " + error.get_timedOut());
		    }
		    function Delete$fileinputname$(contentId) {
		        Telerik.Cms.Engine.Services.ContentService.DeleteContent(contentId, dataProviderName, Delete$fileinputname$_Success, OnFailed);
		    }
		    function Delete$fileinputname$_Success(result) {
		        var tableView = $find('<%= ItemsGrid.ClientID %>').get_masterTableView();
		        var currentPageIndex = tableView.get_currentPageIndex();
		        var pageSize = tableView.get_pageSize();
		        var sortExpression = tableView.get_sortExpressions();
		        var filterExpressionField = document.getElementById('<%= FilterExpressionField.ClientID %>');
		        var sortExpressionAsSQL = sortExpression.toString();
		        var filterExpressionAsSQL = filterExpressionField.value.toString();
		        DataBindGrid(currentPageIndex, pageSize, sortExpressionAsSQL, filterExpressionAsSQL);
		    }
-->
		</script>
	</telerik:RadCodeBlock>
	<asp:PlaceHolder ID="emptyWindow" runat="server">
		<div id="empty">
			<h2 class="gridTitle">Currently there are no $fileinputname$.</h2>
			<p>
			    <asp:HyperLink ID="createNewEmpty" runat="server" cssClass="mainLink" ToolTip="Click here create your first $fileinputname$."><strong>Create your first $fileinputname$.</strong></asp:HyperLink><br />
			</p>       
		</div>
	</asp:PlaceHolder>
</div>



