using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using BL;

namespace iStore.Admin.Products
{
    public partial class ProductPropertyEdit : System.Web.UI.Page
    {
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        BL.Modules.Products.ProductProperies ppbl = new BL.Modules.Products.ProductProperies();
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Product == null)
                Response.Redirect(iStore.Site.SiteAdminUrl + "Products/");

            if (!IsPostBack)
            {
                var relatedProds = Product.ProductsRefCategories.SelectMany(c => c.Category.ProductsRefCategories.Select(r => r.Product)).
                Distinct().Where(p => p.ProductID != Product.ProductID).OrderBy(p => p.Name).ToList();

                if (relatedProds.Count == 0)
                    FromCat.Visible = false;

                cpddl.DataSource = relatedProds;
                cpddl.DataTextField = "Name";
                cpddl.DataValueField = "ProductID";
                cpddl.DataBind();
                var allProds = pbl.GetAllProducts().ToList();
                apddl.DataSource = allProds.Where(p => p.ProductID != Product.ProductID).OrderBy(p => p.Name);

                if (allProds.Count == 1)
                    FromAllCat.Visible = false;

                apddl.DataTextField = "Name";
                apddl.DataValueField = "ProductID";
                apddl.DataBind();
            }
        }
        List<ProductProperty> _ProductsProperies;
        protected List<ProductProperty> ProductsProperies
        {
            get
            {
                if (_ProductsProperies == null)
                    _ProductsProperies = Product.ProductProperties.Where(p =>
                        p.PropertyName != ProductPropertyConstants.ProductPhotoPreview
                        && p.PropertyName != ProductPropertyConstants.ProductPhotoOriginal
                            && p.PropertyName != ProductPropertyConstants.ProductDescription
                                && p.PropertyName != ProductPropertyConstants.ProductPhotoOriginal2
                                    && p.PropertyName != ProductPropertyConstants.ProductPhotoOriginal3).OrderBy(p => p.Sort).ToList();

                return _ProductsProperies;
            }
        }

        Product _product;
        protected Product Product
        {
            get
            {
                if (_product == null)
                {
                    Guid pid;
                    if (!Guid.TryParse(Request.QueryString["pid"], out pid))
                        return null;
                    _product = pbl.GetProductById(pid);
                }
                return _product;
            }
        }

        protected void Copy(object obj, EventArgs args)
        {
            Guid prodID;
            if (!Guid.TryParse(obj.Equals(AddPropertiesbtn1)
                ? cpddl.SelectedValue
                : apddl.SelectedValue, out prodID))
                return;

            pbl.CopyProperties(prodID, Product.ProductID);

        }
        private class Supporting
        {
            public int sort;
            public string name;
            public string val;
        }
        protected void Save(object obj, EventArgs args)
        {

            List<Supporting> strProps;

            try
            {
                strProps = hf.Value.Split(new string[] { "!~!!~!!~!!~!" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(el => el.Trim().Split(new string[] { "!~!!~!" }, StringSplitOptions.RemoveEmptyEntries))
                     .Select(e => new Supporting() { sort = int.Parse(e[2]), name = e[0], val = e[1] }).OrderBy(s => s.sort).ToList();
            }
            catch
            {
                divError.Visible = true;
                divError.InnerHtml = "Данные не были сохранены, перезагрузите страницу и попробуйте еще раз!";
                return;
            }

            var maxLen = 50;
            if (strProps.Any(p => p.name.Length > maxLen || p.val.Length > maxLen))
            {
                divError.Visible = true;
                divError.InnerHtml = "Длина названия свойства не может превышать" + maxLen.ToString() + " символов";
                return;
            }

            List<BL.ProductProperty> props = new List<ProductProperty>();
            foreach (var item in strProps)
            {
                props.Add(
                    new BL.ProductProperty()
                    {
                        PropertyName = Server.HtmlEncode(item.name),
                        PropertyValue = Server.HtmlEncode(item.val),
                        IsImportant = true,
                        Sort = item.sort
                    }
                );
            }

            ppbl.DeleteAllProperties(Product.ProductID);
            ppbl.AddProperties(Product.ProductID, props);
            divError.Visible = true;
            divError.InnerHtml = "Свойства продукта обновлены";
        }
    }
}