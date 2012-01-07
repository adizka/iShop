using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Orders
{
    public partial class OrderList : System.Web.UI.Page
    {
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        List<BL.Order> _FilteredOrders;
        protected List<BL.Order> FilteredOrders
        {
            get
            {
                if (_FilteredOrders == null)
                {
                    var txt = string.IsNullOrEmpty(usrtxt.Text) ? "\n\n\n" : usrtxt.Text;
                    _FilteredOrders = obl.GetAllOrders().
                        Where(o =>
                            (
                            o.OrderStatusID == int.Parse(statusDdl.SelectedValue)
                            && o.DeliveryTypeID == int.Parse(deliveryDdl.SelectedValue)
                            && o.CreateDate >= DateTime.Now.AddHours(-int.Parse(dateDdl.SelectedValue)))
                            || o.User.Login.IndexOf(txt) != -1
                            || o.User.Email.IndexOf(txt) != -1
                            ).OrderBy(d=>d.CreateDate).ToList();
                }
                return _FilteredOrders;
            }
        }

        protected void GetByTransactionID(object obj, EventArgs args)
        {
            _FilteredOrders = obl.GetAllOrders().Where(o => o.TransactionID.IndexOf(txName.Text) != -1).ToList();
        }

        protected void Update(object obj, EventArgs args)
        {
            _FilteredOrders = null;
        }

    }
}