using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Modules.Controls
{
    public partial class AddToCard : System.Web.UI.UserControl
    {
        
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                hf.Value = "1";
        }

        public Guid ProductId
        {
            get
            {
                return Guid.Parse(ProdID.Value);
            }
            set
            {
                ProdID.Value = value.ToString();
            }
        }
        public bool IsCounterVisible
        {
            get
            {
                return CounterContainer.Visible;
            }
            set
            {
                CounterContainer.Visible = value;
            }
        }

        protected void AddToCart(object obj, EventArgs args)
        {
            int count;
            if (!int.TryParse(hf.Value, out count) || count < 1)
                return;
            obl.AddToCart(new List<BL.ProductCounter>() { new BL.ProductCounter() { ID = ProductId, Count = count } }, ubl.CurrentUser.UserID);
        }
    }
}