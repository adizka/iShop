﻿using System;
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
            var a = fu.FileName;
            BL.Product product = currentProduct;
            string name = Server.HtmlEncode(txtName.Text);
            string unit = Server.HtmlEncode(txtUnit.Text);
            string price = Server.HtmlEncode(txtPrice.Text);
            string scount = Server.HtmlEncode(txtCount.Text);
            if (!CheckAll(name, unit, price, scount)) { return; }
            int count = 0;

            try { count = Convert.ToInt32(scount); }
            catch { Response.Redirect(Request.Url.AbsolutePath); }


            var categoriesIDs = hf.Value.Split(new string[] { "!~!" }, StringSplitOptions.RemoveEmptyEntries).Select(id => new Guid(id)).ToList();


            if (product == null)
            {
                bool isAdd = pbl.AddProduct(name, unit, price, chkVisible.Checked, count, out product);
                if (isAdd)
                {
                    prcbl.AddCategoriesToProduct(categoriesIDs, product.ProductID);
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
                    prcbl.UpdateCategoriesToProduct(categoriesIDs, product.ProductID);
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

            var extension = System.IO.Path.GetExtension(fu.FileName);

            if (fu.HasFile && fu.FileBytes.Length > (1 << 20) && (extension == ".jpg" || extension == ".png" || extension == ".gif"))
            {
                var path =  ConfigurationManager.AppSettings["ProductsFileStoragePath"]+Guid.NewGuid().ToString();
                fu.SaveAs(path);
                File.WriteAllBytes(path, fu.FileBytes);
                prop.AddProperty(product.ProductID, "Picture",path, true);
            }
            var redirectURL = ((product == null) ? iStore.Site.SiteAdminUrl + "Products/" : Request.Url.AbsolutePath);
            Response.Redirect(redirectURL);
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