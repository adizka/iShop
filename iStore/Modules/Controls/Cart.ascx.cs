using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Modules.Controls
{
    public partial class Cart : System.Web.UI.UserControl
    {
        
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        object ProdCountObj;
        int _prodCount;
        protected int ProdCount
        {
            get
            {
                if (ProdCountObj == null)
                {
                    _prodCount = 0;
                    ProdCountObj = new object();
                    if (Order != null)
                        _prodCount = Order.OrdersRefProducts.Sum(p => p.Count);
                }

                return _prodCount;
            }
        }

        object TotalSumObj;
        decimal _totalSum;
        protected decimal TotalSum
        {
            get
            {
                if (TotalSumObj == null)
                {
                    TotalSumObj = new object();
                    _totalSum = 0;
                    if (Order != null)
                        _totalSum = Convert.ToDecimal(Order.OrdersRefProducts.Sum(r => r.Product.Price * r.Count));
                }

                return _totalSum;
            }
        }
        object _orderObj;
        BL.Order _order;
        protected BL.Order Order
        {
            get
            {
                if (_orderObj == null)
                {
                    _orderObj = new object();
                    _order = obl.GetUserOrderedProducts(ubl.CurrentUser.UserID).FirstOrDefault(o => o.IsActive);
                }
                return _order;
            }
        }

        List<BL.OrdersRefProduct> _userProducts;
        protected List<BL.OrdersRefProduct> UserProducts
        {
            get
            {
                if (_userProducts == null)
                    _userProducts = obl.GetUserOrderedProducts(ubl.CurrentUser.UserID).
                        SelectMany(p => p.OrdersRefProducts).ToList();

                return _userProducts;
            }
        }

    }
}