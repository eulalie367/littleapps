<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="envato._Default" %>
<%@ Register Namespace="gImage" TagPrefix="gImage" Assembly="envato" %>
<%@ Register Namespace="EditableControls" TagPrefix="EditableControls" Assembly="envato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <gImage:Image ID="Image1" runat="server" ImageUrl="/Zoomable/Images/LargeImage2.jpg" UserName="eulalie367@gmail.com" Password="edgarallen" AlbumName="Your Site" Height="400" />
        <gImage:Image ID="asdf" runat="server" ImageUrl="/Zoomable/Images/LargeImage.jpg" UserName="eulalie367@gmail.com" Password="edgarallen" AlbumName="Your Site" Height="400" />
        <EditableControls:DropDownList ID="ddEditable" runat="server">
            
        </EditableControls:DropDownList>
    </div>
    </form>
</body>
</html>
