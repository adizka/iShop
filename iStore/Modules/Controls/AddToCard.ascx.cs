using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace iStore.Modules.Controls
{
    public partial class AddToCard : System.Web.UI.UserControl
    {

        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                hf.Value = "1";
        }

        object _GlobalIDObj;
        Guid _GlobalID;
        public Guid GlobalID
        {
            get
            {
                if (_GlobalIDObj == null)
                {
                    _GlobalIDObj = new object();
                    _GlobalID = Guid.NewGuid();
                }
                return _GlobalID;
            }
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

        public void AddToCart(object obj, EventArgs args)
        {
            if (auth.CurrentUser == null)
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx");
            int count;
            if (!int.TryParse(hf.Value, out count) || count < 1)
                return;
            obl.AddToCart(new List<BL.ProductCounter>() { new BL.ProductCounter() { ID = ProductId, Count = count } }, auth.CurrentUser.UserID);
        }

        public static string RenderControl(Control ctrl)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            ctrl.RenderControl(hw);
            return sb.ToString();
        }
    }
}