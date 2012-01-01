using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Design
{
    public partial class MostPopularProducts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<BL.Product> AllProducts
        {
            get 
            { 
                var pbl = new BL.Modules.Products.Products();
                return pbl.GetMostPopularProducts();
            }
        }

        public string GetProductPreviewById(Guid id)
        {
            var ppbl = new BL.Modules.Products.ProductProperies();
            return ppbl.GetProductPreviewByProductId(id);
        }
    }
}