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

            if (ubl.CurrentUser == null)
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx");

            if (UserOrder == null || UserOrder.OrdersRefProducts.Count == 0)
            {
                msg.InnerHtml = "You do not have any items in your cart";
                return;
            }

            if (UserOrder.UserID != ubl.CurrentUser.UserID)
                Response.Redirect("~/Orders/OrdersList.aspx");


            msg.InnerHtml = "You transaction had already been passed! :)";
        }

        BL.Order _UserOrder;
        protected BL.Order UserOrder
        {
            get
            {
                if (_UserOrder == null)
                {
                    var txToken = Request.QueryString.Get("txn_id");
                    if (string.IsNullOrEmpty(txToken))
                        txToken = Request.QueryString.Get("tx");

                    _UserOrder = obl.GetAllOrders().FirstOrDefault(o => o.TransactionID == txToken);
                }
                return _UserOrder;
            }
            set
            {
                _UserOrder = value;
            }
        }
    }
}