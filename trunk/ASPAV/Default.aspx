<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="ASPAV._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        var isIE  = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
        var isNS = (navigator.appName.indexOf("Netscape") != -1);
        var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
        AVCapabilities = function ()
        {
            this.GetPluginID = function(PluginName)
            {
                retVal = "";
               if(isIE && isWin)
               {
                    switch(PluginName.toLowerCase())
                    {
                        case "svg":
                            retVal = "Adobe.SVGCtl";
                        break;
                        case "shockwave":
                            retVal = "SWCtl.SWCtl.1";
                        break;
                        case "flash":
                            retVal = "ShockwaveFlash.ShockwaveFlash.1";
                        break;
                        case "realplayer":
                            retVal = "rmocx.RealPlayer G2 Control.1";
                        break;
                        case "quicktime":
                            retVal = "QuickTimeCheckObject.QuickTimeCheck.1";
                        break;
                        case "mediaplayer":
                            retVal = "MediaPlayer.MediaPlayer.1";
                        break;
                        case "acrobat":
                            retVal = "PDF.PdfCtrl.5";
                        break;
                    }
               }
               else if(isNS || !isWin)
               {
                    switch (PluginName.toLowerCase())
                    {
                        case "svg":
                            retVal = "image/svg-xml";
                        break;
                        case "shockwave":
                            retVal = "application/x-director";
                        break;
                        case "flash":
                            retVal = "application/x-shockwave-flash";
                        break;
                        case "realplayer":
                            retVal = "audio/x-pn-realaudio-plugin";
                        break;
                        case "quicktime":
                            retVal = "video/quicktime";
                        break;
                        case "mediaplayer":
                            retVal = "application/x-mplayer2";
                        break;
                        case "acrobat":
                            retVal = "application/pdf";
                        break;
                    }
               }
               return retVal;
            }        
            this.HasPlugin = function (PluginName)
            {
                var p = PluginName;
                var retVal = null;
                if (isIE && isWin) 
                {
                    result = false; 
                    document.write("<SCRIPT LANGUAGE=\"VBScript\">\n on error resume next \n result = IsObject(CreateObject(\"" + this.GetPluginID(PluginName) + "\"))<\/SCRIPT>\n"); 
                    retVal = result;
                }
                else if(isNS || !isWin)
                {
                    nse = "";
                    for (var i=0;i<navigator.mimeTypes.length;i++)
                    {
	                    nse += navigator.mimeTypes[i].type.toLowerCase();
                        if (nse.indexOf(this.GetPluginID(PluginName)) != -1) 
                        {
                            if (navigator.mimeTypes[this.GetPluginID(PluginName)].enabledPlugin != null)
                            { 
                                retVal = true;
                                break;
                            } 
                            else
                            {
                                retVal = false;
                            }
                        }
                        else
                        {
                            retVal = false;
                        }        
                    }
                }
                return retVal;
            }
            this.hasFlash = this.HasPlugin("Flash");
            this.hasDirector = this.HasPlugin("Shockwave");                
            this.hasRealPlayer = this.HasPlugin("RealPlayer");                
            this.hasQuickTime = this.HasPlugin("QuickTime");                
            this.hasWindowsMediaPlayer = this.HasPlugin("MediaPlayer");                
            this.hasAcrobat = this.HasPlugin("Acrobat");                
            this.hasSVG = this.HasPlugin("SVG");
            this.hasJava = (navigator.javaEnabled());
            this.hasSilverlight = typeof(Silverlight) != "undefined" && Silverlight.isInstalled("2.0");
            this.toString = function()
            {
                retVal = "";
                
                retVal += "<BrowserPlugins   ";
                retVal += "Flash=\"" + this.hasFlash + "\" ";
                retVal += " Director=\"" + this.hasDirector + "\" ";
                retVal += "RealPlayer=\"" + this.hasRealPlayer + "\" ";
                retVal += "QuickTime=\"" + this.hasQuickTime + "\" ";
                retVal += "WindowsMediaPlayer=\"" + this.hasWindowsMediaPlayer + "\" ";
                retVal += "Acrobat=\"" + this.hasAcrobat + "\" ";
                retVal += "SVG=\"" + this.hasSVG + "\" ";
                retVal += "Java=\"" + this.hasJava + "\" ";
                retVal += "Silverlight=\"" + this.hasSilverlight + "\" ";
                retVal += "/>";
                
                return retVal;
            };
        }
    </script>
    <script type="text/javascript">
        function Load()
        {
            var a = new AVCapabilities();            
            if(a.hasFlash)
            {
                alert(a.toString());
            }
        }    
    </script>
</head>
<body onload="Load();">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
