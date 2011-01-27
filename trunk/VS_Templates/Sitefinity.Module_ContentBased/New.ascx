<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="New.ascx.cs" Inherits="$rootnamespace$.$fileinputname$.ViewControls.New" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Cms.Engine.WebControls" Assembly="Telerik.Cms.Engine" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Cms.Engine.WebControls.Admin" Assembly="Telerik.Cms.Engine" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Cms.Web.UI" Assembly="Telerik.Cms.Web.UI" %>

<div class="ToolsAll">
    <div class="backWrapp">
        <asp:HyperLink ID="BackButton1" CssClass="actions back" runat="server">
			Back to all $fileinputname$
        </asp:HyperLink>
    </div>
</div>
<div id="divWorkArea" runat="server" class="workArea">
    <sf:MessageControl runat="server" id="MessageControl1">
        <ItemTemplate>
                    <asp:Label runat="server" ID="messageText"></asp:Label>
                </ItemTemplate>
    </sf:MessageControl>
    <div class="mainForm">
        <p class="button_area top">
            <asp:LinkButton ID="saveButton1" runat="server" CssClass="CmsButLeft okdark">        
                <strong class="CmsButRight dark">Create item</strong>
            </asp:LinkButton>
            <span>or</span>
            <asp:LinkButton ID="cancelButton1" Text="Cancel" runat="server" CssClass="cmscclcmd"
                CausesValidation="false" />
        </p>
        <h3>Name *</h3>
        <fieldset class="set">
            <div class="setIn title">
                <asp:TextBox ID="Name" Text="$fileinputname$ Name..." runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="validator1" runat="server" ControlToValidate="Name"
                    Display="Dynamic" EnableViewState="False" CssClass="validMessage" SetFocusOnError="True" InitialValue="$fileinputname$ Name...">
                    <strong>$fileinputname$ name is a required field.</strong></asp:RequiredFieldValidator>
            </div>
        </fieldset>
        <div class="bottom">
            <div>
                <!-- -->
            </div>
        </div>
        <h3>
            $fileinputname$ Description *<em id="contentEditorLabel" runat="server"></em></h3>
        <fieldset class="set">
            <div class="setIn">
                <telerik:RadEditor id="Editor" runat="server" contentareacssfile="~/Sitefinity/Admin/Themes/Default/AjaxControlsSkins/Sitefinity/EditorContentArea.css"
                    toolsfile="~/Sitefinity/Admin/ControlTemplates/EditorToolsFile.xml" skin="WebBlue"
                    newlinebr="False" width="95%"> 
                                <ImageManager ViewPaths="~/Images" UploadPaths="~/Images" DeletePaths="~/Images" />
                                <MediaManager ViewPaths="~/Files" UploadPaths="~/Files" DeletePaths="~/Files" />
                                <FlashManager ViewPaths="~/Files" UploadPaths="~/Files" DeletePaths="~/Files" />
                                <DocumentManager ViewPaths="~/Files" UploadPaths="~/Files" DeletePaths="~/Files" />
                                <CssFiles>
                                    <telerik:EditorCssFile Value="~/Sitefinity/Admin/Themes/Default/AjaxControlsSkins/Sitefinity/EditorCssFile.css" />
                                </CssFiles>
                            </telerik:RadEditor>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Editor"
                    Display="Dynamic" EnableViewState="False" CssClass="validMessage" SetFocusOnError="True" InitialValue="Type your content here...">
                    <strong>$fileinputname$ description is a required field.</strong></asp:RequiredFieldValidator>
            </div>
        </fieldset>
        <div class="bottom">
            <div>
                <!-- -->
            </div>
        </div>
        <h3>
            Category</h3>
        <sf:ContentMetaFields id="MetaFields" runat="server">
            <ItemTemplate>
                    <fieldset class="set">
						<ol class="setIn">
							<li class="catSel clearfix">
                               <asp:Label ID="Label4" AssociatedControlID="Category" runat="server">
                               <asp:Literal ID="Literal10" runat="server" Text="Category"></asp:Literal>
                               <em id="Em1" runat="server"></em></asp:Label>
                               <sf:ContentCategoriesField ID="Category" runat="server" />
                               </li>
                          </ol>
                        </fieldset>
                        <div class="bottom"><div><!-- --></div></div>
                         <h3>Tags</h3>
                        <fieldset class="set">
                            <ol class="setIn">
                                <li class="tags">
                                    <sf:ContentTagEditor ID="tagsControl" runat="server" />
                                </li>
                            </ol>
                        </fieldset>
                        <div class="bottom"><div><!-- --></div></div>
                       
                    </ItemTemplate>
        </sf:ContentMetaFields>
        <p class="button_area bot">
            <asp:LinkButton ID="saveButton2" runat="server" CssClass="CmsButLeft okdark">
                <strong class="CmsButRight dark">Create menu item</strong>
            </asp:LinkButton>
            <span>or</span>
            <asp:LinkButton ID="cancelButton2" Text="Cancel" runat="server" CssClass="cmscclcmd"
                CausesValidation="false" />
        </p>
    </div>
    <div class="info" id="divEditFaq" runat="server">
        <div class="infoBottom">
            <h3>
                $fileinputname$ FAQ></h3>
            <dl class="faq">
                <dt>What is $fileinputname$ module?</dt>
                <dd>
                    $fileinputname$ module is a sample for development of Generic Content based modules.</dd>
            </dl>
            <p class="hideAllFAQs">
                <a href="javascript:void(0)" onclick="javascript:Personalization.hideFaqSection('<%= divEditFaq.ClientID %>')">
                    Hide FAQs everywhere</a>
            </p>
        </div>
    </div>
</div>

                <script type="text/javascript">

                    Telerik.Web.UI.Editor.CommandList["LibraryImageManager"] = function (commandName, editor, args) {
                        var editorArgs = editor.getSelectedElement();
                        if (!editorArgs.nodeName || typeof (editorArgs.nodeName) == "undefined" || editorArgs.nodeName != "A")
                            editorArgs = editor.getSelection();

                        var myCallbackFunction = function (sender, args) {
                            if (typeof (editorArgs.nodeName) != "undefined" && editorArgs.nodeName == "IMG")
                                args.parentNode.replaceChild(editorArgs, args);
                            else {
                                var cloned = args.cloneNode(true);
                                var div = args.ownerDocument.createElement("DIV");
                                div.appendChild(cloned);
                                editorArgs.pasteHtml(div.innerHTML);
                            }
                        }
                        editor.showExternalDialog(
                               '<%= Page.ResolveUrl("~/Sitefinity/UserControls/Dialogs/ImageEditorDialog.aspx") %>',
                               editorArgs,
                               750,
                               600,
                               myCallbackFunction,
                               null,
                               'ImageLibraryDialog',
                               true,
                               Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                               false,
                               true)
                    };

                    Telerik.Web.UI.Editor.CommandList["LibraryDocumentManager"] = function (commandName, editor, args) {
                        var editorArgs = editor.getSelectedElement();
                        if (!editorArgs.nodeName || typeof (editorArgs.nodeName) == "undefined" || editorArgs.nodeName != "A")
                            editorArgs = editor.getSelection();

                        var myCallbackFunction = function (sender, args) {
                            if (typeof (editorArgs.nodeName) != "undefined" && editorArgs.nodeName == "A")
                                args.parentNode.replaceChild(editorArgs, args);
                            else {
                                var cloned = args.cloneNode(true);
                                var div = args.ownerDocument.createElement("DIV");
                                div.appendChild(cloned);
                                editorArgs.pasteHtml(div.innerHTML);
                            }
                        }
                        editor.showExternalDialog(
                               '<%= Page.ResolveUrl("~/Sitefinity/UserControls/Dialogs/DocumentEditorDialog.aspx") %>',
                               editorArgs,
                               750,
                               600,
                               myCallbackFunction,
                               null,
                               'ImageLibraryDialog',
                               false,
                               Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                               false,
                               true)
                    };

                    Telerik.Web.UI.Editor.CommandList["LinkManager"] = function (commandName, editor, args) {
                        var editorArgs = editor.getSelectedElement();
                        if (!editorArgs.nodeName || typeof (editorArgs.nodeName) == "undefined" || editorArgs.nodeName != "A") {
                            var sel = editor.getSelection();
                            editorArgs = sel;
                            editorArgs.Html = sel.getHtml();
                            editorArgs.Text = sel.getText();
                        }

                        var myCallbackFunction = function (sender, args) {
                            if (typeof (editorArgs.nodeName) != "undefined" && editorArgs.nodeName == "A")
                                args.parentNode.replaceChild(editorArgs, args);
                            else {
                                var cloned = args.cloneNode(true);
                                var div = args.ownerDocument.createElement("DIV");
                                div.appendChild(cloned);
                                editorArgs.pasteHtml(div.innerHTML);
                            }
                        }
                        editor.showExternalDialog(
                               '<%= Page.ResolveUrl("~/Sitefinity/UserControls/Dialogs/LinksDialog.aspx") %>',
                               editorArgs,
                               750,
                               600,
                               myCallbackFunction,
                               null,
                               'ImageLibraryDialog',
                               false,
                               Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                               false,
                               true)
                    };

                    Telerik.Web.UI.Editor.CommandList["SetLinkProperties"] = function (commandName, editor, args) {
                        var editorArgs = editor.getSelectedElement();
                        if (!editorArgs.nodeName || typeof (editorArgs.nodeName) == "undefined" || editorArgs.nodeName != "A")
                            editorArgs = editor.getSelection();

                        var myCallbackFunction = function (sender, args) {
                            if (typeof (editorArgs.nodeName) != "undefined" && editorArgs.nodeName == "A")
                                args.parentNode.replaceChild(editorArgs, args);
                            else {
                                var cloned = args.cloneNode(true);
                                var div = args.ownerDocument.createElement("DIV");
                                div.appendChild(cloned);
                                editorArgs.pasteHtml(div.innerHTML);
                            }
                        }
                        editor.showExternalDialog(
                               '<%= Page.ResolveUrl("~/Sitefinity/UserControls/Dialogs/LinksDialog.aspx") %>',
                               editorArgs,
                               750,
                               600,
                               myCallbackFunction,
                               null,
                               'ImageLibraryDialog',
                               false,
                               Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                               false,
                               true)
                    };

                    var oldFunction = Telerik.Web.UI.Editor.CommandList["ToggleScreenMode"]; //save the original Paste function

                    Telerik.Web.UI.Editor.CommandList["ToggleScreenMode"] = function (commandName, editor, args) {
                        oldFunction(commandName, editor, args);
                        var bd = document.getElementsByTagName("body")[0];

                        if (/fullScreenMode/.test(bd.className)) {
                            var rep = bd.className.match(' ' + 'fullScreenMode') ? ' ' + 'fullScreenMode' : 'fullScreenMode';
                            bd.className = bd.className.replace(rep, '');

                        } else {
                            bd.className += bd.className ? ' ' + 'fullScreenMode' : 'fullScreenMode';
                        }
                    };

                    // automated tests helper function
                    function InsertTextArea() {
                        var editor = $find('<%=Editor.ClientID%>');
                        editor.set_html('<textarea id="myTableToFind" style="overflow:hidden; height: 300px; width: 500px;" border="none"></textarea>');
                    }


                </script>
