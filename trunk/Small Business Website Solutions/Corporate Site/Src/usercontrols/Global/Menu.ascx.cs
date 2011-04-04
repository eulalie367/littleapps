using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;

namespace SBWS.usercontrols.Global
{
    public enum MenuType : int
    {
        Single_Level_FromRoot = 0,
        Single_Level_FromCurrent = 1
    }
    public partial class Menu : System.Web.UI.UserControl
    {
        public MenuType Type { get; set; }
        public string RootName { get; set; }
        public string SelectedClass { get; set; }
        public string SiteName { get; set; }
        
        private Node _currentNode;
        private Node currentNode
        {
            get
            {
                if(_currentNode == null)
                    _currentNode = Node.GetCurrent();
    
                return _currentNode;
            }
        }
        public Menu()
        {
            this.Type = MenuType.Single_Level_FromRoot;
            this.RootName = "Home";
            this.SelectedClass = "selected";
            this.SiteName = "CorporateSite";
        }
        private Node node;
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (this.Type)
            {
                case MenuType.Single_Level_FromRoot:
                default:
                    umbraco.cms.businesslogic.web.Document root = LoadRootNode(SiteName);
                    node = new Node(root.Id);
                    LoadHome(node);
                    break;
                case MenuType.Single_Level_FromCurrent:
                    node = currentNode;
                    break;
            }

            menuItems.DataSource = node.Children;
            menuItems.DataBind();
        }

        private void LoadHome(Node node)
        {
            liHome.Visible = true;
            aHome.InnerText = RootName;
            aHome.HRef = node.Url;
            if (currentNode.Id == node.Id)
                aHome.Attributes.AddSafely("class", SelectedClass);
        }

        private umbraco.cms.businesslogic.web.Document LoadRootNode(string siteName)
        {
            umbraco.cms.businesslogic.web.Document[] sites = umbraco.cms.businesslogic.web.Document.GetRootDocuments();
            umbraco.cms.businesslogic.web.Document site = sites.Where(s => s.Text == siteName).FirstOrDefault();


            return site ?? sites.FirstOrDefault() ?? new umbraco.cms.businesslogic.web.Document(-1);
        }

        #region ClientBindings

        public string selected(RepeaterItem container)
        {
            if (currentNode.Id == (DataBinder.Eval(container.DataItem, "Id").ToString().ToInt() ?? 0))
            {
                return "class=\"" + SelectedClass + "\"";
            }
            return "";
        }
        public string seperator(RepeaterItem container)
        {
            if (node.Children.Count - 1 != container.ItemIndex)
            {
                return "<li class=\"spacer\">|</li>";
            }
            return "";
        }

        #endregion
    }
}