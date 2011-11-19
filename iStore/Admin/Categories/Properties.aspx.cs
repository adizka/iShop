using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Categories
{
    public partial class Properties : System.Web.UI.Page
    {
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        BL.Modules.Categories.Properties pcbl = new BL.Modules.Categories.Properties();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IQueryable<BL.CategoryProperty> cp = allProperties;
                string sid = Request.QueryString["id"];
                if (sid == null)
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Categories/");
                }
                if (cp != null)
                {
                    rlProp.DataSource = allProperties;


                    rlProp.SortOrderField = "Sort";
                    rlProp.DataBind();
                }
            }
        }

        public IQueryable<BL.CategoryProperty> allProperties
        {
            get
            {
                string sid = Request.QueryString["id"];
                if (sid != null)
                {
                    Guid id = new Guid(sid);
                    IQueryable<BL.CategoryProperty> cp = pcbl.GetAllPropertiesByCategoryId(id);
                    if (cp.Any())
                    {
                        return cp;
                    }
                }
                return null;
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string value = txtValue.Text;
            if (string.IsNullOrEmpty(name))
            {
                ve.Visible = true;
                ve.ClearErrors();
                ve.Errors = "Сохранить не удалось, поле Name незаполнено";
                ve.SetErrors();
                return;
            }
            if (string.IsNullOrEmpty(value))
            {
                ve.Visible = true;
                ve.ClearErrors();
                ve.Errors = "Сохранить не удалось, поле Value незаполнено";
                ve.SetErrors();
                return;
            }
            string sid = Request.QueryString["id"];
            if (sid != null)
            {
                Guid id = new Guid(sid);
                bool success = pcbl.AddProperties(id, name, value, -1);
                if (success)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    ve.Visible = true;
                    ve.ClearErrors();
                    ve.Errors = "Сохранить не удалось";
                    ve.SetErrors();
                    return;
                }
            }
        }
    }
}