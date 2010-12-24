using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Promotions;

namespace Hershey.Web
{
    public partial class EditPromo : System.Web.UI.Page
    {
        public PromotionService PromotionObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string strPromoID = Request["id"];
            if (!string.IsNullOrEmpty(strPromoID))
            {
                var promo = PromotionObject.GetPromotionById(new Guid(strPromoID));
                if (promo != null)
                {
                    litPromo.Text = promo.Name;
                }
            }
        }
    }
}