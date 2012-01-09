using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace iStore.Modules.Controls.NewProducts
{
    public partial class NewProducts : System.Web.UI.UserControl
    {
        BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        List<BL.ProductsRefCategory> _newProducts;
        protected List<BL.ProductsRefCategory> NewestProducts
        {
            get
            {
                if (_newProducts == null)
                {
                    if (CurrentCategoryId.HasValue)
                        _newProducts = prcbl.GetProductRefCategoriesByCategoryId(CurrentCategoryId.Value).OrderByDescending(p => p.Product.CreateDate).Take(RowsCount * ColumnsCount).ToList();
                    else
                        _newProducts = pbl.GetAllProducts().OrderByDescending(p => p.CreateDate).Take(RowsCount * ColumnsCount).Select(p => p.ProductsRefCategories.First()).ToList();
                }

                return _newProducts;
            }
        }

        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }

        protected string GetPreviewUrl(BL.Product prod)
        {
            var prodProp = prod.ProductProperties.FirstOrDefault(p => p.PropertyName == BL.ProductPropertyConstants.ProductPhotoPreview);
            return (prodProp == null) ? BL.Site.DefaultPhotoPreview : prodProp.PropertyValue;
        }

        public Guid? CurrentCategoryId
        {
            get
            {
                Guid cid;
                if(Guid.TryParse(Request.QueryString["cid"],out cid))
                    return cid;

                return null;
            }
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