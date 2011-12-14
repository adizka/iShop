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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Product == null)
                Response.Redirect(iStore.Site.SiteAdminUrl + "Products/");
        }
        List<ProductProperty> _ProductsProperies;
        protected List<ProductProperty> ProductsProperies
        {
            get
            {
                if (_ProductsProperies == null)
                    _ProductsProperies = Product.ProductProperties.Where(p => p.PropertyName != "ProductPhotoPreview" && p.PropertyName != "ProductPhotoOriginal").OrderBy(p => p.Sort).ToList();

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
        }
    }
}