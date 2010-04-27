using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;
using Google.GData.Photos;


namespace gImage
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), DefaultProperty("ImageUrl"), ToolboxData("<{0}:Image runat=\"server\"> </{0}:Image>")]
    public class Image : System.Web.UI.WebControls.Image
    {
        [Bindable(false), Category("Appearance"), Description("The Gmail Address to use."), DefaultValue(""), Localizable(true)]
        public string UserName { get; set; }
        [Bindable(false), Category("Appearance"), Description("The Gmail Password to use."), DefaultValue(""), Localizable(true)]
        public string Password { get; set; }
        [Bindable(false), Category("Appearance"), Description("The Picasa Album to store images."), DefaultValue("gImage"), Localizable(true)]
        public string AlbumName { get; set; }
        [Bindable(false), Category("Appearance"), Description("Use cacheing to speed up calls to Picasa."), DefaultValue(true), Localizable(true)]
        public bool UseCache { get; set; }

        public Image()
        {
            this.UseCache = true;
            this.AlbumName = "gImage";
            this.Password = "";
            this.UserName = "";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.ImageUrl))
            {
                GoogleImageModule im = new GoogleImageModule(this.UserName, this.Password, this.ImageUrl);
                im.UseCache = this.UseCache;
                PicasaEntry image = im.GetImage(UserName, AlbumName);

                if (!this.Height.IsEmpty || !this.Width.IsEmpty)
                {
                    var thumbs = image.Media.Thumbnails.Where(t => (this.Height.IsEmpty || !string.IsNullOrEmpty(t.Height) && int.Parse(t.Height) >= this.Height.Value) && (this.Width.IsEmpty || !string.IsNullOrEmpty(t.Width) && int.Parse(t.Width) >= this.Width.Value)).ToList();

                    if(thumbs.Count > 0)
                        this.ImageUrl = thumbs.OrderBy(t => t.Height).Select(t => t.Url).FirstOrDefault();
                    else
                        this.ImageUrl = image.Media.Content.Url;

                }
                else
                    this.ImageUrl = image.Media.Content.Url;
            }
            base.Render(writer);
        }
    }
}
