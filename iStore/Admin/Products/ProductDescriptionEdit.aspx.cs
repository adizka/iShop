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
            
            if (!IsPostBack)
            {
                txtBody.Text = ProductDescription;
                divError.Visible = false;
                divError.InnerHtml = string.Empty;
            }

        }

        string _ProductDescription;
        protected string ProductDescription
        {
            get
            {
                if (_ProductDescription == null)
                {
                    var prodProp = CurrentProduct.ProductProperties.FirstOrDefault(p => p.PropertyName == BL.ProductPropertyConstants.ProductDescription);
                    _ProductDescription = (prodProp == null) ? string.Empty : prodProp.PropertyValue;
                }
                return _ProductDescription;
            }
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
            ppbl.UpdateProductDescription(txtBody.Text, CurrentProduct.ProductID);
            divError.Visible = true;
            divError.InnerHtml = "Description saved successfully!";
            Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?p=" + Request.QueryString["p"] + "&cid=" + Request.QueryString["cid"]);
        }
    }
}