using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Collections;
using System.Configuration;

namespace iStore.Orders
{
    public partial class Default : System.Web.UI.Page
    {

        iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (auth.CurrentUser == null)
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx");

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
                        _UserOrder = obl.GetUserOrderedProducts(auth.CurrentUser.UserID).FirstOrDefault(o => o.IsActive);
                    else
                    {
                        _UserOrder = obl.GetUserOrderedProducts(auth.CurrentUser.UserID).FirstOrDefault(o => o.OrderID == oid);
                    }
                    if (_UserOrder == null)
                        _UserOrder = obl.CreateOrder(auth.CurrentUser.UserID);
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
                return;
            }

            obl.Remove(toDelete, auth.CurrentUser.UserID, UserOrder.OrderID);
            obl.UpdateCounts(newCounts, auth.CurrentUser.UserID, UserOrder.OrderID);
        }
        protected void Clear(object obj, EventArgs args)
        {
            obl.ClearCart(auth.CurrentUser.UserID, UserOrder.OrderID);
        }
        protected void FromOrder(object obj, EventArgs args)
        {
            Save(obj, args);
            Response.Redirect(iStore.Site.SiteUrl + "Orders/FormOrder.aspx");
        }
    }
}