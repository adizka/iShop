using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Modules.Stock;
using BL.Modules.Products;

namespace iStore.Admin.Products
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();        
        Stock sbl = new Stock();
        ProductRefCategories prcbl = new ProductRefCategories();

        protected void Page_Load(object sender, EventArgs e)
        {
            hf.Value = string.Empty;
            if (!IsPostBack)
            {
                BL.Product product = currentProduct;
                if (product != null)
                {
                    BL.Stock stock = sbl.GetStockByProductId(product.ProductID);
                    if (stock != null)
                    {
                        txtName.Text = product.Name;
                        txtPrice.Text = product.Price;
                        txtCount.Text = stock.Count.ToString();
                        txtUnit.Text = product.Unit;
                    }
                }
            }
        }

        #region SaveProduct
        protected void Save(object sender, EventArgs e)
        {
            BL.Product product = currentProduct;
            string name = Server.HtmlEncode(txtName.Text);
            string unit = Server.HtmlEncode(txtUnit.Text);
            string price = Server.HtmlEncode(txtPrice.Text);
            string scount = Server.HtmlEncode(txtCount.Text);
            if (!CheckAll(name, unit, price, scount)) { return; }
            int count = 0;

            try { count = Convert.ToInt32(scount); }
            catch { Response.Redirect(Request.Url.AbsolutePath); }

            if (product == null)
            {
                bool isAdd = pbl.AddProduct(name, unit, price, chkVisible.Checked, count);
                if (isAdd)
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Products/");
                }
                else
                {
                    ve.Visible = true;
                    ve.ClearErrors();
                    ve.Errors = "Продукт не был добавлен.";
                    ve.SetErrors();
                    return;
                }
            }
            else
            {
                bool isUpdate = pbl.UpdateProduct(product.ProductID, name, unit, price, chkVisible.Checked, count);
                if (isUpdate)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    ve.Visible = true;
                    ve.ClearErrors();
                    ve.Errors = "Продукт не был обновлён.";
                    ve.SetErrors();
                    return;
                }
            }
        }
        #endregion

        #region Check
        private bool CheckAll(string name, string unit, string price, string count)
        {
            if (string.IsNullOrEmpty(name))
            {
                ve.Visible = true;
                ve.ClearErrors();
                ve.Errors = "Не заполненно поле Name";
                ve.SetErrors();
                return false;
            }
            if (string.IsNullOrEmpty(unit))
            {
                ve.Visible = true;
                ve.ClearErrors();
                ve.Errors = "Не заполненно поле Unit";
                ve.SetErrors();
                return false;
            }
            if (string.IsNullOrEmpty(price))
            {
                ve.Visible = true;
                ve.ClearErrors();
                ve.Errors = "Не заполненно поле Price";
                ve.SetErrors();
                return false;
            }
            if (string.IsNullOrEmpty(count))
            {
                ve.Visible = true;
                ve.ClearErrors();
                ve.Errors = "Не заполненно поле Count";
                ve.SetErrors();
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

        public BL.Product currentProduct
        {
            get
            {
                string sid = Request.QueryString["pid"];
                if (string.IsNullOrEmpty(sid))
                {
                    return null;
                }
                try
                {
                    Guid id = new Guid(sid);
                    BL.Product product = pbl.GetProductById(id);
                    if (product != null)
                    {
                        return product;
                    }
                }
                catch { Response.Redirect(iStore.Site.SiteAdminUrl + "Products/"); }
                return null;
            }
        }

        public IQueryable<BL.ProductsRefCategory> allCategoriesRefsCurrentProduct
        {
            get
            {
                BL.Product product = currentProduct;
                if (product != null)
                {
                    return prcbl.GetProductRefCategoriesByProductId(product.ProductID);
                }
                return null;
            }
        }

        public bool CategoriInDB(Guid categoryId)
        {
            BL.Category category = currentCategory;
            if (category != null)
            {
                if (categoryId == category.CategoryID) return true;
            }
            IQueryable<BL.ProductsRefCategory> cpr = allCategoriesRefsCurrentProduct;
            if (cpr == null) { return false; }
            if (!cpr.Any()) { return false; }
            List<Guid> Ids = new List<Guid>();
            foreach (var item in cpr)
            {
                Ids.Add(item.CategoryID);
            }
            Guid id = Ids.Find(p => p == categoryId);

            return (id != null);
        }
    }
}