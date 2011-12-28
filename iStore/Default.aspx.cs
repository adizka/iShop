using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;

namespace iStore
{
    public partial class _Default : System.Web.UI.Page
    {

        public IQueryable<BL.Product> AllProducts
        {
            get 
            {
                BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
                return pbl.GetAllProducts().Where(p => p.IsVisible == true).OrderByDescending(p => p.CreateDate).Take(4);
            }
        }

        public string GetProductPreviewById(Guid id)
        {
            var ppbl = new BL.Modules.Products.ProductProperies();
            return ppbl.GetProductPreviewByProductId(id);
        }
    }
}
