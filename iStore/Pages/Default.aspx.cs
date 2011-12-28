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
        BL.Modules.Pages.Pages pbl = new BL.Modules.Pages.Pages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentPage == null)
            {
                Response.Redirect(iStore.Site.SiteUrl);
            }
        }

        public BL.Page CurrentPage
        {
            get 
            {
                string name = Request.QueryString["name"];
                if (!string.IsNullOrEmpty(name))
                {
                    BL.Page page = pbl.GetPageByName(name);
                    if (page != null)
                    {
                        return page;
                    }
                }
                return null;
            }
        }

        public string CurrentPageName
        {
            get 
            {
                if (CurrentPage == null) return string.Empty;
                string name = CurrentPage.PageName;
                return name.Replace("_", " ");
            }
        }
    }
}