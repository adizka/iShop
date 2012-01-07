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

        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        List<BL.Order> _UserOrder;
        protected List<BL.Order> UserOrders
        {
            get
            {
                if (_UserOrder == null)
                    _UserOrder = obl.GetUserOrderedProducts(ubl.CurrentUser.UserID).OrderByDescending(o=>o.CreateDate).ToList();
                return _UserOrder;
            }
        }
    }
}