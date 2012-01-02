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
                Distinct().Where(p => p.ProductID != Product.ProductID).OrderBy(p => p.Name);

                cpddl.DataSource = relatedProds;
                cpddl.DataTextField = "Name";
                cpddl.DataValueField = "ProductID";
                cpddl.DataBind();
                var allProds = pbl.GetAllProducts().ToList();
                apddl.DataSource = allProds.Where(p => p.ProductID != Product.ProductID).OrderBy(p => p.Name);
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
                        && p.PropertyName != ProductPropertyConstants.ProductPhotoOriginal).OrderBy(p => p.Sort).ToList();

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
            Guid prodID = (obj.Equals(AddPropertiesbtn1)) ?
                new Guid(cpddl.SelectedValue)
                : new Guid(apddl.SelectedValue);


            pbl.CopyProperties(prodID, Product.ProductID);

        }
        protected void Save(object obj, EventArgs args)
        {

            var strProps = hf.Value.Split(new string[] { "!~!!~!!~!!~!" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(el => el.Trim().Split(new string[] { "!~!!~!" }, StringSplitOptions.RemoveEmptyEntries))
                 .Select(e => new {Sort = int.Parse(e[2]), Name = e[0], Val = e[1]  }).OrderBy(s => s.Sort).ToList();


            List<BL.ProductProperty> props = new List<ProductProperty>();
            foreach (var item in strProps)
            {
                props.Add(
                    new BL.ProductProperty()
                    {
                        PropertyName = Server.HtmlEncode(item.Name),
                        PropertyValue = Server.HtmlEncode(item.Val),
                        IsImportant = true,
                        Sort = item.Sort 
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