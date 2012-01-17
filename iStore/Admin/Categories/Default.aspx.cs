using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Categories
{
    public partial class Default : System.Web.UI.Page
    {
        public BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        public  BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sid = Request.QueryString["delcid"];
                if (sid != null)
                {
                    Guid id = new Guid(sid);
                    Delete(id);
                }
            }
            pager.EntityCount = allCategories.Count();
        }

        public IQueryable<BL.Category> allCategories
        {
            get
            {
                string sid = Request.QueryString["cid"];
                BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
                if (string.IsNullOrEmpty(sid)) 
                {
                    return cbl.GetAllRootCatgories().OrderBy(c => c.Sort);
                }
                try
                {
                    Guid id = new Guid(sid);
                    return cbl.GetCategoriesByParentId(id).OrderBy(c => c.Sort);
                }
                catch
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Login/");
                }
                return null;
            }
        }
     
        public IQueryable<BL.Category> allParentCategory
        {
            get
            {
                return cbl.GetAllCategories().Where(c => c.ParentID == null).OrderBy(c => c.Sort);
            }
        }

        public void Delete(Guid id)
        {
            iStore.Modules.Logic.Auth.Users ul = new iStore.Modules.Logic.Auth.Users();
            int errorMessageId = cbl.DeleteCategoryByIdWithErrorMessage(id);
            string errorMessage = string.Empty;

            divError.Visible = true;

            switch (errorMessageId)
            {
                //HasChilds = 1, HasProducts = 2, Invalid = 3, Success = 4
                case 1: { divError.InnerHtml = "Deletion failed. Please first delete subcategories."; break; };
                case 2: { divError.InnerHtml = "Deletion failed. Please first delete all products of this category"; break; };
                case 3: { divError.InnerHtml = "Deletion failed. Please chouse category"; break; };
                default: divError.InnerHtml = "Category successfully deleted.";  break;
            }
        }
    }
}