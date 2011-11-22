using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Categories
{
    public partial class CategoryEdit : System.Web.UI.Page
    {
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategories.DataSource = allCategories;
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataTextField = "Name";
                ddlCategories.DataBind();
                BL.Category category = CurrentCategory;
                if (category != null)
                {
                    txtName.Text = category.Name;
                    if (category.ParentID == null)
                    {
                        ddlCategories.SelectedValue = "parent";
                    }
                    else
                    {
                        ddlCategories.SelectedValue = category.CategoryID.ToString();
                    }
                }
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            string name = Server.HtmlEncode(txtName.Text);
            BL.Category category = CurrentCategory;
            string hf = ddlCategories.SelectedItem.Value;
            if (string.IsNullOrEmpty(name))
            {
                ve.ClearErrors();
                ve.Errors = "Не заполнили название";
                ve.SetErrors();
                return;
            }

            if (cbl.NameInBD(name))
            {
                if (category == null)
                {
                    ve.ClearErrors();
                    ve.Errors = "Категория с таким именем уже существует";
                    ve.SetErrors();
                    return;
                }
            }
            else
            {
                if (category == null)
                {
                    Guid parentId;
                    if (hf == "parent")
                    {
                        cbl.AddCategory(name, null);
                    }
                    else
                    {
                        parentId = new Guid(hf);
                        cbl.AddCategory(name, parentId);
                    }
                }
                else
                {
                    Guid parentId;
                    if (hf == "parent")
                    {
                        cbl.UpdateCategory(category.CategoryID, name, null);
                    }
                    else
                    {
                        parentId = new Guid(hf);
                        cbl.UpdateCategory(category.CategoryID, name, parentId);
                    }
                }
                Response.Redirect(iStore.Site.SiteAdminUrl + "Categories/");

            }
        }

        public IQueryable<BL.Category> allCategories
        {
            get
            {
                return cbl.GetAllCategories();
            }
        }

        public BL.Category CurrentCategory
        {
            get
            {
                string sid = Request.QueryString["cid"];
                if (sid != null)
                {
                    Guid id = new Guid(sid);
                    return cbl.GetCategoryById(id);
                }
                return null;
            }
        }
    }
}