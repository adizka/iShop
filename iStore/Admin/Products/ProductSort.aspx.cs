using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Products
{
    public partial class ProductSort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();

        List<BL.ProductsRefCategory> _CatProducts;
        public List<BL.ProductsRefCategory> CatProducts
        {
            get
            {
                if (_CatProducts == null)
                    _CatProducts = prcbl.GetProductRefCategoriesByCategoryId(CategoryID).OrderBy(c => c.Sort).ToList();
                
                    
                return _CatProducts;
            }
        }

        Guid CategoryID
        {
            get
            {
                try
                {
                    return new Guid(Request.QueryString["cid"]);
                }
                catch
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Products/");
                }
                return Guid.Empty;
            }
        }

        protected void SaveCategoriesRate(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hd.Value))
            {
                var prodIds = hd.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(id => new Guid(id)).ToList();
                prcbl.UpdateProductsRefCategoriesSort(prodIds);
            }
            Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + Request.QueryString["cid"]);
        }
    }
}