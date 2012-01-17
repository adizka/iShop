using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Products
{
    public partial class Default : System.Web.UI.Page
    {

        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentProduct == null)
                Response.Redirect(iStore.Site.SiteUrl);

            CurrProdAddToCart.ProductId = CurrentProduct.ProductID;
            rpt.DataSource = RelatedProducts;
            rpt.DataBind();
            Page.Title = iStore.Modules.Controls.PageTitle.Get(CurrentProduct.Name);
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

        List<BL.ProductProperty> _productProperties;
        protected List<BL.ProductProperty> ProductProperties
        {
            get
            {
                if (_productProperties == null)
                    _productProperties = CurrentProduct.ProductProperties
                        .Where(p => p.PropertyName != BL.ProductPropertyConstants.ProductDescription
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoPreview
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal2
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal3).Take(8).ToList();
                return _productProperties;
            }
        }

        string _ProductPreviewUrl;
        protected string ProductPreviewUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_ProductPreviewUrl))
                {
                    var prodProp = CurrentProduct.ProductProperties.FirstOrDefault(p => p.PropertyName == BL.ProductPropertyConstants.ProductPhotoPreview);
                    _ProductPreviewUrl = (prodProp == null) ? BL.Site.DefaultPhotoPreview : prodProp.PropertyValue;
                }
                return _ProductPreviewUrl;
            }
        }

        protected int counter = -1;
        protected string GetPreviewUrl()
        {
            var prodProp = RelatedProducts[counter].ProductProperties.FirstOrDefault(p => p.PropertyName == BL.ProductPropertyConstants.ProductPhotoPreview);
            return (prodProp == null) ? BL.Site.DefaultPhotoPreview : prodProp.PropertyValue;
        }


        BL.Product _CurrentProduct;
        protected BL.Product CurrentProduct
        {
            get
            {
                if (_CurrentProduct == null)
                {
                    Guid pid;
                    if (Guid.TryParse(Request.QueryString["pid"], out pid))
                        _CurrentProduct = pbl.GetProductById(pid);
                }
                return _CurrentProduct;
            }
        }
        List<BL.Product> _RelatedProducts;
        protected List<BL.Product> RelatedProducts
        {
            get
            {
                if (_RelatedProducts == null)
                {
                    var cat = CurrentProduct.ProductsRefCategories.FirstOrDefault(s => s.Sort == 0);
                    BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();
                    _RelatedProducts = prcbl.GetAllProductsRefCategories()
                        .Where(c => ChildCategories.Contains(c.CategoryID) && c.ProductID != CurrentProduct.ProductID)
                        .Select(pr => pr.Product).Distinct().Take(4).ToList();
                }

                return _RelatedProducts;
            }
        }

        List<Guid> _childCategories;
        List<Guid> ChildCategories
        {
            get
            {
                if (_childCategories != null)
                    return _childCategories;

                BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
                _childCategories = new List<Guid>();
                foreach (var item in CurrentProduct.ProductsRefCategories.OrderBy(s => s.Sort))
                {
                    _childCategories.AddRange(cbl.GetAllNestedCategories(item.CategoryID));
                    _childCategories = _childCategories.Distinct().ToList();
                }

                return _childCategories;
            }
        }

        public string ProductOriginalPhoto(string num)
        {
            //ProductPhotoOriginal
            string propName = "ProductPhotoOriginal" + num;
            BL.ProductProperty productProperty = CurrentProduct.ProductProperties.Where(p => p.PropertyName == propName).FirstOrDefault();
            if (productProperty != null)
            {
                string propValue = productProperty.PropertyValue;
                return iStore.Site.PreUrlProductOriginalImage + propValue;
            }
            return string.Empty;
        }
    }
}
