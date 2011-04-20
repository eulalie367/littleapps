using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Web.UI;
using System.Net;

namespace System
{
    public static class Extensions
    {
        /// <summary>
        /// Parses a request string and returns it's nullable int value
        /// </summary>
        public static int? ToInt(this HttpRequest req, string key)
        {
            int? retVal = null;
            try
            {
                int tmp = -1;
                if (req[key] != null && !string.IsNullOrEmpty(req[key]))
                    if (int.TryParse(req[key], out tmp))
                        retVal = tmp;
            }
            catch
            { }
            return retVal;
        }
        /// <summary>
        /// Parses a request string and returns it's nullable float value
        /// </summary>
        public static float? ToFloat(this HttpRequest req, string key)
        {
            float? retVal = null;
            try
            {
                float tmp = 0;
                if (req[key] != null && !string.IsNullOrEmpty(req[key]))
                    if (float.TryParse(req[key], out tmp))
                        retVal = tmp;
            }
            catch
            { }
            return retVal;
        }
        /// <summary>
        /// Parses a string and returns it's nullable int value
        /// </summary>
        public static int? ToInt(this string str)
        {
            int? retVal = null;
            try
            {
                int tmp = -1;
                if (!string.IsNullOrEmpty(str))
                    if (int.TryParse(str, out tmp))
                        retVal = tmp;
            }
            catch
            { }
            return retVal;
        }
        /// <summary>
        /// Parses a string and returns it's nullable float value
        /// </summary>
        public static float? ToFloat(this string str)
        {
            float? retVal = null;
            try
            {
                float tmp = -1;
                if (!string.IsNullOrEmpty(str))
                    if (float.TryParse(str, out tmp))
                        retVal = tmp;
            }
            catch
            { }
            return retVal;
        }
        /// <summary>
        /// Parses a string and returns it's nullable datetime value
        /// </summary>
        public static DateTime? ToDateTime(this string str)
        {
            DateTime? retVal = null;
            try
            {
                DateTime tmp;
                if (!string.IsNullOrEmpty(str))
                    if (DateTime.TryParse(str, out tmp))
                        retVal = tmp;
            }
            catch
            { }
            return retVal;
        }
        /// <summary>
        /// Parses a string and returns it's nullable bool value
        /// </summary>
        public static Boolean? ToBool(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                switch (str.ToLower())
                {
                    case "yes":
                        return true;
                        break;
                    case "no":
                        return false;
                        break;
                    case "true":
                        return true;
                        break;
                    case "false":
                        return false;
                        break;
                    case "1":
                        return true;
                        break;
                    case "0":
                        return false;
                        break;
                    default:
                        return null;
                        break;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// This is a helper stub to fill a select in 1 line...
        /// </summary>
        /// <typeparam name="source">IEnumerable object to fill the select with</typeparam>
        /// <param name="nameCol">The parameterName to use for the DataTextField when filling the select</param>
        /// <param name="valCol">The parameterName to use for the DataValueField when filling the select</param>
        /// <param name="index0">The text to display as the first option in the select</param>
        public static void FillSelect<t>(this DropDownList select, t source, string nameCol, string valCol, string index0) where t : IEnumerable
        {
            if (!string.IsNullOrEmpty(index0))
            {
                select.Items.Add(new ListItem(index0, ""));
                select.AppendDataBoundItems = true;
            }
            select.DataSource = source;
            select.DataTextField = nameCol;
            select.DataValueField = valCol;
            select.DataBind();
        }
        /// <summary>
        /// This is a helper stub to fill a select in 1 line...
        /// </summary>
        /// <typeparam name="source">IEnumerable object to fill the select with</typeparam>
        /// <param name="nameCol">The parameterName to use for the DataTextField when filling the select</param>
        /// <param name="valCol">The parameterName to use for the DataValueField when filling the select</param>
        public static void FillSelect<t>(this HtmlSelect select, t source, string nameCol, string valCol) where t : IEnumerable
        {
            select.DataSource = source;
            select.DataTextField = nameCol;
            select.DataValueField = valCol;
            select.DataBind();
        }

        public static string GetItemValue(this XmlAttributeCollection attribs, string name)
        {
            XmlNode n = attribs.GetNamedItem(name);
            if (n != null)
                return n.Value;
            else
                return "";
        }

        public static string GetAttributeValue(this XElement elem, string name)
        {
            XAttribute attr = elem.Attribute(name);
            if (attr != null && !string.IsNullOrEmpty(attr.Value))
                return attr.Value;
            else
                return "";
        }

        public static string GetElementValue(this XmlDocument xDoc, string xpath)
        {
            XmlNode node = xDoc.SelectSingleNode(xpath);
            if (node != null)
            {
                return node.Value;
            }
            return "";
        }
        public static string GetElementValue(this XmlNode xNode, string xpath)
        {
            XmlNode node = xNode.SelectSingleNode(xpath);
            if (node != null)
            {
                return node.InnerText;
            }
            return "";
        }

        public static void AppendAttribute(this System.Web.UI.HtmlControls.HtmlContainerControl cont, string name, string value)
        {
            string a = cont.Attributes[name];
            if (string.IsNullOrEmpty(a))
                cont.Attributes.Add(name, value);
            else
            {
                cont.Attributes[name] += " " + value;
            }
        }

        public static string GetResponseString(this HttpWebRequest req)
        {
            return GetResponseString(req, "");
        }
        public static string GetResponseString(this HttpWebRequest req, string pData)
        {
            if (req != null)
            {
                req.Method = "POST";
                if (!string.IsNullOrEmpty(pData))
                {
                    using (System.IO.Stream reqStream = req.GetRequestStream())
                    {
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        byte[] data = encoding.GetBytes(pData);
                        reqStream.Write(data, 0, data.Length);
                    }
                }
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    using (System.IO.Stream st = resp.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(st))
                        {
                            return sr.ReadToEnd();
                        }
                    }

                }
            }
            return "";
        }

        public static void AddSafely(this AttributeCollection attribs, string key, string value)
        {
            if (attribs != null)
            {
                if (!string.IsNullOrEmpty(attribs[key]))
                {
                    attribs[key] += " " + value;
                }
                else
                {
                    attribs.Add(key, value);
                }
            }
        }
        public static string ToRssDateString(this DateTime dt)
        {
            dt = dt.ToUniversalTime();
            return dt.ToString("ddd, dd MMM yyyy HH:mm:ss G\\MT");
        }

        public static string TruncateWholeWordBetweenTags(this string s, int length, string closingTag)
        {
            if (s.Length >= length)
            {
                s = s.Substring(0, 250);
                s = s.Substring(0, s.LastIndexOf(' '));
                if (!string.IsNullOrEmpty(closingTag))
                    s += closingTag;
            }
            return s;
        }
        public static string TruncateWholeWord(this string s, int length)
        {
            return TruncateWholeWordBetweenTags(s, length, "");
        }
    }
}
