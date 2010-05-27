<%@ Page Language="c#" CodeBehind="editMedia.aspx.cs" ValidateRequest="false" MasterPageFile="masterpages/umbracoPage.Master"
  AutoEventWireup="True" Inherits="umbraco.cms.presentation.editMedia" %>
  
<asp:Content ContentPlaceHolderID="head" runat="server">

  <script type="text/javascript">
    // Save handlers for IDataFields		
    var saveHandlers = new Array()

    function addSaveHandler(handler) {
      saveHandlers[saveHandlers.length] = handler;
    }

    function invokeSaveHandlers() {
      for (var i = 0; i < saveHandlers.length; i++) {
        eval(saveHandlers[i]);
      }
    }

  </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
  <asp:PlaceHolder ID="plc" runat="server"></asp:PlaceHolder>
  <input id="doSave" type="hidden" name="doSave" runat="server">
  <input id="doPublish" type="hidden" name="doPublish" runat="server">
  
  <asp:Panel ID="syncScript" runat="server">

    <script type="text/javascript">
        parent.top.syncTree('<asp:Literal id="SyncPath" runat="server"></asp:Literal>', '<%=umbraco.helper.Request("isNew") %>', '<asp:Literal id="newName" runat="server"></asp:Literal>', '');
    </script>

  </asp:Panel>
</asp:Content>
