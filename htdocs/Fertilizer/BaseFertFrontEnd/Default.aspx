<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaseFertFrontEnd._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" class="textbox" id="Compound" runat="server" />
        <input type="button" class="button" id="btnCalculate" runat="server" />
        <div id="content" runat="server" />
    </div>
    </form>
</body>
</html>
