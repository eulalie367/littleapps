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
using Google.GData.Photos;

namespace gImage
{
    internal class GoogleImageModule
    {
        private static int albumcachetime = 60, imagecachetime = 5;
        private PicasaService service;
        private string UserName, Password, ImageUrl;
        private bool isLoggedIn;
        private bool forceUpdate { get; set; }
        internal bool UseCache { get; set; }
        internal GoogleImageModule(string username, string password, string imageurl)
        {
            this.UserName = username;
            this.Password = password;
            this.ImageUrl = imageurl;
            this.isLoggedIn = false;
            this.UseCache = true;
            this.forceUpdate = false;
            this.service = new PicasaService("gImage");
        }

        internal PicasaEntry GetImage(string username, string albumname)
        {
            string key = "gImage" + username + albumname + HttpContext.Current.Server.MapPath(this.ImageUrl);
            PicasaEntry entry = HttpContext.Current.Cache[key] as PicasaEntry;

            if (!UseCache || entry == null)
            {
                string path = HttpContext.Current.Server.MapPath(this.ImageUrl);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);

                albumname = GetAlbumID(username, albumname) ?? "gImage";
                PicasaFeed feed = GetImageFeed(username, albumname);

                entry = feed.Entries.Where(f => (f as PicasaEntry).IsPhoto && f.Summary.Text == this.ImageUrl).FirstOrDefault() as PicasaEntry;

                if (entry != null)
                    this.forceUpdate = entry.Updated.ToUniversalTime() < fileInfo.LastWriteTimeUtc;

                if (entry == null)
                    entry = AddImage(username, albumname);
                else if (this.forceUpdate)
                {
                    entry.Delete();
                    entry = AddImage(username, albumname);
                }

                HttpContext.Current.Cache.Add(key, entry, null, DateTime.Now.AddMinutes(imagecachetime), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return entry;
        }

        private PicasaService Login()
        {
            if (!isLoggedIn)
                this.service.setUserCredentials(this.UserName, this.Password);

            isLoggedIn = true;

            return this.service;
        }

        #region Image
        private PicasaFeed GetImageFeed(string username, string albumid)
        {
            string key = "imagefeed" + username + albumid;
            PicasaFeed feed = HttpContext.Current.Cache[key] as PicasaFeed;
            if (!UseCache || feed == null)
            {
                Login();

                PhotoQuery query = new PhotoQuery(PicasaQuery.CreatePicasaUri(username, albumid));

                feed = service.Query(query);
                HttpContext.Current.Cache.Add(key, feed, null, DateTime.Now.AddMinutes(imagecachetime), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
            return feed;
        }
        private PicasaEntry AddImage(string username, string albumid)
        {
            Login();
            PicasaEntry entry = new PhotoEntry();
            Uri postUri = new Uri(PicasaQuery.CreatePicasaUri(username, albumid));
            string path = HttpContext.Current.Server.MapPath(this.ImageUrl);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            using (System.IO.FileStream fileStream = fileInfo.OpenRead())
            {
                entry.MediaSource = new Google.GData.Client.MediaFileSource(fileStream, HttpContext.Current.Server.MapPath(this.ImageUrl), "image/jpeg");

                entry = service.Insert(postUri, entry) as PicasaEntry;
            }

            entry.Summary.Text = this.ImageUrl;
            entry.Update();

            return entry;
        }
        #endregion

        #region Album
        private string GetAlbumID(string username, string albumname)
        {
            string key = "albumid" + username + albumname;
            string id = HttpContext.Current.Cache[key] as string;
            if (!UseCache)
                id = "";
            if (string.IsNullOrEmpty(id))
            {
                Login();
                PicasaFeed feed = GetAlbumFeed(username);
                string title = albumname == "default" ? "Drop Box" : albumname;
                foreach (PicasaEntry entry in feed.Entries)
                {
                    AlbumAccessor ac = new AlbumAccessor(entry);
                    if (ac.AlbumTitle == title)
                    {
                        id = ac.Id;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(id) && albumname != "default")
                {
                    PicasaEntry entry = AddAlbum(username, albumname);
                    AlbumAccessor ac = new AlbumAccessor(entry);
                    id = ac.Id;
                }
                HttpContext.Current.Cache.Add(key, id, null, DateTime.Now.AddMinutes(albumcachetime), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
            return id;
        }
        private PicasaFeed GetAlbumFeed(string username)
        {
            string key = "albumfeed" + username;
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri(username));

            PicasaFeed feed = HttpContext.Current.Cache[key] as PicasaFeed;

            if (!UseCache || feed == null)
            {
                Login();
                feed = service.Query(query);
                //this could be saved as xml
                HttpContext.Current.Cache.Add(key, feed, null, DateTime.Now.AddMinutes(albumcachetime), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return feed;
        }
        private PicasaEntry AddAlbum(string username, string albumname)
        {
            Login();
            AlbumEntry newEntry = new AlbumEntry();
            newEntry.Title.Text = albumname;
            newEntry.Summary.Text = albumname;
            AlbumAccessor ac = new AlbumAccessor(newEntry);
            ac.Access = "private";

            Uri feedUri = new Uri(PicasaQuery.CreatePicasaUri(username));

            return (PicasaEntry)service.Insert(feedUri, newEntry);
        }
        #endregion
    }
}
