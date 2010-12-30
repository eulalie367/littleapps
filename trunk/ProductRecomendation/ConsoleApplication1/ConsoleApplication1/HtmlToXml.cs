// Copyright (C) 2008 Alain COUTHURES http://www.agencexml.com info@agencexml.com
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.

/* COMPILE INSTRUCTIONS
%WINDIR%\Microsoft.NET\Framework\v2.0.50727\Csc.exe /out:WebCapt.dll /target:library WebCapt.cs
%WINDIR%\Microsoft.NET\Framework\v2.0.50727\RegAsm.exe /codebase /tlb:WebCapt.tlb WebCapt.dll
*/

/* Excel 2003 VBA sample
Dim wc As New WebCapt.agenceXML
wc.Capture("http://weather.yahoo.com/forecast/FRXX0016.html")
MsgBox wc.Query("//div[@id='weather-title']//h1") & ": " & wc.Query("//dd[preceding-sibling::*[1][name()='dt' and contains(.,'Feels Like:')]]") & ", " & wc.Query("//div[@id='yui-main']/div[@class='yui-b']/div[@class='forecast-module']/h3")
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
namespace WebCapt
{
    public class agenceXML
    {
        enum states { text, tag, endtag, attrtext, script, endscript, specialtag, comment, skipcdata, entity, namedentity, numericentity, hexaentity, tillgt, tillquote, tillinst, andgt };
        private Dictionary<string, int> namedentities = new Dictionary<string, int>();
        private List<string> emptytags = new List<string>();
        private Dictionary<string, List<string>> autoclosetags = new Dictionary<string, List<string>>();
        public XmlDocument xDoc { get; set; }
        public agenceXML()
        {
            xDoc = new XmlDocument();
            namedentities["AElig"] = 198;
            namedentities["Aacute"] = 193;
            namedentities["Acirc"] = 194;
            namedentities["Agrave"] = 192;
            namedentities["Alpha"] = 913;
            namedentities["Aring"] = 197;
            namedentities["Atilde"] = 195;
            namedentities["Auml"] = 196;
            namedentities["Beta"] = 914;
            namedentities["Ccedil"] = 199;
            namedentities["Chi"] = 935;
            namedentities["Dagger"] = 8225;
            namedentities["Delta"] = 916;
            namedentities["ETH"] = 208;
            namedentities["Eacute"] = 201;
            namedentities["Ecirc"] = 202;
            namedentities["Egrave"] = 200;
            namedentities["Epsilon"] = 917;
            namedentities["Eta"] = 919;
            namedentities["Euml"] = 203;
            namedentities["Gamma"] = 915;
            namedentities["Iacute"] = 205;
            namedentities["Icirc"] = 206;
            namedentities["Igrave"] = 204;
            namedentities["Iota"] = 921;
            namedentities["Iuml"] = 207;
            namedentities["Kappa"] = 922;
            namedentities["Lambda"] = 923;
            namedentities["Mu"] = 924;
            namedentities["Ntilde"] = 209;
            namedentities["Nu"] = 925;
            namedentities["OElig"] = 338;
            namedentities["Oacute"] = 211;
            namedentities["Ocirc"] = 212;
            namedentities["Ograve"] = 210;
            namedentities["Omega"] = 937;
            namedentities["Omicron"] = 927;
            namedentities["Oslash"] = 216;
            namedentities["Otilde"] = 213;
            namedentities["Ouml"] = 214;
            namedentities["Phi"] = 934;
            namedentities["Pi"] = 928;
            namedentities["Prime"] = 8243;
            namedentities["Psi"] = 936;
            namedentities["Rho"] = 929;
            namedentities["Scaron"] = 352;
            namedentities["Sigma"] = 931;
            namedentities["THORN"] = 222;
            namedentities["Tau"] = 932;
            namedentities["Theta"] = 920;
            namedentities["Uacute"] = 218;
            namedentities["Ucirc"] = 219;
            namedentities["Ugrave"] = 217;
            namedentities["Upsilon"] = 933;
            namedentities["Uuml"] = 220;
            namedentities["Xi"] = 926;
            namedentities["Yacute"] = 221;
            namedentities["Yuml"] = 376;
            namedentities["Zeta"] = 918;
            namedentities["aacute"] = 225;
            namedentities["acirc"] = 226;
            namedentities["acute"] = 180;
            namedentities["aelig"] = 230;
            namedentities["agrave"] = 224;
            namedentities["alpha"] = 945;
            namedentities["and"] = 8743;
            namedentities["ang"] = 8736;
            namedentities["aring"] = 229;
            namedentities["asymp"] = 8776;
            namedentities["atilde"] = 227;
            namedentities["auml"] = 228;
            namedentities["bdquo"] = 8222;
            namedentities["beta"] = 946;
            namedentities["brvbar"] = 166;
            namedentities["bull"] = 8226;
            namedentities["cap"] = 8745;
            namedentities["ccedil"] = 231;
            namedentities["cedil"] = 184;
            namedentities["cent"] = 162;
            namedentities["chi"] = 967;
            namedentities["circ"] = 710;
            namedentities["clubs"] = 9827;
            namedentities["cong"] = 8773;
            namedentities["copy"] = 169;
            namedentities["crarr"] = 8629;
            namedentities["cup"] = 8746;
            namedentities["curren"] = 164;
            namedentities["dagger"] = 8224;
            namedentities["darr"] = 8595;
            namedentities["deg"] = 176;
            namedentities["delta"] = 948;
            namedentities["diams"] = 9830;
            namedentities["divide"] = 247;
            namedentities["eacute"] = 233;
            namedentities["ecirc"] = 234;
            namedentities["egrave"] = 232;
            namedentities["empty"] = 8709;
            namedentities["emsp"] = 8195;
            namedentities["ensp"] = 8194;
            namedentities["epsilon"] = 949;
            namedentities["equiv"] = 8801;
            namedentities["eta"] = 951;
            namedentities["eth"] = 240;
            namedentities["euml"] = 235;
            namedentities["euro"] = 8364;
            namedentities["exists"] = 8707;
            namedentities["fnof"] = 402;
            namedentities["forall"] = 8704;
            namedentities["frac12"] = 189;
            namedentities["frac14"] = 188;
            namedentities["frac34"] = 190;
            namedentities["gamma"] = 947;
            namedentities["ge"] = 8805;
            namedentities["harr"] = 8596;
            namedentities["hearts"] = 9829;
            namedentities["hellip"] = 8230;
            namedentities["iacute"] = 237;
            namedentities["icirc"] = 238;
            namedentities["iexcl"] = 161;
            namedentities["igrave"] = 236;
            namedentities["infin"] = 8734;
            namedentities["int"] = 8747;
            namedentities["iota"] = 953;
            namedentities["iquest"] = 191;
            namedentities["isin"] = 8712;
            namedentities["iuml"] = 239;
            namedentities["kappa"] = 954;
            namedentities["lambda"] = 923;
            namedentities["laquo"] = 171;
            namedentities["larr"] = 8592;
            namedentities["lceil"] = 8968;
            namedentities["ldquo"] = 8220;
            namedentities["le"] = 8804;
            namedentities["lfloor"] = 8970;
            namedentities["lowast"] = 8727;
            namedentities["loz"] = 9674;
            namedentities["lrm"] = 8206;
            namedentities["lsaquo"] = 8249;
            namedentities["lsquo"] = 8216;
            namedentities["macr"] = 175;
            namedentities["mdash"] = 8212;
            namedentities["micro"] = 181;
            namedentities["middot"] = 183;
            namedentities["minus"] = 8722;
            namedentities["mu"] = 956;
            namedentities["nabla"] = 8711;
            namedentities["nbsp"] = 160;
            namedentities["ndash"] = 8211;
            namedentities["ne"] = 8800;
            namedentities["ni"] = 8715;
            namedentities["not"] = 172;
            namedentities["notin"] = 8713;
            namedentities["nsub"] = 8836;
            namedentities["ntilde"] = 241;
            namedentities["nu"] = 925;
            namedentities["oacute"] = 243;
            namedentities["ocirc"] = 244;
            namedentities["oelig"] = 339;
            namedentities["ograve"] = 242;
            namedentities["oline"] = 8254;
            namedentities["omega"] = 969;
            namedentities["omicron"] = 959;
            namedentities["oplus"] = 8853;
            namedentities["or"] = 8744;
            namedentities["ordf"] = 170;
            namedentities["ordm"] = 186;
            namedentities["oslash"] = 248;
            namedentities["otilde"] = 245;
            namedentities["otimes"] = 8855;
            namedentities["ouml"] = 246;
            namedentities["para"] = 182;
            namedentities["part"] = 8706;
            namedentities["permil"] = 8240;
            namedentities["perp"] = 8869;
            namedentities["phi"] = 966;
            namedentities["pi"] = 960;
            namedentities["piv"] = 982;
            namedentities["plusmn"] = 177;
            namedentities["pound"] = 163;
            namedentities["prime"] = 8242;
            namedentities["prod"] = 8719;
            namedentities["prop"] = 8733;
            namedentities["psi"] = 968;
            namedentities["radic"] = 8730;
            namedentities["raquo"] = 187;
            namedentities["rarr"] = 8594;
            namedentities["rceil"] = 8969;
            namedentities["rdquo"] = 8221;
            namedentities["reg"] = 174;
            namedentities["rfloor"] = 8971;
            namedentities["rho"] = 961;
            namedentities["rlm"] = 8207;
            namedentities["rsaquo"] = 8250;
            namedentities["rsquo"] = 8217;
            namedentities["sbquo"] = 8218;
            namedentities["scaron"] = 353;
            namedentities["sdot"] = 8901;
            namedentities["sect"] = 167;
            namedentities["shy"] = 173;
            namedentities["sigma"] = 963;
            namedentities["sigmaf"] = 962;
            namedentities["sim"] = 8764;
            namedentities["spades"] = 9824;
            namedentities["sub"] = 8834;
            namedentities["sube"] = 8838;
            namedentities["sum"] = 8721;
            namedentities["sup"] = 8835;
            namedentities["sup1"] = 185;
            namedentities["sup3"] = 179;
            namedentities["supe"] = 8839;
            namedentities["szlig"] = 223;
            namedentities["tau"] = 964;
            namedentities["there4"] = 8756;
            namedentities["theta"] = 952;
            namedentities["thetasym"] = 977;
            namedentities["thinsp"] = 8201;
            namedentities["thorn"] = 254;
            namedentities["tilde"] = 732;
            namedentities["times"] = 215;
            namedentities["trade"] = 8482;
            namedentities["uacute"] = 250;
            namedentities["uarr"] = 8593;
            namedentities["ucirc"] = 251;
            namedentities["ugrave"] = 249;
            namedentities["uml"] = 168;
            namedentities["up2"] = 178;
            namedentities["upsih"] = 978;
            namedentities["upsilon"] = 965;
            namedentities["uuml"] = 252;
            namedentities["xi"] = 958;
            namedentities["yacute"] = 253;
            namedentities["yen"] = 165;
            namedentities["yuml"] = 255;
            namedentities["zeta"] = 950;
            namedentities["zwj"] = 8205;
            namedentities["zwnj"] = 8204;
            emptytags.Add("area");
            emptytags.Add("base");
            emptytags.Add("basefont");
            emptytags.Add("br");
            emptytags.Add("col");
            emptytags.Add("frame");
            emptytags.Add("hr");
            emptytags.Add("img");
            emptytags.Add("input");
            emptytags.Add("isindex");
            emptytags.Add("link");
            emptytags.Add("meta");
            emptytags.Add("param");
            autoclosetags["basefont"] = new List<string>();
            autoclosetags["basefont"].Add("basefont");
            autoclosetags["colgroup"] = new List<string>();
            autoclosetags["colgroup"].Add("colgroup");
            autoclosetags["dd"] = new List<string>();
            autoclosetags["dd"].Add("colgroup");
            autoclosetags["dt"] = new List<string>();
            autoclosetags["dt"].Add("dt");
            autoclosetags["li"] = new List<string>();
            autoclosetags["li"].Add("li");
            autoclosetags["p"] = new List<string>();
            autoclosetags["p"].Add("p");
            autoclosetags["thead"] = new List<string>();
            autoclosetags["thead"].Add("tbody");
            autoclosetags["thead"].Add("tfoot");
            autoclosetags["tbody"] = new List<string>();
            autoclosetags["tbody"].Add("thead");
            autoclosetags["tbody"].Add("tfoot");
            autoclosetags["tfoot"] = new List<string>();
            autoclosetags["tfoot"].Add("thead");
            autoclosetags["tfoot"].Add("tbody");
            autoclosetags["th"] = new List<string>();
            autoclosetags["th"].Add("td");
            autoclosetags["td"] = new List<string>();
            autoclosetags["td"].Add("th");
            autoclosetags["td"].Add("td");
            autoclosetags["tr"] = new List<string>();
            autoclosetags["tr"].Add("tr");
        }
        public string Capture(string url)
        {
            string s = null;
            int nb_essais;
            StreamReader loResponseStream = null;
            HttpWebResponse HttpWResp = null;
            for (nb_essais = 10; nb_essais != 0; nb_essais--)
            {
                try
                {
                    HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
                    HttpWReq.Method = "GET";
                    HttpWReq.ContentType = "application/x-www-form-urlencoded";
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                    Encoding enc1252 = Encoding.GetEncoding(1252);
                    loResponseStream = new StreamReader(HttpWResp.GetResponseStream(), enc1252);
                    s = loResponseStream.ReadToEnd();
                    loResponseStream.Close();
                    try
                    {
                        string xs = Html2Xml(s);
                        Regex normspace = new Regex(@"[\f\n\r\t\v\x85\p{Z}\xA0]+");
                        Regex xmlnsrem = new Regex(" xmlns=\"[^\"]*\"");
                        xDoc.LoadXml(xmlnsrem.Replace(normspace.Replace(Html2Xml(s), " "), ""));
                        HttpWResp.Close();
                        return xDoc.OuterXml;
                    }
                    catch
                    {
                    }
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(nb_essais < 3 ? 1000 * 10 : 1000 * 5);
                }
                finally
                {
                    try
                    {
                        HttpWResp.Close();
                    }
                    catch
                    {
                    }
                }
            }
            return "(error)";
        }
        private string Html2Xml(string s)
        {
            string r2 = "";
            string r = "";
            int limit = s.Length;
            states state = states.text;
            states prevstate = state;
            Stack<string> opentags = new Stack<string>();
            string name = "";
            string tagname = "";
            string attrname = "";
            string attrs = "";
            List<string> attrnames = new List<string>();
            int entvalue = 0;
            char attrdelim = '"';
            string attrvalue = "";
            string cs = "";
            char prec = ' ';
            char preprec = ' ';
            char c = ' ';
            int start = 0;
            string encoding = "";
            if (s.Substring(0, 3) == "\0xEF\0xBB\0xBF")
            {
                encoding = "utf-8";
                start = 3;
            }
            else
            {
                encoding = "iso-8859-1";
                start = 0;
            }
            for (int i = start; i < limit && ((r2 == "" && r == "") || opentags.Count != 0); i++)
            {
                if (r.Length > 10240)
                {
                    r2 += r;
                    r = "";
                }
                c = s[i];
                switch (state)
                {
                    case states.text:
                        if (c == '<')
                        {
                            name = "";
                            tagname = "";
                            attrname = "";
                            attrs = "";
                            attrnames.Clear();
                            state = states.tag;
                            break;
                        }
                        if (!Char.IsWhiteSpace(c) && opentags.Count == 0)
                        {
                            r += "<html>";
                            opentags.Push("html");
                        }
                        if (Char.IsWhiteSpace(c) && opentags.Count == 0)
                        {
                            break;
                        }
                        if (c == '&')
                        {
                            name = "";
                            entvalue = 0;
                            prevstate = state;
                            state = states.entity;
                            break;
                        }
                        r += c;
                        break;
                    case states.tag:
                        if (c == '?' && tagname == "")
                        {
                            state = states.tillinst;
                            break;
                        }
                        if (c == '!' && tagname == "")
                        {
                            state = states.specialtag;
                            prec = ' ';
                            break;
                        }
                        if (c == '/' && name == "" && tagname == "")
                        {
                            state = states.endtag;
                            name = "";
                            break;
                        }
                        if (Char.IsWhiteSpace(c))
                        {
                            if (name == "")
                            {
                                break;
                            }
                            if (tagname == "" && name != "_")
                            {
                                tagname = name;
                                name = "";
                                break;
                            }
                            if (attrname == "")
                            {
                                attrname = name.ToLower();
                                name = "";
                                break;
                            }
                            break;
                        }
                        if (c == '=')
                        {
                            if (attrname == "")
                            {
                                attrname = name.ToLower();
                                name = "";
                            }
                            state = states.tillquote;
                            break;
                        }
                        if (c == '/' && (tagname != "" || name != ""))
                        {
                            if (tagname == "")
                            {
                                tagname = name;
                            }
                            tagname = tagname.ToLower();
                            if (tagname != "html" && opentags.Count == 0)
                            {
                                r += "<html>";
                                opentags.Push("html");
                            }
                            if (autoclosetags.ContainsKey(tagname) && opentags.Count > 0)
                            {
                                string prevtag = opentags.Peek();
                                if (autoclosetags[tagname].Contains(prevtag))
                                {
                                    opentags.Pop();
                                    r += "</" + prevtag + ">";
                                }
                            }
                            if (tagname == "tr" && opentags.Peek() == "table")
                            {
                                r += "<tbody>";
                                opentags.Push("tbody");
                            }
                            r += "<" + tagname + attrs + "/>";
                            state = states.tillgt;
                            break;
                        }
                        if (c == '>')
                        {
                            if (tagname == "" && name != "")
                            {
                                tagname = name;
                            }
                            if (tagname != "")
                            {
                                tagname = tagname.ToLower();
                                if (tagname != "html" && opentags.Count == 0)
                                {
                                    r += "<html>";
                                    opentags.Push("html");
                                }
                                if (autoclosetags.ContainsKey(tagname) && opentags.Count > 0)
                                {
                                    string prevtag = opentags.Peek();
                                    if (autoclosetags[tagname].Contains(prevtag))
                                    {
                                        opentags.Pop();
                                        r += "</" + prevtag + ">";
                                    }
                                }
                                if (tagname == "tr" && opentags.Peek() == "table")
                                {
                                    r += "<tbody>";
                                    opentags.Push("tbody");
                                }
                                if (emptytags.Contains(tagname))
                                {
                                    r += "<" + tagname.ToLower() + attrs + "/>";
                                }
                                else
                                {
                                    opentags.Push(tagname);
                                    r += "<" + tagname + attrs + ">";
                                    if (tagname == "script")
                                    {
                                        r += "<![CDATA[";
                                        opentags.Pop();
                                        state = states.script;
                                        break;
                                    }
                                }
                                state = states.text;
                                break;
                            }
                        }
                        if (attrname != "")
                        {
                            attrs += " " + attrname + "=\"" + attrname + "\"";
                            attrname = "";
                        }
                        cs = "" + c;
                        name += (Char.IsLetterOrDigit(c) && name != "") || Char.IsLetter(c) ? cs : (name == "" ? "_" : (c == '-' ? "-" : (name != "_" ? "_" : "")));
                        break;
                    case states.endtag:
                        if (c == '>')
                        {
                            name = name.ToLower();
                            if (opentags.Contains(name))
                            {
                                string prevtag;
                                while ((prevtag = opentags.Pop()) != name)
                                {
                                    r += "</" + prevtag + ">";
                                }
                                r += "</" + name + ">";
                            }
                            else
                            {
                                if (name != "html" && opentags.Count == 0)
                                {
                                    r += "<html>";
                                    opentags.Push("html");
                                }
                            }
                            state = states.text;
                            break;
                        }
                        if (Char.IsWhiteSpace(c))
                        {
                            break;
                        }
                        cs = "" + c;
                        name += Char.IsLetterOrDigit(c) ? cs : name != "_" ? "_" : "";
                        break;
                    case states.attrtext:
                        if (c == attrdelim || (Char.IsWhiteSpace(c) && attrdelim == ' '))
                        {
                            if (!attrnames.Contains(attrname))
                            {
                                attrnames.Add(attrname);
                                attrs += " " + attrname + "=\"" + attrvalue + "\"";
                            }
                            attrname = "";
                            state = states.tag;
                            break;
                        }
                        if (attrdelim == ' ' && (c == '/' || c == '>'))
                        {
                            tagname = tagname.ToLower();
                            if (tagname != "html" && opentags.Count == 0)
                            {
                                r += "<html>";
                                opentags.Push("html");
                            }
                            if (autoclosetags.ContainsKey(tagname) && opentags.Count > 0)
                            {
                                string prevtag = opentags.Peek();
                                if (autoclosetags[tagname].Contains(prevtag))
                                {
                                    opentags.Pop();
                                    r += "</" + prevtag + ">";
                                }
                            }
                            if (!attrnames.Contains(attrname))
                            {
                                attrnames.Add(attrname);
                                attrs += " " + attrname + "=\"" + attrvalue + "\"";
                            }
                            attrname = "";
                            if (c == '/')
                            {
                                r += "<" + tagname + attrs + "/>";
                                state = states.tillgt;
                                break;
                            }
                            if (c == '>')
                            {
                                if (emptytags.Contains(tagname))
                                {
                                    r += "<" + tagname + attrs + "/>";
                                    state = states.text;
                                    break;
                                }
                                else
                                {
                                    opentags.Push(tagname);
                                    r += "<" + tagname + attrs + ">";
                                    if (tagname == "script")
                                    {
                                        r += "<![CDATA[";
                                        opentags.Pop();
                                        prec = ' ';
                                        preprec = ' ';
                                        state = states.script;
                                        break;
                                    }
                                    state = states.text;
                                    break;
                                }
                            }
                        }
                        if (c == '&')
                        {
                            name = "";
                            entvalue = 0;
                            prevstate = state;
                            state = states.entity;
                            break;
                        }
                        cs = "" + c;
                        attrvalue += c == '"' ? "&quot;" : c == '\'' ? "&apos;" : cs;
                        break;
                    case states.script:
                        if (c == '/' && prec == '<')
                        {
                            state = states.endscript;
                            name = "";
                            break;
                        }
                        if (c == '[' && prec == '!' && preprec == '<')
                        {
                            state = states.skipcdata;
                            name = "<![";
                            break;
                        }
                        if (c == '>' && prec == ']' && preprec == ']')
                        {
                            c = r[r.Length - 3];
                            r = r.Substring(0, r.Length - 4);
                        }
                        r += c;
                        preprec = prec;
                        prec = c;
                        break;
                    case states.endscript:
                        if (c == '>' && name.ToLower() == "script")
                        {
                            r = r.Substring(0, r.Length - 1);
                            r += "]]></script>";
                            state = states.text;
                            break;
                        }
                        name += c;
                        string sscr = "script";
                        if (!sscr.StartsWith(name.ToLower()))
                        {
                            r += name;
                            state = states.script;
                        }
                        break;
                    case states.specialtag:
                        if (c != '-')
                        {
                            state = states.tillgt;
                            break;
                        }
                        if (prec == '-')
                        {
                            state = states.comment;
                            preprec = ' ';
                            break;
                        }
                        prec = c;
                        break;
                    case states.comment:
                        if (c == '>' && prec == '-' && preprec == '-')
                        {
                            state = states.text;
                            break;
                        }
                        preprec = prec;
                        prec = c;
                        break;
                    case states.skipcdata:
                        if (name == "<![CDATA[")
                        {
                            state = states.script;
                            break;
                        }
                        name += c;
                        string scdata = "<![CDATA[";
                        if (!scdata.StartsWith(name))
                        {
                            r += name;
                            state = states.script;
                        }
                        break;
                    case states.entity:
                        if (c == '#')
                        {
                            state = states.numericentity;
                            break;
                        }
                        name += c;
                        state = states.namedentity;
                        break;
                    case states.numericentity:
                        if (c == 'x' || c == 'X')
                        {
                            state = states.hexaentity;
                            break;
                        }
                        if (c == ';')
                        {
                            string ent = "&#" + entvalue + ";";
                            if (prevstate == states.text)
                            {
                                r += ent;
                            }
                            else
                            {
                                attrvalue += ent;
                            }
                            state = prevstate;
                            break;
                        }
                        entvalue = entvalue * 10 + c - '0';
                        break;
                    case states.hexaentity:
                        if (c == ';')
                        {
                            string ent = "&#" + entvalue + ";";
                            if (prevstate == states.text)
                            {
                                r += ent;
                            }
                            else
                            {
                                attrvalue += ent;
                            }
                            state = prevstate;
                            break;
                        }
                        entvalue = entvalue * 16 + (Char.IsDigit(c) ? c - '0' : Char.ToUpper(c) - 'A');
                        break;
                    case states.namedentity:
                        if (c == ';')
                        {
                            string ent;
                            name = name.ToLower();
                            if (name == "amp" || name == "lt" || name == "gt" || name == "quot" || name == "apos")
                            {
                                ent = "&" + name + ";";
                                name = "";
                                if (prevstate == states.text)
                                {
                                    r += ent;
                                }
                                else
                                {
                                    attrvalue += ent;
                                }
                                state = prevstate;
                                break;
                            }
                            namedentities.TryGetValue(name, out entvalue);
                            ent = "&#" + entvalue + ";";
                            name = "";
                            if (prevstate == states.text)
                            {
                                r += ent;
                            }
                            else
                            {
                                attrvalue += ent;
                            }
                            state = prevstate;
                            break;
                        }
                        if (!Char.IsLetterOrDigit(c) || name.Length > 6)
                        {
                            string ent = "&amp;" + name;
                            name = "";
                            if (prevstate == states.text)
                            {
                                r += ent;
                            }
                            else
                            {
                                attrvalue += ent;
                            }
                            state = prevstate;
                            i--;
                            break;
                        }
                        name += c;
                        break;
                    case states.tillinst:
                        if (c == '?')
                        {
                            state = states.andgt;
                        }
                        break;
                    case states.andgt:
                        if (c == '>')
                        {
                            state = states.text;
                            break;
                        }
                        state = states.tillinst;
                        break;
                    case states.tillgt:
                        if (c == '>')
                        {
                            state = states.text;
                        }
                        break;
                    case states.tillquote:
                        if (Char.IsWhiteSpace(c))
                        {
                            break;
                        }
                        if (c == '"' || c == '\'')
                        {
                            attrdelim = c;
                            attrvalue = "";
                            state = states.attrtext;
                            break;
                        }
                        if (c == '/' || c == '>')
                        {
                            attrs += " " + attrname + "=\"" + attrname + "\"";
                        }
                        if (c == '/')
                        {
                            r += "<" + tagname.ToLower() + attrs + "/>";
                            state = states.tillgt;
                            break;
                        }
                        if (c == '>')
                        {
                            tagname = tagname.ToLower();
                            if (tagname != "html" && opentags.Count == 0)
                            {
                                r += "<html>";
                                opentags.Push("html");
                            }
                            if (autoclosetags.ContainsKey(tagname) && opentags.Count > 0)
                            {
                                string prevtag = opentags.Peek();
                                if (autoclosetags[tagname].Contains(prevtag))
                                {
                                    opentags.Pop();
                                    r += "</" + prevtag + ">";
                                }
                            }
                            if (emptytags.Contains(tagname))
                            {
                                r += "<" + tagname + attrs + "/>";
                                state = states.text;
                                break;
                            }
                            else
                            {
                                opentags.Push(tagname);
                                r += "<" + tagname + attrs + ">";
                                if (tagname == "script")
                                {
                                    r += "<![CDATA[";
                                    opentags.Pop();
                                    state = states.script;
                                    break;
                                }
                            }
                        }
                        attrdelim = ' ';
                        attrvalue = "" + c;
                        state = states.attrtext;
                        break;
                }
            }
            while (opentags.Count != 0)
            {
                r += "</" + opentags.Pop() + ">";
            }
            r2 += r;
            return "<?xml version=\"1.0\" encoding=\"" + encoding + "\"?>\n" + r2;
        }
        public string Query(string q)
        {
            XmlNode n = xDoc.DocumentElement.SelectSingleNode(q);
            return n == null ? "(not found)" : n.InnerText.Trim();
        }
    }
}
