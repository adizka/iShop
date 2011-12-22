using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}