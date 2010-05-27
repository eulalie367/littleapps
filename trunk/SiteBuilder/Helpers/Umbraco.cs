using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using umbraco.presentation.nodeFactory;

namespace SiteBuilder.Helpers
{
    public class Umbraco
    {
        public Node CurrentNode { get; set; }
        public Umbraco()
        {
            this.CurrentNode = null;
        }
        public Umbraco(Node currentNode)
            : base()
        {
            this.CurrentNode = currentNode;
        }
        public static void SetProperties<t>(t page) where t : Control
        {
            Node current = LoadCurrentNode();
            try
            {
                if (current != null)
                {
                    Type type = page.GetType();
                    if (type != null)
                        foreach (Property prop in current.Properties)
                        {
                            SetProperty(prop, type, page);
                        }
                }
            }
            catch
            {
            }
        }

        public static Node LoadCurrentNode()
        {
            Node current = null;
            try
            {
                current = Node.GetCurrent();
            }
            catch
            {
            }
            return current;
        }
        public static Node LoadMainNode()
        {
            Node current = LoadCurrentNode();
            Node main = current;

            if (current != null)
            {
                for (int i = 0; i <= 4; i++)
                {
                    if (main.Parent != null)
                        main = main.Parent;
                    else
                        break;
                }
            }

            return main;
        }
        public static Node LoadInnerParentNode()
        {
            Node current = LoadCurrentNode();
            Node main = current;

            if (current != null)
            {
                for (int i = 0; i <= 4; i++)
                {
                    if (main.Parent != null && main.NodeTypeAlias == main.Parent.NodeTypeAlias)
                        main = main.Parent;
                    else
                        break;
                }
            }

            return main;
        }
        public static void SetProperty<t>(t page, string Alias) where t : Control
        {
            Node current = LoadCurrentNode();
            try
            {
                if (current != null)
                {
                    Type type = page.GetType();
                    if (type != null)
                    {
                        Property prop = current.GetProperty(Alias);
                        SetProperty(prop, type, page);
                    }
                }
            }
            catch
            {
            }
        }
        private static void SetProperty(Property prop, Type type, object page)
        {
            if (!string.IsNullOrEmpty(prop.Alias))
            {
                System.Reflection.PropertyInfo cProp = type.GetProperty(prop.Alias);
                if (cProp != null)
                    if (!string.IsNullOrEmpty(prop.Value))
                        cProp.SetValue(page, prop.Value, null);
            }
        }

        public static void PrintProperties<t>(t page) where t : Control
        {
            Node current = null;
            try
            {
                current = Node.GetCurrent();
                if (current != null)
                {
                    foreach (Property prop in current.Properties)
                        if (!string.IsNullOrEmpty(prop.Alias))
                            HttpContext.Current.Response.Write("public string " + prop.Alias + " {get;set;}<br/>");
                    foreach (Property prop in current.Properties)
                        if (!string.IsNullOrEmpty(prop.Alias))
                            HttpContext.Current.Response.Write(prop.Alias + " = \"" + prop.Value + "\"<br/>");
                }
            }
            catch
            {
            }
        }



    }








}
