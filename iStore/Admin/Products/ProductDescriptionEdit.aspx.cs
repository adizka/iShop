using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Products
{
    public partial class ProductDescriptionEdit : System.Web.UI.Page
    {
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductProperies ppbl = new BL.Modules.Products.ProductProperies();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (CurrentProduct == null)
                Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + Request.QueryString["cid"]);
            txtBody.Text = string.Empty;

        }

        public BL.Product _CurrentProduct;
        public BL.Product CurrentProduct
        {
            get
            {

                if (_CurrentProduct != null)
                    return _CurrentProduct;

                Guid pid;
                if (!Guid.TryParse(Request.QueryString["pid"], out pid))
                    return null;

                _CurrentProduct = pbl.GetProductById(pid);
                return _CurrentProduct;
            }
        }

        protected void SaveDescription(object sender, EventArgs e)
        {
            ppbl.AddProductProperty(BL.ProductPropertyConstants.ProductDescription, txtBody.Text, CurrentProduct.ProductID);
        }
    }
}