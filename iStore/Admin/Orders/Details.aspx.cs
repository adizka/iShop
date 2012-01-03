using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Orders
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                statusDdl.SelectedIndex = Order.OrderStatusID - 1;
                deliveryDdl.SelectedIndex = Order.DeliveryTypeID - 1;
            }
        }

        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

        BL.Order _order;
        protected BL.Order Order
        {
            get
            {
                if(_order == null)
                {
                    Guid oid;
                    if(Guid.TryParse(Request.QueryString["oid"],out oid))
                        _order = obl.GetOrderById(oid);

                    if(_order == null)
                        Response.Redirect(iStore.Site.SiteAdminUrl + "Orders/OrderList.aspx");
                }
                return _order;
            }
        }

        protected void Save(object obj, EventArgs args)
        {
            var statusID = int.Parse(statusDdl.SelectedValue);
            var deliveryID = int.Parse(deliveryDdl.SelectedValue);

            obl.UpdateOrder(Order.OrderID, statusID, deliveryID);
            _order = null;
        }
    }
}