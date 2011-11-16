using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace iStore.Admin.Categories
{
    public partial class Default : System.Web.UI.Page
    {
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sid = Request.QueryString["delid"];
                if (sid != null)
                {
                    Guid id = new Guid(sid);
                    Delete(id);
                }
            }

            var temp = RootCategory;

        }

        public Categories RootCategory
        {
            get
            {
                iStore.Admin.Categories.Categories cl = new iStore.Admin.Categories.Categories();
                return cl;
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
            if (!ul.CurrentUserIdAdministrator)
            {
                Response.Redirect(iStore.Site.SiteAdminUrl);
            }
            
            int errorMessageId = cbl.DeleteCategoryByIdWithErrorMessage(id);
            string errorMessage = string.Empty;
            switch (errorMessageId)
            {
                //HasChilds = 1, HasProducts = 2, Invalid = 3, Success = 4
                case 1: { errorMessage = "Удаление не возможно. Категория имеет подкатегории."; break; };
                case 2: { errorMessage = "Удаление не возможно. Категория имеет товары."; break; };
                case 3: { errorMessage = "Удаление не возможно. Категория не выбрана."; break; };
                default: errorMessage = "Удаление прошло успешно"; break;
            }
            ve.Visible = true;
            ve.ClearErrors();
            ve.Errors = errorMessage;
            ve.SetErrors();
        }
    }
}