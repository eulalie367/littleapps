using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace SolutionMatchTool.Data
{
    public static class Extensions
    {
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
                        return true;
                        break;
                    default:
                        return null;
                        break;
                }
            }
            else
                return null;
        }
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
        public static void FillSelect<t>(this HtmlSelect select, t source, string nameCol, string valCol) where t : IEnumerable
        {
            select.DataSource = source;
            select.DataTextField = nameCol;
            select.DataValueField = valCol;
            select.DataBind();
        }
    }
}
