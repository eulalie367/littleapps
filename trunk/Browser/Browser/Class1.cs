using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;

namespace Browser
{
    public class WebSiteThumbnail
    {
        #region Properties
        public string URL { get; set; }
        public Bitmap Image { get; set; }
        private ManualResetEvent ReseteEvent = new ManualResetEvent(false);
        public int TimeOut { get; set; } // this could be adjusted up or down
        public int Width { get; set; }
        public int Height { get; set; }
        public string AbsolutePath { get; set; }
        public int Resolution { get; set; }
        #endregion
        
        #region Static Methods
        public static Bitmap GetSiteThumbnail(string url, int width, int height)
        {
            WebSiteThumbnail thumb = new WebSiteThumbnail(url, width, height);
            Bitmap b = thumb.GetScreenShot();
            if (b == null)
                b = (Bitmap)System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PAB.WebControls.Notavailable.jpg"));
            return b;
        }

        public static Bitmap GetSiteThumbnail(string url, int width, int height, string absolutePath)
        {
            WebSiteThumbnail thumb = new WebSiteThumbnail(url, width, height,absolutePath);
            Bitmap b = thumb.GetScreenShot();
            if (b == null)
                b = (Bitmap)System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PAB.WebControls.Notavailable.jpg"));
            return b;
        }
        #endregion

        #region Constructors
        public WebSiteThumbnail()
        {
            this.TimeOut = 30;
            this.Resolution = 72;
        }
        public WebSiteThumbnail(string url, int width, int height)
        {
            this.URL = url;
            this.Width = width;
            this.Height = height;
            this.TimeOut = 30;
            this.Resolution = 72;
        }
        public WebSiteThumbnail(string url, int width, int height, string absolutePath)
        {
            this.URL = url;
            this.Width = width;
            this.Height = height;
            this.AbsolutePath = absolutePath;
            this.TimeOut = 30;
            this.Resolution = 72;
        }
        #endregion

        #region ScreenShot
        /// <summary>
        /// Gets the screenshot from the given url
        /// </summary>
        /// <returns>Bitmap of the webpage</returns>
        public Bitmap GetScreenShot()
        {
                Thread t = new Thread(new ParameterizedThreadStart(getScreenShotFromURL));
                t.SetApartmentState(ApartmentState.STA);
                t.Start(URL);
                ReseteEvent.WaitOne();
                t.Abort();
            return Image;
        }
        /// <summary>
        /// GEts the screenshot from the supplied HTML
        /// </summary>
        /// <returns>Bitmap of webpage</returns>
        public Bitmap GetScreenShot(string HTML)
        {
                Thread t = new Thread(new ParameterizedThreadStart(getScreenShotFromHTML));
                t.SetApartmentState(ApartmentState.STA);
                t.Start(HTML);
                ReseteEvent.WaitOne();
                t.Abort();
            return Image;
        }
        #endregion
        private void getScreenShotFromURL(object strURL)
        {
            URL = (string)strURL;
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.ScrollBarsEnabled = false;
            DateTime time = DateTime.Now;
            webBrowser.Navigate(URL);
            //This doesn't actually mean all frames and images are ready, just that the javascript:body.onload() event has hit
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            while (true)
            {
                Thread.Sleep(0);
                TimeSpan elapsedTime = DateTime.Now - time;
                if (elapsedTime.Seconds >= TimeOut)
                {
                    ReseteEvent.Set();
                }
                Application.DoEvents();
            }
        }
        private void getScreenShotFromHTML(object strHTML)
        {
            string html = (string)strHTML;
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.ScrollBarsEnabled = false;
            DateTime time = DateTime.Now;
            webBrowser.Validating += new System.ComponentModel.CancelEventHandler(webBrowser_Validating);
            webBrowser.DocumentText = html;
            //This doesn't actually mean all frames and images are ready, just that the javascript:body.onload() event has hit
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            while (true)
            {
                Thread.Sleep(0);
                TimeSpan elapsedTime = DateTime.Now - time;
                if (elapsedTime.Seconds >= TimeOut)
                {
                    ReseteEvent.Set();
                }
                Application.DoEvents();
            }
        }

        void webBrowser_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webBrowser = (WebBrowser)sender;
            //give it 10 seconds to get the images and frames
//            System.Threading.Timer t = new System.Threading.Timer(saveImage, webBrowser, 2000, System.Threading.Timeout.Infinite);
            //if (webBrowser.ReadyState == WebBrowserReadyState.Loaded)
            //{
                saveImage(webBrowser);
            //}
            //else
            //{
            //    WebBrowser_DocumentCompleted(sender, e);
            //}
        }
        private void saveImage(object webBrowser)
        {
            WebBrowser browser = (WebBrowser)webBrowser;
            browser.ClientSize = new Size(this.Width, this.Height);
            browser.ScrollBarsEnabled = false;
            Image = new Bitmap(browser.Bounds.Width, browser.Bounds.Height);
            //up the res.
            Image.SetResolution(this.Resolution, this.Resolution);
            browser.BringToFront();
            browser.DrawToBitmap(Image, browser.Bounds);

            if (AbsolutePath != null)
            {
                string fileName = AbsolutePath + URL.Replace("http://", "").Replace(".", "_") + ".png";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                Image.MakeTransparent(Color.White);
                Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            browser.Dispose();
            Image.Dispose();

            if (ReseteEvent != null)
                ReseteEvent.Set();
        }

        public void Dispose()
        {
            if (Image != null) this.Image.Dispose();
        }
    }
}