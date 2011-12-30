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
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["paymentSystem"] == "paypal")
                {
                    var authToken = WebConfigurationManager.AppSettings["PDTToken"];

                    var txToken = Request.QueryString.Get("tx");
                    txToken = "1R8946467V588680E";
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
                    BL.Helpers.PayPalPayerInfo ppInfo = new BL.Helpers.PayPalPayerInfo(strResponse);
                    if (ppInfo.IsSucced)
                    {
                        var a = ppInfo.mc_currency;
                        a = ppInfo.item_name1;
                        a = ppInfo.payer_email;
                        a = ppInfo.receiver_email;
                        obl.FormOrder(BL.PaymentTypes.PayPal, ubl.CurrentUser.UserID);
                            
                    }
                    else
                    {
                        //Label2.Text = "Oooops, something went wrong...";
                    }
                }
            }
        }
    }
}