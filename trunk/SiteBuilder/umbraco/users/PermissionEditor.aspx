<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../masterpages/umbracoPage.Master" CodeBehind="PermissionEditor.aspx.cs" Inherits="umbraco.cms.presentation.user.PermissionEditor" %>

<%@ Register Src="NodePermissions.ascx" TagName="NodePermissions" TagPrefix="user" %>
<%@ Register Namespace="umbraco.presentation.controls" Assembly="umbraco" TagPrefix="tree" %>
<%@ Register TagPrefix="ui" Namespace="umbraco.uicontrols" Assembly="controls" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
	<link href="../css/treeicons.css" type="text/css" rel="stylesheet" />
	<link href="../css/xtree.css" type="text/css" rel="stylesheet" />

	<script type="text/javascript" src="PermissionsEditor.js"></script>
	<script type="text/javascript" language="javascript">				
		jQuery(document).ready(function() {
			//initialize the permissionsEditor with the current user id and other element ids.
			PermissionsEditor.Init(<%=Request.QueryString["id"] %>, "currPermissions", "treeContainer", "chkChildPermissions");			
		});
    </script>  

	<style type="text/css">
		a
		{
			text-decoration: none !important;
		}
		.treeCheckBox
		{
			padding: 0px 5px 0px 0px;
		}
		.treeIcon
		{
			padding: 0px 10px 0px 0px;
		}
		.treeText
		{
			color: #000;
		}
		.treeRootItem a
		{
			color: #000;
			text-decoration: none;
			padding-left: 7px;
		}
		#permissionGrid td
		{
			vertical-align: top;
		}
		.activePermissions
		{
			font-weight: bold;
		}
		#currPermissions
		{
			display: none;
			float: right;
			height: auto !important;
			min-height: 300px;
			height: 300px;
			width: 48%;
			border-left: 1px solid #D9D7D7;
			margin-left: 20px;
			padding-left: 20px;
		}
		#nodepermissionsList
		{
			list-style: none;
		}
	</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">

	<ui:UmbracoPanel ID="pnlUmbraco" runat="server" hasMenu="true" Text="Content Tree Permissions" Width="608px">
		<ui:Pane ID="pnl1" Style="padding: 10px; text-align: left;" runat="server" Text="Select pages to modify their permissions">
			<div style="float: left; width: 45%;" id="treeContainer">	
			
			<tree:CheckboxTree ID="cbt_content" runat="server" OnClientNodeChecked="PermissionsEditor.TreeNodeChecked" ExpandDepth="1" PopulateNodesFromClient="true" EnableClientScript="true" OnTreeNodePopulate="objTree_TreeNodePopulate" CollapseImageUrl="../images/xp/Lminus.gif" EnableViewState="true" ExpandImageUrl="../images/xp/Lplus.gif" RootNodeStyle-CssClass="treeRootItem">
					<Nodes>
						<asp:TreeNode Text="Content" ImageUrl="../images/umbraco/folder_o.gif" SelectAction="Expand" ShowCheckBox="false" PopulateOnDemand="true" Value="-1" />
					</Nodes>
				</tree:CheckboxTree>		
			</div>
			<div id="currPermissions">
				<user:NodePermissions ID="nodePermissions" runat="server" />
			</div>			
		</ui:Pane>
	</ui:UmbracoPanel>
	
</asp:Content>
