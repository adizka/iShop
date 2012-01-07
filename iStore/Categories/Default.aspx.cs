using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace iStore.Categories
{
    public partial class Default : System.Web.UI.Page
    {
        public BL.Modules.Categories.Categories Cbl = new BL.Modules.Categories.Categories();
        public BL.Modules.Products.ProductRefCategories Prcbl = new BL.Modules.Products.ProductRefCategories();
        public BL.Modules.Products.ProductProperies Ppbl = new BL.Modules.Products.ProductProperies();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentCategory == null) Response.Redirect(iStore.Site.SiteUrl);
            }
            pager.EntityCount = GetProductsRefCurrentCategory.Count();
        }

        public BL.Category CurrentCategory
        {
            get
            {
                string cid = Request.QueryString["cid"];
                Guid id;
                if (string.IsNullOrEmpty(cid)) return null;
                try { id = new Guid(cid); } catch { return null; }
                return Cbl.GetCategoryById(id);
            }
        }

        public IQueryable<BL.ProductsRefCategory> GetProductsRefCurrentCategory
        {
            get 
            { 
                BL.Category category = CurrentCategory;
                if (category == null) return null;
                return Prcbl.GetProductRefCategoriesByCategoryId(category.CategoryID);
            }
        }

        public IQueryable<BL.ProductsRefCategory> GetPageProductsRefCurrentCategory
        {
            get
            {
                BL.Category category = CurrentCategory;
                if (category == null) return null;
                return Prcbl.GetProductRefCategoriesByCategoryId(category.CategoryID).Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage);
            }
        }

        public IList<BL.ProductsRefCategory> GetProductsRefCurrentCategoryList
        {
            get 
            {
                return GetProductsRefCurrentCategory.ToList();
            }
        }

        public IQueryable<BL.Category> GetChildCategoryWhereParentIsCurrentCategory
        {
            get 
            { 
                BL.Category category = CurrentCategory;
                if (category == null) return null;
                return Cbl.GetCategoriesByParentId(category.CategoryID);
            }
        }

        public string GetProductsCountInCategory(Guid categoryId)
        {
            return Prcbl.GetProductRefCategoriesByCategoryId(categoryId).Count().ToString();
        }

        public string GetProductPreview(Guid productId)
        {
            return Ppbl.GetProductPreviewByProductId(productId);
        }

        public IQueryable<BL.ProductProperty> GetProductPropery(Guid productId)
        {
            return Ppbl.GetAllProperyByProductId(productId).Take(6);
        }

        protected string GetRenderedControl(BL.ProductsRefCategory item)
        {
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            var control = LoadControl("~/Modules/Controls/AddToCard.ascx") as iStore.Modules.Controls.AddToCard;
            control.ProductId = item.ProductID;
            control.ID = Guid.NewGuid().ToString();
            control.IsCounterVisible = false;
            var btn = control.FindControl("addBtn") as System.Web.UI.WebControls.Button;
            btn.OnClientClick = "addTocart('" + item.ProductID.ToString() + "',1)";
            return iStore.Modules.Controls.AddToCard.RenderControl(control);
        }
    }
}
