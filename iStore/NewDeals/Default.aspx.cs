using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iStore.Modules.Controls;

namespace iStore.NewDeals
{
    public partial class Default : System.Web.UI.Page
    {
        public BL.Modules.Categories.Categories Cbl = new BL.Modules.Categories.Categories();
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductProperies Ppbl = new BL.Modules.Products.ProductProperies();

        protected void Page_Load(object sender, EventArgs e)
        {
            pager.EntityCount = GetProductsRefCurrentCategory.Count();
        }


        public IQueryable<BL.ProductsRefCategory> GetProductsRefCurrentCategory
        {
            get
            {
                return pbl.GetAllProducts().OrderByDescending(p=>p.CreateDate).Select(r=>r.ProductsRefCategories.First());
            }
        }

        public IQueryable<BL.ProductsRefCategory> GetPageProductsRefCurrentCategory
        {
            get
            {
                return GetProductsRefCurrentCategory.Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage);
            }
        }

        public IList<BL.ProductsRefCategory> GetProductsRefCurrentCategoryList
        {
            get
            {
                return GetProductsRefCurrentCategory.ToList();
            }
        }


        public string GetProductPreview(Guid productId)
        {
            return Ppbl.GetProductPreviewByProductId(productId);
        }

        public IQueryable<BL.ProductProperty> GetProductPropery(Guid productId)
        {
            return Ppbl.GetAllProperyByProductId(productId).Where(p => p.PropertyName != BL.ProductPropertyConstants.ProductDescription
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoPreview
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal2
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal3).Take(6);
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
