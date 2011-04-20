using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.NodeFactory;
using umbraco.interfaces;

namespace umbraco
{
    public static class UmbracoExtensions
    {
        public static Node GetRootNode(this Node current)
        {
            INode n = current;
            while (n.Level != 1)
                n = n.Parent;

            return n as Node;
        }
    }
}