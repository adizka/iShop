using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iStore.Modules.Controls;

namespace iStore.Admin.News
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               Delete();
               Activate();
               DeActivate();
            }
            Page.Title = PageTitle.Get(global::Resources.Shops.NewsList);
        }

        public IQueryable<BL.News> allNews
        {
            get
            {
                return BL.Modules.News.News.GetAllnews();
            }
        }


        private void Delete()
        {
            string sid = Request.QueryString["delid"];
            if (sid != null)
            {
                iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
                if (ubl.CurrentUserIdAdministrator)
                {
                    int id = Convert.ToInt32(sid);
                    BL.Modules.News.News nbl = new BL.Modules.News.News();
                    BL.News news = BL.Modules.News.News.GetNewsById(id);
                    if (news != null)
                    {
                        nbl.DeleteNewsById(news.NewsID);
                        Response.Redirect(iStore.Site.SiteAdminUrl + "News/");
                    }
                }
                else
                {
                    Response.Redirect(iStore.Site.SiteUrl);
                }
            }
        }

        private void Activate()
        {

            string sid = Request.QueryString["activid"];
            if (sid != null)
            {
                iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
                if (ubl.CurrentUserIdAdministrator)
                {
                    int id = Convert.ToInt32(sid);
                    BL.Modules.News.News nbl = new BL.Modules.News.News();
                    BL.News news = BL.Modules.News.News.GetNewsById(id);
                    if (news != null)
                    {
                        nbl.AcivateNewsById(news.NewsID);
                        Response.Redirect(iStore.Site.SiteAdminUrl + "News/");
                    }
                }
                else
                {
                    Response.Redirect(iStore.Site.SiteUrl);
                }
            }
        }

        private void DeActivate()
        {
            string sid = Request.QueryString["deactivid"];
            if (sid != null)
            {
                iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
                if (ubl.CurrentUserIdAdministrator)
                {
                    int id = Convert.ToInt32(sid);
                    BL.Modules.News.News nbl = new BL.Modules.News.News();
                    BL.News news = BL.Modules.News.News.GetNewsById(id);
                    if (news != null)
                    {
                        nbl.DeAcivateNewsById(news.NewsID);
                        Response.Redirect(iStore.Site.SiteAdminUrl + "News/");
                    }
                }
                else
                {
                    Response.Redirect(iStore.Site.SiteUrl);
                }
            }
        }
    }
}