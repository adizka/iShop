using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Products
{
    public partial class ProductsPhoto : System.Web.UI.Page
    {
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductProperies ppbl = new BL.Modules.Products.ProductProperies();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public BL.Product CuurentProduct
        {
            get
            {
                string spid = Request.QueryString["pid"];
                if (string.IsNullOrEmpty(spid))
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl  + "Products/?cid=" + Request.QueryString["cid"]);
                }
                Guid pid = Guid.NewGuid();
                try
                {
                    pid = new Guid(spid);
                }
                catch
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + Request.QueryString["cid"]);
                }
                BL.Product product = pbl.GetProductById(pid);
                if (product != null) return product;
                return null;
            }
        }

        public string CurrentProductPreview
        {
            get 
            { 
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return iStore.Site.SiteUrl + ppbl.GetProductPreviewByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }

        public string CurrentProductOriginal
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return iStore.Site.SiteUrl + ppbl.GetProductOriginalByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }
    }
}