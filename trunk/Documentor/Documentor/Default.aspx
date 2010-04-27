<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Documentor._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script id="" type="text/javascript" language="javascript">
        Documentor = function()
        {
            this.Elements = [];
            this.Elements.toString = function()
            {
                 retVal = "";
                 for(i=0;i<this.length;i++)
                 {
                    e = this[i];
                    retVal += e.className;
                 }
                 return retVal;
            }
        }
        var thisDocumentor = new Documentor();
        function LoadElement(elem)
        {
             var d = thisDocumentor;
             d.Elements.push(elem);
             document.write(d.Elements.toString());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="blah" class="blah" onclick="LoadElement(this);">
    hello
    </div>
    </form>
</body>
</html>
