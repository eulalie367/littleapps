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

namespace WhatIsHappening
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public class Event
    {
        #region Contructors

        #endregion

        #region Properties
        public bool Continual   { get; set; }
        public DateTime Starts  { get; set; }
        public DateTime Ends    { get; set; }
        public string Name      { get; set; }
        public Int64 LocationID { get; set; }
        public Int64? EventSetID{ get; set; }
        public int EventType    { get; set; }
        #endregion

        #region Events

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

        #region Internal Classes

        #endregion

    }
}
