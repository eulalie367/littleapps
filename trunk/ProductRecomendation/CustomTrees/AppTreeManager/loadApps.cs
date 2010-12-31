using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using umbraco.cms.presentation.Trees;
using System.Collections.Generic;
using umbraco.BusinessLogic.Actions;
using ProlificNotion.Umbraco.AppManager;
using umbraco.interfaces;
using System.Data.SqlClient;
using System.Linq;
using umbraco;
using System.Text;

namespace ProductRecomendation.CustomTrees.AppTreeManager
{
    public class loadApps : BaseTree
    {
        // Methods
        public loadApps(string application)
            : base(application)
        {
        }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.Icon = ".sprTreeFolder";
            rootNode.OpenIcon = ".sprTreeFolder_o";
            rootNode.NodeType = this.TreeAlias;
            rootNode.NodeID = "init";
            rootNode.Menu.Clear();
            List<IAction> collection = new List<IAction>();
            collection.Add(ActionCreateApp.Instance);
            collection.Add(ContextMenuSeperator.Instance);
            collection.Add(ActionRefresh.Instance);
            rootNode.Menu.AddRange(collection);
        }

        public override void Render(ref XmlTree tree)
        {
            XmlTreeNode node;
            if (base.NodeKey == string.Empty)
            {
                SqlDataReader reader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(GlobalSettings.DbDSN, CommandType.Text, "SELECT * FROM UmbracoApp ORDER BY sortOrder");
                while (reader.Read())
                {
                    node = XmlTreeNode.Create(this);
                    node.NodeID = reader["appAlias"].ToString();
                    node.Text = reader["appAlias"].ToString();
                    node.Action = "javascript:openUrl('/umbraco/plugins/appmanager/editApp.aspx?appAlias=" + reader["appAlias"] + "')";
                    node.Icon = "application_cascade.png";
                    node.OpenIcon = "application_cascade.png";
                    node.Menu.Clear();
                    List<IAction> collection = new List<IAction>();
                    collection.Add(ActionCreateAppTree.Instance);
                    collection.Add(ContextMenuSeperator.Instance);
                    collection.Add(ActionRefresh.Instance);
                    node.Menu.AddRange(collection);
                    if (!FailSafe.ProtectedApps.Contains<string>(node.Text))
                    {
                        node.Menu.Insert(2, ActionDeleteApp.Instance);
                    }
                    node.Source = new TreeService(-1, this.TreeAlias, new bool?(base.ShowContextMenu), new bool?(base.IsDialog), base.DialogMode, this.app, reader["appAlias"].ToString()).GetServiceUrl();
                    tree.Add(node);
                }
            }
            else
            {
                SqlDataReader reader2 = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(GlobalSettings.DbDSN, CommandType.Text, string.Format("SELECT * FROM umbracoAppTree WHERE appAlias = '{0}' ORDER BY treeSortOrder", base.NodeKey));
                while (reader2.Read())
                {
                    node = XmlTreeNode.Create(this);
                    node.NodeID = reader2["treeAlias"].ToString();
                    node.Text = reader2["treeAlias"].ToString();
                    node.Action = "javascript:openUrl('/umbraco/plugins/appmanager/editAppTree.aspx?treeAlias=" + node.Text + "')";
                    node.Icon = "application.png";
                    node.OpenIcon = "application.png";
                    node.Source = string.Empty;
                    node.Menu.Clear();
                    if (!FailSafe.ProtectedAppTrees.Contains<string>(node.Text))
                    {
                        node.Menu.Add(ActionDeleteAppTree.Instance);
                    }
                    node.Menu.Add(ActionRefresh.Instance);
                    tree.Add(node);
                }
            }
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.AppendLine("function openUrl(url){");
            Javascript.AppendLine("\tparent.right.document.location.href = url;");
            Javascript.AppendLine("}");
        }
    }

}
