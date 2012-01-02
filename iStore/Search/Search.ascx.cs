using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Search
{
    public partial class Search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text == "Search request here")
                return;
            string searchItem = Server.HtmlEncode(txtSearch.Text);
            string searchType = ddlSearchType.SelectedItem.Value;
            Response.Redirect(iStore.Site.SiteUrl + "Search/?s=" + searchItem + "&t=" + searchType);
        }
    }
}