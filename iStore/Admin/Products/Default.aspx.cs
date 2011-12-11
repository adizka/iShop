using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Products
{
    public partial class Default : System.Web.UI.Page
    {
        
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

        protected void Page_Load(object sender, EventArgs e)
        {
            pager.EntityCount = ProductsCount; 
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


        List<BL.Product> _PageProducts;
        protected List<BL.Product> PageProducts
        {
            get
            {
                if (_PageProducts == null)
                    _PageProducts = pbl.GetAllProducts().ToList()
                        .Where((c, ind) => ind >= pager .PageIndex * pager.EntitiesPerPage 
                            && ind < (pager.PageIndex + 1) * pager.EntitiesPerPage).ToList();

                return _PageProducts;
            }
        }
    }
}