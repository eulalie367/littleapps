<%@ Page Language="c#" CodeBehind="TreeInit.aspx.cs" AutoEventWireup="True" Inherits="umbraco.cms.presentation.TreeInit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TreeInit</title>

    <script type="text/javascript" language="javascript">
      var menuType = 'treeEdit';
      var menuActive = '';
		  <asp:PlaceHolder id="PlaceHolderJavascript" runat="server"></asp:PlaceHolder>
    </script>

    <!-- styles -->
    <link type="text/css" rel="stylesheet" href="css/xtree.css" />
    <link type="text/css" rel="stylesheet" href="/umbraco_client/ui/default.css" />
    <link type="text/css" rel="stylesheet" href="css/treeIcons.css" />
    
    <style type="text/css">
        body
        {
            background: #fff;
            margin: 0px;
            padding: 0px;
        }
        
    </style>
    <!--[if IE 8]>
<style type="text/css">
.newProtectOverlay
{
top: -3px; 
}
.newVersionOverlay
{
top: -12px; 
}
</style>
<![endif]-->
    <!--[if IE 6]>
    <style type="text/css">
          .sprTree{
            background-image: url(images/umbraco/sprites_ie6.gif) !Important;
          } 
    </style>
    <![endif]-->
        
    <!-- jquery -->
    <script type="text/javascript" src="<%=umbraco.GlobalSettings.Path %>_client/ui/jquery.js"></script>
    <script type="text/javascript" src="<%=umbraco.GlobalSettings.Path %>_client/ui/jqueryui.js"></script>

    <script type="text/javascript" src="js/umbContext.js"></script>
    <script type="text/javascript" src="js/contextmenu.aspx"></script>
    <script type="text/javascript" src="js/xtree.js"></script>
    <script type="text/javascript" src="js/xmlextras.js"></script>
    <script type="text/javascript" src="js/xloadtree.js"></script>

    
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div style="padding-right: 5px; padding-left: 2px; padding-bottom: 0px; padding-top: 1px">
        <asp:PlaceHolder ID="PlaceHolderTree" runat="server"></asp:PlaceHolder>
    </div>
    </form>

    <script type="text/javascript">
      <%if (umbraco.helper.Request("syncTree") != "") { %>
      
        jQuery(document).ready(function() {
            var treeArray = '<%=umbraco.helper.Request("syncTree") %>'.split(',');
            jQuery(document).bind('xTreeNodeLoaded', function(event, msg) {
                SyncTree(treeArray, '');
            });

        });
      
      <%} %>
    </script>

</body>
</html>
