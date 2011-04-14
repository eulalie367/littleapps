using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco;
using umbraco.NodeFactory;

namespace SBWS.usercontrols.Global
{
    public enum MenuType : int
    {
        Single_Level_FromRoot = 0,
        Single_Level_FromCurrent = 1,
        Single_Level_FromSpecific = 2
    }
    public partial class Menu : System.Web.UI.UserControl
    {
        public MenuType Type { get; set; }
        public int ParentNodeID { get; set; }
        public string SelectedClass { get; set; }
        public string Seperator { get; set; }
        public string HomeName { get; set; }
        
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
            this.SelectedClass = "selected";
            this.Seperator = "";
            this.ParentNodeID = 0;
            this.HomeName = "Home";
        }

        private Node node;
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (this.Type)
            {
                case MenuType.Single_Level_FromRoot:
                default:
                    node = currentNode.GetRootNode();
                    LoadHome(node);
                    break;
                case MenuType.Single_Level_FromCurrent:
                    node = currentNode;
                    break;
                case MenuType.Single_Level_FromSpecific:
                    node = currentNode.GetRootNode().ChildrenAsList.Where(n => n.Id == ParentNodeID).FirstOrDefault() as Node;
                    break;
            }

            menuItems.DataSource = node.Children;
            menuItems.DataBind();
        }


        #region ClientBindings
        private void LoadHome(Node node)
        {
            liHome.Visible = true;
            spHome.InnerText = HomeName;
            aHome.HRef = node.Url;
            if (currentNode.Id == node.Id)
                aHome.Attributes.AddSafely("class", SelectedClass);
        }

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
            if (!string.IsNullOrEmpty(this.Seperator) && node.Children.Count - 1 != container.ItemIndex)
            {
                return "<li class=\"spacer\"><span>" + this.Seperator + "</span></li>";
            }
            return "";
        }

        #endregion
    }
}