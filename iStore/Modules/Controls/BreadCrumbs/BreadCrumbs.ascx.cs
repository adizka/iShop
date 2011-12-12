using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Modules.Controls.BreadCrumbs
{
    public partial class BCCategories : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCrumbs();
        }

        private void SetCrumbs()
        {
            string cid = Request.QueryString["cid"];
            if (string.IsNullOrEmpty(cid))
            {
                pBC.InnerHtml = SetRootCategories();
            }
            else
            {
                SetBranch(cid);
            }
        }

        private void SetBranch(string cid)
        {
            Guid id;
            try
            {
                id = new Guid(cid);
                BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
                BL.Category category = cbl.GetCategoryById(id);
                if (category != null)
                {
                    string bctemplate = string.Empty;
                    string preUrl = ((!SiteMode) ? iStore.Site.SiteAdminUrl : iStore.Site.SiteUrl) + EntityType + "/";
                    Guid? parentId = category.CategoryID;
                    BL.Category tempCategory;
                    int i = 0;
                    for ( ; ; )
                    {   
                        if (parentId == null)
                        {
                            break;
                        }
                        tempCategory = cbl.GetCategoryById(parentId);
                        if (tempCategory == null)
                        {
                            break;
                        }
                        parentId = tempCategory.ParentID;
                        string arrow = (i == 0) ? string.Empty : "->";
                        i++;
                        string url = String.Format("{0}?cid={1}", preUrl,  tempCategory.CategoryID.ToString());
                        bctemplate = String.Format("<a href='{0}' class='BCCategoriesLink'>{1}</a>{2}", url, tempCategory.Name, arrow) + bctemplate;
                    }
                    bctemplate = SetRootCategories() +  bctemplate;
                    pBC.InnerHtml = bctemplate;
                }
            }
            catch
            {
                pBC.Visible = false;
            }
            
        }

        private string SetRootCategories()
        {
            string bctemplate = string.Empty;
            string url = ((!SiteMode) ? iStore.Site.SiteAdminUrl : iStore.Site.SiteUrl) + EntityType  + "/";
            bctemplate =  String.Format("<a class='BCCategoriesLink' href='{0}'>{1}</a>{2}", url, "Главная", "->");
            return bctemplate;
        }

        public bool SiteMode { get; set; }

        public string EntityType { get; set; }
    }
}