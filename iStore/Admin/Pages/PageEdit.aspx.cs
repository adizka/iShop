﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Resources;
using System.Web.Resources;
using iStore.Modules.Controls;


namespace iStore.Admin.Pages
{
    public partial class PageEdit : System.Web.UI.Page
    {
        string sid = HttpContext.Current.Request.QueryString["id"];

        BL.Modules.Pages.Pages pages = new BL.Modules.Pages.Pages();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PageTitle.Get("Page edit");
            if (!IsPostBack)
            {
                int id = CurrentPageId;
                if (id != -1)
                {
                    BL.Page page = pages.GetPageById(id);
                    txtName.Text = page.PageName;
                    txtBody.Text = page.PageBody;
                    txtDesc.Text = page.Keywords;
                }
            }
        }

        

        protected void Save(object sender, EventArgs e)
        {   
            string name = HttpContext.Current.Server.HtmlEncode(txtName.Text);
            string keywords = HttpContext.Current.Server.HtmlEncode(txtDesc.Text.Replace(" ", "_"));
            string body = txtBody.Text;
            bool AllRight = CheckAll(keywords, name, body);
            if (!AllRight)
            {
                return;
            }
            pages.AddOrUpdatePage(CurrentPageId, keywords, name, body);
            HttpContext.Current.Response.Redirect(iStore.Site.SiteAdminUrl + "Pages/");
        }

        private bool CheckAll(string name, string keywords, string body)
        {
            ve.ClearErrors();
            if (string.IsNullOrEmpty(name))
            {
                ve.Errors = global::Resources.Admin_Edits.PageNameRequired.ToString();
                ve.SetErrors();
                return false;
            }
            var maxLen = 25;
            if (name.Length  > maxLen)
            {
                ve.Errors = "Name must be no longer than   " + maxLen.ToString() + " characters"; 
                ve.SetErrors();
                return false;
            }

            if (pages.IsPageNameInDB(name))
            {
                if (CurrentPageId == -1)
                {
                    ve.Errors = global::Resources.Admin_Edits.PageNameInDB.ToString();
                    ve.SetErrors();
                    return false;
                }
            }
            return true;
        }

        private int CurrentPageId
        {
            get 
            {
                if (sid != null)
                {
                    int id = Convert.ToInt32(sid);
                    BL.Page page = pages.GetPageById(id);
                    if (page != null)
                    {
                        return page.PageID;        
                    }
                }
                return -1;
            }
        }
        
    }
}