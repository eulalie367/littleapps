<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Preview.ascx.cs" Inherits="$rootnamespace$.$fileinputname$.ViewControls.Preview" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Workflow.WebControls" Assembly="Telerik.Workflow"  %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Cms.Engine.WebControls" Assembly="Telerik.Cms.Engine"  %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Localization.WebControls" Assembly="Telerik.Localization"  %>


<div class="ToolsAll">
    <asp:Label ID="lockedWarning" runat="server">
		<p class="locked">
			<strong>{0}</strong> 
			is editing $fileinputname$
		</p>
    </asp:Label>
    <div class="backWrapp">
        <asp:HyperLink ID="BackButton1" CssClass="actions back" runat="server">
			Back to all $fileinputname$
        </asp:HyperLink>
    </div>
    <telerik:RadTabStrip id="tabStrip" Align="Right" runat="server" selectedindex="0"
        causesvalidation="false" EnableEmbeddedSkins="false" Skin="SitefinityPages">
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
    <telerik:MessageControl runat="server" ID="message1">
        <ItemTemplate>
			<asp:Label runat="server" ID="messageText"></asp:Label>
		</ItemTemplate>
    </telerik:MessageControl>
    <div class="view">
        <p class="button_area">
            <!-- BEGIN EXCLUDE FOR COMMUNITY -->
            <telerik:WorkflowMenu ID="workflowMenu" runat="server" />
            <!-- END EXCLUDE FOR COMMUNITY -->
            <asp:HyperLink ID="editCommand1" runat="server" CssClass="CmsButLeft editdark">	
				<strong class="CmsButRight dark">Edit this $fileinputname$</strong>
            </asp:HyperLink>
        </p>
        <div class="setW clearfix">
            <div class="setInW clearfix">
                <div class="viewIn">
                    <h1 class="viewHead">
                        <asp:Literal ID="itemName" runat="server" />
                    </h1>
                    <telerik:GenericContent ID="contentPreview" runat="server" />
                </div>
                <div class="details">
                    <asp:Repeater ID="repeaterItemMetaData" runat="server">
                        <ItemTemplate>
                            <dt>
                                <asp:Literal ID="lblKey" runat="server" />
                            </dt>
                            <dd>
                                <asp:Literal ID="lblValue" runat="server" />
                            </dd>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="bottomW">
            <div>
                <!-- -->
            </div>
        </div>
    </div>
    <div class="info">
        <div class="infoBottom">
            <!-- BEGIN EXCLUDE FOR COMMUNITY -->
            <asp:PlaceHolder ID="languagePanel" runat="server">
                <h3>
                    Change Language</h3>
                <div class="langCol set">
                    <telerik:LanguageBar CssClass="setIn" ID="languageBar" PostBack="True" runat="server">
                        <LayoutTemplate>
                            <telerik:LanguageList ID="languageList" runat="server" CssClass="setIn">
								<ItemTemplate>
								    <asp:HyperLink CssClass="emptyLang" runat="server" />
								</ItemTemplate>
								<CurrentItemTemplate>
								    <asp:Label CssClass="currentLang"  runat="server" />
								</CurrentItemTemplate>
								<SelectedItemTemplate>
								    <asp:HyperLink CssClass="filledLang" runat="server" />
								</SelectedItemTemplate>
                            </telerik:LanguageList>
                        </LayoutTemplate>
                    </telerik:LanguageBar>
                </div>
                <div class="bottom">
                    <div>
                        <!-- -->
                    </div>
                </div>
            </asp:PlaceHolder>
            <!-- END EXCLUDE FOR COMMUNITY -->
            <div id="divViewFaq" runat="server">
                <h3>
                    $fileinputname$ FAQ
                </h3>
                <dl class="faq">
                    <dt>What is $fileinputname$ module?</dt>
                    <dd>
                        $fileinputname$ module is a sample for development of Generic Content based modules.</dd>
                </dl>
                <p class="hideAllFAQs">
                    <a href="javascript:void(0)" onclick="javascript:Personalization.hideFaqSection('<%= divViewFaq.ClientID %>')">
                        Hide FAQs everywhere</a>
                </p>
            </div>
        </div>
    </div>
    <div class="clear">
        <!-- -->
    </div>
</div>


