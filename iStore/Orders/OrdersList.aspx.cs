using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Orders
{
    public partial class OrdersList : System.Web.UI.Page
    {

        iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (auth.CurrentUser == null)
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx");
        }
        List<BL.Order> _UserOrder;
        protected List<BL.Order> UserOrders
        {
            get
            {
                if (_UserOrder == null)
                    _UserOrder = obl.GetUserOrderedProducts(auth.CurrentUser.UserID).OrderByDescending(o => o.CreateDate).ToList();
                return _UserOrder;
            }
        }
    }
}