using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Products
{
    public partial class ProductsList : System.Web.UI.Page
    {

        public BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        object _prodCountInd;
        int _ProductsCount;
        public int ProductsCount
        {
            get
            {
                if (_prodCountInd == null)
                    _ProductsCount = pbl.GetAllProducts().Count();

                _prodCountInd = new object();
                return _ProductsCount;
            }
        }

        public Guid? CurrentCategoryId
        {
            get
            {
                string scid = Request.QueryString["cid"];
                Guid cid;
                try
                {
                    cid = new Guid(scid);
                }
                catch
                {
                    return null;
                }
                return cid;
            }
        }

        List<BL.ProductsRefCategory> _PageProducts;
        protected List<BL.ProductsRefCategory> PageProducts
        {
            get
            {
                if (CurrentCategoryId == null)
                {
                    if (_PageProducts == null)
                        _PageProducts = prcbl.GetAllProductsRefCategories().ToArray().Distinct(new BL.ProductsRefCategoryComparer())
                            .Where((c, ind) => ind >= pager.PageIndex * pager.EntitiesPerPage
                                               && ind < (pager.PageIndex + 1) * pager.EntitiesPerPage).ToList();
                }
                else
                {
                    if (_PageProducts == null)
                        _PageProducts = prcbl.GetProductRefCategoriesByCategoryId(CurrentCategoryId.Value).ToArray()
                            .Distinct(new BL.ProductsRefCategoryComparer())
                            .Where((c, ind) => ind >= pager.PageIndex * pager.EntitiesPerPage
                                               && ind < (pager.PageIndex + 1) * pager.EntitiesPerPage).ToList();
                }
                return _PageProducts;
            }
        }



    }
}