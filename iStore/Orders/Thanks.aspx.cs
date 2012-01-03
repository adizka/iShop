using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.IO;

namespace iStore.Orders
{
    public partial class Thanks : System.Web.UI.Page
    {
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (UserOrder == null || UserOrder.OrdersRefProducts.Count == 0)
            {
                msg.InnerHtml = "You do not have any items in your cart";
                return;
            }

            if (!Page.IsPostBack)
            {
                // if (Request.QueryString["paymentSystem"] == "paypal")
                {
                    var ppInfo = GetPaymentInfo();
                    if (ppInfo.IsSucced
                        && Convert.ToDecimal(UserOrder.OrdersRefProducts.Sum(r => r.Product.Price * r.Count)) == ppInfo.mc_gross
                        && ppInfo.receiver_email == WebConfigurationManager.AppSettings["Login"])
                    {

                        if (obl.TryFormOrder(BL.PaymentTypes.PayPal, ubl.CurrentUser.UserID, ppInfo.payment_date, ppInfo.txn_id))
                            msg.InnerHtml = "Transaction complete successfuly! :)";
                        else
                            msg.InnerHtml = "You transaction had already been passed! :)";
                    }
                    else
                    {
                        msg.InnerHtml = "You had paid incorrect sum ):";
                    }
                }
            }
        }


        BL.Helpers.PayPalPayerInfo GetPaymentInfo()
        {
            var authToken = WebConfigurationManager.AppSettings["PDTToken"];

            var txToken = Request.QueryString.Get("tx");

            var query = string.Format("cmd=_notify-synch&tx={0}&at={1}",
                                  txToken, authToken);

            string url = WebConfigurationManager.AppSettings["PayPalPaymentUrlTest"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            StreamWriter stOut = new StreamWriter(req.GetRequestStream(),
                                     System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            var strResponse = stIn.ReadToEnd();
            stIn.Close();
            return new BL.Helpers.PayPalPayerInfo(HttpUtility.UrlDecode(strResponse));
        }

        BL.Order _UserOrder;
        protected BL.Order UserOrder
        {
            get
            {
                if (_UserOrder == null)
                    _UserOrder = obl.GetUserOrderedProducts(ubl.CurrentUser.UserID).FirstOrDefault(o => o.IsActive);
                return _UserOrder;
            }
            set
            {
                _UserOrder = value;
            }
        }
    }
}