using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string InitializeRecord()
    {
        return "";
    }
    [WebMethod]
    public void AddToAudioFile(string CachedAudioFileId)
    {
    }
    [WebMethod]
    public string Recognize()//(string CachedAudioFileId)
    {
        DateTime start = DateTime.Now;
        msRecognizer regon = new msRecognizer();
        System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(regon.Recognize));
        th.Start("");
        //It takes at least 4 seconds to perform the action
        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(4000));
        while (th.IsAlive) { System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100)); }

        return regon.retVal + " time=" + DateTime.Now.Subtract(start).TotalMilliseconds + "ms";
    }
}
