using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Collections;

namespace iStore.Orders
{
    public partial class Default : System.Web.UI.Page
    {

        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserOrder == null)
                Response.Redirect("~/Orders/OrdersList.aspx");
        }

        BL.Order _UserOrder;
        protected BL.Order UserOrder
        {
            get
            {
                if (_UserOrder == null)
                {
                    Guid oid;
                    if (!Guid.TryParse(Request.QueryString["oid"], out oid))
                        return null;
                    _UserOrder = obl.GetUserOrderedProducts(ubl.CurrentUser.UserID).FirstOrDefault(o => o.OrderID == oid);
                }
                return _UserOrder;
            }
        }
        protected void Save(object obj, EventArgs args)
        {
            if (!UserOrder.IsActive)
                return;
            List<Guid> toDelete;
            List<BL.ProductCounter> newCounts;
            try
            {

                var data = hf.Value.Split(new string[] { "~~~" }, StringSplitOptions.RemoveEmptyEntries);

                var es = data[1].Split(new string[] { "~~" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries)).First();

                toDelete = data[0].Split(new char[] { '~', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(i => new Guid(i)).ToList();
                newCounts = data[1].Split(new string[] { "~~" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries)).
                    Where(r => r.Count() > 0).Select(pc => new BL.ProductCounter() { Count = int.Parse(pc[1]), ID = new Guid(pc[0]) }).ToList();
            }
            catch (Exception e)
            {
                var a = e;
                return;
            }
            Guid oid;
            if (!Guid.TryParse(Request.QueryString["oid"], out oid))
                return;
            obl.Remove(toDelete, ubl.CurrentUser.UserID, oid);
            obl.UpdateCounts(newCounts, ubl.CurrentUser.UserID, oid);
        }
    }
}