<%@ Page Trace="false" Language="c#" CodeBehind="umbraco.aspx.cs" AutoEventWireup="True"
    Inherits="umbraco.cms.presentation._umbraco" ClientTarget="uplevel" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register TagPrefix="uc1" TagName="quickEdit" Src="dashboard/quickEdit.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>umbraco -
        <%=Request.Url.Host.ToLower().Replace("www.", "") %></title>
    <asp:PlaceHolder id="IActionJSFileRef" runat="server"></asp:PlaceHolder>
    <link href="css/xtree.css" type="text/css" rel="stylesheet" />
    <link href="css/umbContext.css" type="text/css" rel="stylesheet" />
    <link href="css/umbracoGui.css" type="text/css" rel="stylesheet" />
    <!-- Seperate modal stylesheet -->
    <link href="/umbraco_client/modal/style.css" type="text/css" rel="stylesheet" />
    <!-- Jquery Scripts -->

    <script type="text/javascript" src="/umbraco_client/ui/jquery.js"></script>

    <script type="text/javascript" src="/umbraco_client/modal/modal.js"></script>

    <script type="text/javascript" src="js/guiFunctions.js"></script>

    <script type="text/javascript" src="js/language.aspx"></script>

    <script type="text/javascript" src="js/umbracoDefault.js"></script>

    <!-- Default js library for events, dimension utills etc -->

    <script type="text/javascript" src="/umbraco_client/ui/default.js"></script>

    <!-- Support to fix IE bug with multiple resize events -->

    <script type="text/javascript" src="/umbraco_client/ui/jQueryWresize.js"></script>

    <script type="text/javascript">
        this.name = 'umbracoMain';
    </script>

</head>
<body id="umbracoMainPageBody">
    <form id="Form1" method="post" runat="server" style="margin: 0px; padding: 0px">
    <asp:ScriptManager runat="server" ID="umbracoScriptManager">
        <Services>
            <asp:ServiceReference Path="webservices/legacyAjaxCalls.asmx" />
        </Services>
    </asp:ScriptManager>
        </form>

    <div style="position: relative;">
        <div class="topBar" id="topBar">
            <div style="float: left">
                <button id="buttonCreate" onclick="createWizard(); return false;" class="topBarButton" accesskey="c">
                    <img src="images/new.png" alt="<%=umbraco.ui.Text("general", "create")%>" /><span><%=umbraco.ui.Text("general", "create")%>...</span>
                </button>
            </div>
            <asp:Panel ID="FindDocuments" runat="server">
                <div style="float: left; margin-left: 20px;">
                <form onsubmit="openModal('dialogs/search.aspx?rndo=45.2&search=' + jQuery('#umbSearchField').val(), 'Search', 470, 620); return false;">
                    <uc1:quickEdit ID="QuickEdit1" runat="server"></uc1:quickEdit>
                </form>
                </div>
            </asp:Panel>
            <div class="topBarButtons">
                <button onclick="about(); return false;" class="topBarButton">
                    <img src="images/about.png" alt="about" /><span><%=umbraco.ui.Text("general", "about")%></span></button>
                <button onclick="window.open('http://umbraco.org/redir/help/<%=this.getUser().Language%>/<%=this.getUser().UserType.Name%>/' + currentApp + fetchUrl(right.document.location.href), 'help','width=750,height=500,scrollbars=auto,resizable=1;'); return false;"
                    class="topBarButton">
                    <img src="images/help.png" alt="Help" /><span><%=umbraco.ui.Text("general", "help")%></span></button>
                <button onclick="if (confirm('<%=umbraco.ui.Text("areyousure")%>')) {document.location.href='logout.aspx'; return false;} else {return false}"
                    class="topBarButton">
                    <img src="images/logout.png" alt="Log out" /><span><%=umbraco.ui.Text("general", "logout")%>:
                        <%=this.getUser().Name%></span></button>
            </div>
        </div>
    </div>
    <a id="treeToggle" href="#" onclick="toggleTree(this); return false;" title="Hide Treeview">
        &nbsp;</a>
    <div id="uiArea">
        <div id="leftDIV">
            <cc1:UmbracoPanel AutoResize="false" ID="treeWindow" runat="server" Height="380px"
                Width="200px" hasMenu="false">
                <iframe class="umbracoGuiWindow" id="tree" style="height: 100%; width: 100%; display: block;"
                    name="tree" src="TreeInit.aspx" frameborder="0"></iframe>
            </cc1:UmbracoPanel>
            <cc1:UmbracoPanel ID="PlaceHolderAppIcons" Text="Sektioner" runat="server" Height="140px"
                Width="200px" hasMenu="false" AutoResize="false">
                <ul id="tray">
                    <asp:Literal ID="plcIcons" runat="server"></asp:Literal>
                </ul>
            </cc1:UmbracoPanel>
        </div>
        <div id="rightDIV">
            <!-- umbraco dashboard -->
            <iframe name="right" id="right" marginwidth="0" marginheight="0" frameborder="0"
                scrolling="no" style="display: none; width: 100%; height: 100%;"></iframe>
        </div>
    </div>

    <script type="text/javascript">
    	  <asp:PlaceHolder ID="bubbleText" Runat="server"/>
    </script>

    <iframe src="keepalive.aspx" style="border: none; width: 1px; height: 1px; position: absolute;">
    </iframe>
    <div id="defaultSpeechbubble">
    </div>
    <div id="umbModalBox">
        <div id="umbModalBoxHeader">
        </div>
        <a href="#" id="umbracModalBoxClose" class="jqmClose">&times;</a>
        <div id="umbModalBoxContent">
            <iframe frameborder="0" id="umbModalBoxIframe" src=""></iframe>
        </div>
    </div>

    <script type="text/javascript" src="js/UmbracoSpeechBubbleBackend.js"></script>

    <script type="text/javascript">

        jQuery(document).ready(function() {


            resizePage('load');
            jQuery(window).wresize(function() { resizePage('resize'); });

            jQuery("#umbracoMainPageBody").css("background", "#fff");


            if (initApp == "" && document.location.hash) {
                initApp = document.location.hash.substring(1);
            }


            // load dashboard
            if (rightAction != '') {
                jQuery("#right").attr("src", rightAction + ".aspx?id=" + rightActionId);
            } else {
                jQuery("#right").attr("src", "dashboard.aspx?app=" + initApp);
            }

            if (initApp != '') {
                shiftApp(initApp, uiKeys['sections_' + initApp], 'true');
            }
            jQuery("#right").show();
        });
    </script>

</body>
</html>
