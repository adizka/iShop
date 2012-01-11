using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Statistics
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Order == null)
                Response.Redirect(iStore.Site.SiteAdminUrl + "Statistics/Userslist.aspx");
        }



        BL.Order _Order;
        protected BL.Order Order
        {
            get
            {
                if (_Order == null)
                {
                    Guid oid;
                    if (Guid.TryParse(Request.QueryString["oid"], out oid))
                        _Order = obl.GetOrderById(oid);
                }
                return _Order;
            }
        }
    }
}