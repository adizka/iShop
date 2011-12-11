using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin
{
    public partial class SearchControl : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PriceTxtFrom.Text = Request.QueryString["prf"];
                this.PriceTxtTo.Text = Request.QueryString["prt"];
                this.CriteriaTxt.Text = Request.QueryString["key"];
            }
        }
        protected void SearchProduct(object obj, EventArgs args)
        {
            if(string.IsNullOrEmpty( CriteriaTxt.Text))
                return;

            Response.Redirect(Server.UrlDecode("~/Admin/Search.aspx?key=" +
                CriteriaTxt.Text + "&prt=" + PriceTxtTo.Text + "&prf=" + PriceTxtFrom.Text));
        }

    }
}