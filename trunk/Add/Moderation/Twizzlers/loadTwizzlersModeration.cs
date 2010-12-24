#region

using System;
using System.Collections.Generic;
using System.Text;
using umbraco.BusinessLogic.Actions;
using umbraco.cms.presentation.Trees;
using umbraco.interfaces;

#endregion

namespace UmbracoAddons.Moderation.Twizzlers
{
    public class loadTwizzlersModeration : BaseTree
    {
        public loadTwizzlersModeration(string application)
            : base(application)
        {
        }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.Icon = FolderIcon;
            rootNode.OpenIcon = FolderIconOpen;
            rootNode.NodeType = "init" + TreeAlias;
            rootNode.NodeID = "init";
        }

        protected override void CreateRootNodeActions(ref List<IAction> actions)
        {
            actions.Clear();
            actions.Add(ActionRefresh.Instance);
        }

        protected override void CreateAllowedActions(ref List<IAction> actions)
        {
            actions.Clear();
            actions.Add(ActionRefresh.Instance);
        }

        public override void Render(ref XmlTree Tree)
        {
            XmlTreeNode ndWhatTheySaid = XmlTreeNode.Create(this);
            ndWhatTheySaid.Text = "Tell Us";
            ndWhatTheySaid.Action = string.Empty;
            ndWhatTheySaid.Icon = "doc.gif";
            ndWhatTheySaid.NodeID = "tellus";
            ndWhatTheySaid.Action = "JavaScript:openWhatTheySaid()";

            OnBeforeNodeRender(ref Tree, ref ndWhatTheySaid, EventArgs.Empty);
            if (ndWhatTheySaid != null)
            {
                Tree.Add(ndWhatTheySaid);
            }
            OnAfterNodeRender(ref Tree, ref ndWhatTheySaid, EventArgs.Empty);
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
                @"
                function openWhatTheySaid() {
                    parent.right.document.location.href = '/UmbracoAddons/Moderation/Twizzlers/TellUs.aspx';
                }
			");
        }
    }
}