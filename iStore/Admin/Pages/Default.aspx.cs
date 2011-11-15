using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using iStore.Modules.Controls;

namespace iStore.Admin.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sid = Request.QueryString["delid"];
                if (sid != null)
                {
                    Delete(sid);
                }
            }
            Page.Title = PageTitle.Get(global::Resources.Shops.PagesList);
        }

        private void Delete(string sid)
        {
            iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
            if (ubl.CurrentUserIdAdministrator)
            {
                int id = Convert.ToInt32(sid);
                BL.Modules.Pages.Pages pbl = new BL.Modules.Pages.Pages();
                BL.Page page = pbl.GetPageById(id);
                if (page != null)
                {
                    pbl.DeletePageById(page.PageID);
                    HttpContext.Current.Response.Redirect(iStore.Site.SiteAdminUrl + "Pages/");
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(iStore.Site.SiteUrl);
            }
        }

        public IQueryable<BL.Page> allPages
        {
            get
            {
                BL.Modules.Pages.Pages page = new BL.Modules.Pages.Pages();
                return page.GetAllPages();
            }
        }
    }
}