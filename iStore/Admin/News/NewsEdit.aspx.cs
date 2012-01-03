using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iStore.Modules.Controls;

namespace iStore.Admin.News
{
    public partial class NewsEdit : System.Web.UI.Page
    {
        string sid = HttpContext.Current.Request.QueryString["id"];

        
        protected void Page_Load(object sender, EventArgs e)
        {
            BL.News news = CurrentNews;
            if (news != null)
            {
                if (!IsPostBack)
                {
                    txtBody.Text = news.NewsBody;
                    txtDesc.Text = news.NewsDescription;
                    txtTitle.Text = news.NewsTitle;
                }
                Page.Title = PageTitle.Get(global::Resources.Admin_Edits.NewsEdit);
            }
            errMsg.Visible = false;
        }

        protected void Save(object sender, EventArgs e)
        {
            string title = HttpContext.Current.Server.HtmlEncode(txtTitle.Text.Trim());
            string desc = HttpContext.Current.Server.HtmlEncode(txtDesc.Text.Trim());
            string body = txtBody.Text;
            var titleMaxLen = 25;
            if (title.Length > titleMaxLen)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Длина заголовка не может превышать" + titleMaxLen.ToString() + " символов";
                return;
            }
            var descriptionMaxLen = 1000;
            if (desc.Length  > descriptionMaxLen)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Длина описания не может превышать" + descriptionMaxLen.ToString() + " символов";
                return;
            }
            if (string.IsNullOrEmpty(body))
            {
                body = string.Empty;
            }
            BL.Modules.News.News nbl = new BL.Modules.News.News();
            int id = -1;
            if (CurrentNews != null)
            {
                id = CurrentNews.NewsID;   
            }
            nbl.AddOrUpdateNews(id, title, desc, body);
            Response.Redirect(iStore.Site.SiteAdminUrl + "News/");
        }

        public BL.News CurrentNews
        {
            get
            {
                int id = Convert.ToInt32(sid);
                BL.News news = BL.Modules.News.News.GetNewsById(id);
                if (news != null)
                {
                    return news;
                }
                return null;
            }
        }
    }
}