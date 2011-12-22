using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Orders
{
    public partial class FormOrder : System.Web.UI.Page
    {
        
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

        protected void Page_Load(object sender, EventArgs e)
        {
 
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
        protected void Pay(object obj, EventArgs args)
        {
            if (UserOrder == null || UserOrder.OrdersRefProducts.Count == 0)
                return;

            obl.FromOrder(int.Parse(paymentDdl.SelectedValue), int.Parse(deliveryDdl.SelectedValue),ubl.CurrentUser.UserID);
            UserOrder = null;
        }
    }
}