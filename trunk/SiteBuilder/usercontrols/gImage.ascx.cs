using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using umbraco.presentation.nodeFactory;

namespace SiteBuilder.usercontrols
{
    public partial class gImage : System.Web.UI.UserControl
    {
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AlbumName { get; set; }
        public string Field { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool Recursive { get; set; }
        public gImage()
        {
            this.ImageUrl = "";
            this.Field = "MainImage";
            this.UserName = "eulalie367@gmail.com";
            this.Password = "edgarallen";
            this.AlbumName = "Staged";
            this.Height = 0;
            this.Width = 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Field))
            {
                Image1.ImageUrl = this.ImageUrl;
                Node current = SiteBuilder.Helpers.Umbraco.LoadCurrentNode();
                Property pUrl = null;
                Node n = current;
                if (this.Recursive)
                    while (pUrl == null)
                    {
                        pUrl = n.GetProperty(this.Field);
                        if ((pUrl == null || string.IsNullOrEmpty(pUrl.Value)) && n.Parent != null)
                        {
                            n = n.Parent;
                            pUrl = null;
                        }
                        else
                            break;
                    }
                else
                    pUrl = n.GetProperty(this.Field);

                if (pUrl != null)
                {
                    string sUrl = pUrl.Value;
                    if (!string.IsNullOrEmpty(sUrl))
                        Image1.ImageUrl = sUrl;
                    else if (!string.IsNullOrEmpty(this.ImageUrl))
                        Image1.ImageUrl = this.ImageUrl;
                }
            }
            else if (!string.IsNullOrEmpty(this.ImageUrl))
                Image1.ImageUrl = this.ImageUrl;

            Image1.UserName = this.UserName;
            Image1.Password = this.Password;
            Image1.AlbumName = this.AlbumName;
            if (this.Height > 0)
                Image1.Height = this.Height;
            if (this.Width > 0)
                Image1.Width = this.Width;
        }
    }
}