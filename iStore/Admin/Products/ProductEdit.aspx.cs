using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Modules.Stock;
using BL.Modules.Products;
using System.Data.Linq;
using System.IO;
using System.Configuration;

namespace iStore.Admin.Products
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        Stock sbl = new Stock();
        ProductRefCategories prcbl = new ProductRefCategories();
        ProductProperies prop = new ProductProperies();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BL.Product product = currentProduct;
                if (product != null)
                {
                    txtName.Text = product.Name;
                    txtPrice.Text = product.Price.ToString();
                    txtCount.Text = product.Count.ToString();
                    txtUnit.Text = product.Unit;
                    shippingTxt.Text = product.Shipping.ToString();
                    taxTxt.Text = product.Tax.ToString();
                }
                divError.InnerHtml = string.Empty;
                divError.Visible = false;
            }
        }

        #region SaveProduct
        protected void Save(object sender, EventArgs e)
        {
            divError.InnerHtml = string.Empty;
            divError.Visible = false;
            BL.Product product = currentProduct;
            string name = Server.HtmlEncode(txtName.Text.Trim());
            string unit = Server.HtmlEncode(txtUnit.Text.Trim());
            string scount = Server.HtmlEncode(txtCount.Text.Trim());
            string strShipping = Server.HtmlEncode(shippingTxt.Text.Trim());
            string strTax = Server.HtmlEncode(taxTxt.Text.Trim());
            var sprice = txtPrice.Text.Trim();

            if (!CheckAll(name, unit, scount, sprice, strTax, strShipping)) { return; }
            int count = Convert.ToInt32(scount);

            decimal tax = decimal.Parse(strTax);
            decimal shipping = decimal.Parse(strShipping);
            decimal price = decimal.Parse(sprice);


            var categoriesIDs = hf.Value.Split(new string[] { "!~!" }, StringSplitOptions.RemoveEmptyEntries).Select(id => new Guid(id)).Distinct().ToList();

            if (categoriesIDs.Count == 0)
            {
                divError.InnerHtml = "Please chouse categories";
                divError.Visible = true;
                return;
            }

            if (product == null)
            {
                bool isAdd = pbl.AddProduct(name, unit, price, chkVisible.Checked, count, tax, shipping, out product);
                if (isAdd)
                {
                    isAdd = prcbl.AddCategoriesToProduct(categoriesIDs, product.ProductID);
                }
                else
                {
                    divError.InnerHtml = "";
                    divError.Visible = true;
                    return;
                }
            }
            else
            {
                bool isUpdate = pbl.UpdateProduct(product.ProductID, name, unit, price, chkVisible.Checked, count, tax, shipping);
                if (isUpdate)
                {
                    prcbl.UpdateCategoriesToProduct(categoriesIDs, product.ProductID);
                }
                else
                {
                    divError.InnerHtml = "Product with the same name exist.";
                    divError.Visible = true;
                    return;
                }
            }
            Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + Request.QueryString["cid"]);
        }
        #endregion

        #region Check
        private bool CheckAll(string name, string unit, string count, string price, string tax, string shipping)
        {
            divError.InnerHtml = string.Empty;
            divError.Visible = false;
            int tmp;
            if (!int.TryParse(count, out tmp))
            {
                divError.InnerHtml = "Wrong count format";
                divError.Visible = true;
                return false;
            }
            decimal temp;
            if (!decimal.TryParse(price, out temp))
            {
                divError.InnerHtml = "Wrong price format";
                divError.Visible = true;
                return false;
            }

            if (!decimal.TryParse(tax, out temp))
            {
                divError.InnerHtml = "Wrong tax per unit format";
                divError.Visible = true;
                return false;
            }

            if (!decimal.TryParse(shipping, out temp))
            {
                divError.InnerHtml = "Wrong shipping format";
                divError.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                divError.InnerHtml = "Please enter Name";
                divError.Visible = true;
                return false;
            }

            if (name.Length > 50)
            {
                divError.InnerHtml = "Product name  must be no longer than 50 characters";
                divError.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(unit))
            {
                divError.InnerHtml = "Please enter Unit";
                divError.Visible = true;
                return false;
            }

            if (unit.Length > 10)
            {
                divError.InnerHtml = "Unit  must be no longer than 10 characters";
                divError.Visible = true;
                return false;
            }

            return true;
        }
        #endregion

        public IQueryable<BL.Category> allCategories
        {
            get
            {
                return cbl.GetAllCategories();
            }
        }

        public BL.Category currentCategory
        {
            get
            {
                string sid = Request.QueryString["cid"];
                if (string.IsNullOrEmpty(sid))
                {
                    return null;
                }
                try
                {
                    Guid id = new Guid(sid);
                    BL.Category category = cbl.GetCategoryById(id);
                    if (category != null)
                    {
                        return category;
                    }
                }
                catch { Response.Redirect(iStore.Site.SiteAdminUrl + "Categories/"); }
                return null;
            }
        }

        public BL.Product _currentProduct;
        public object _currentProductIndicator;

        public BL.Product currentProduct
        {
            get
            {
                if (_currentProductIndicator != null)
                    return _currentProduct;

                _currentProductIndicator = new object();

                string sid = Request.QueryString["pid"];
                if (string.IsNullOrEmpty(sid))
                {
                    return null;
                }
                try
                {
                    Guid id = new Guid(sid);
                    _currentProduct = pbl.GetProductById(id);
                    return _currentProduct;
                }
                catch { Response.Redirect(iStore.Site.SiteAdminUrl + "Products/"); }
                return null;
            }
        }

        List<BL.ProductsRefCategory> _allCategoriesRefsCurrentProduct;
        public List<BL.ProductsRefCategory> allCategoriesRefsCurrentProduct
        {
            get
            {
                if (_allCategoriesRefsCurrentProduct != null)
                    return _allCategoriesRefsCurrentProduct;

                if (currentProduct == null)
                    _allCategoriesRefsCurrentProduct = new List<BL.ProductsRefCategory>();

                else
                {
                    _allCategoriesRefsCurrentProduct = currentProduct.ProductsRefCategories.ToList();
                }
                return _allCategoriesRefsCurrentProduct;
            }
        }
    }
}