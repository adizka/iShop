using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        BL.Page _page;
        protected BL.Page Page
        {
            get
            {
                if (_page == null)
                    _page = BL.Modules.Pages.Pages.GetPageByName(Request.QueryString["name"]);
                return _page;
            }
        }
    }
}