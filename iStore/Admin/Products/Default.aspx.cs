using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Modules.Products;

namespace iStore.Admin.Products
{
    public partial class Default : System.Web.UI.Page
    {
        public  BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        public  BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public  BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AllChildCategories != null)
                {
                    pager.EntityCount = ProductsCount;
                    ddlChildCategories.DataSource = AllChildCategories;
                    ddlChildCategories.DataValueField = "CategoryID";
                    ddlChildCategories.DataTextField = "Name";
                    ddlChildCategories.DataBind();
                    if (AllChildCategories.Count() < 1)
                    {
                        ddlChildCategories.Visible = false;
                        btnRedirect.Visible = false;
                    }
                }
                else
                {
                    ddlChildCategories.Visible = false;
                    btnRedirect.Visible = false;
                }
            }
        }

        protected  void RedirectToSelectedCategory(object sender, EventArgs e)
        {
            string hf = ddlChildCategories.SelectedItem.Value;
            Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + hf);
        }

        object _prodCountInd;
        int _ProductsCount;
        public int ProductsCount
        {
            get
            {
                if (_prodCountInd == null)
                {
                    _ProductsCount = pbl.GetAllProducts().Count();
                    _prodCountInd = new object();
                }

                return _ProductsCount;
            }
        }

        public Guid? CurrentCategoryId
        {
            get 
            { 
                string scid = Request.QueryString["cid"];
                Guid cid;
                try
                {
                    cid = new Guid(scid);
                }
                catch
                {
                    return null;
                }
                return cid;
            }
        }

        public IQueryable<BL.Category> AllChildCategories
        {
            get
            {
                if (CurrentCategoryId == null) return cbl.GetAllRootCatgories();
                try
                {
                    return cbl.GetCategoriesByParentId(CurrentCategoryId);
                }
                catch
                {
                    return null;
                }
            }
        }

        List<BL.ProductsRefCategory> _PageProducts;
        protected List<BL.ProductsRefCategory> PageProducts
        {
            get
            {
                if (CurrentCategoryId == null)
                {
                    if (_PageProducts == null)
                        _PageProducts = prcbl.GetAllProductsRefCategories().ToArray().Distinct(new BL.ProductsRefCategoryComparer())
                            .Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage).OrderBy(p => p.Sort).ToList();
                }
                else
                {
                    if (_PageProducts == null)
                        _PageProducts = prcbl.GetProductRefCategoriesByCategoryId(CurrentCategoryId.Value).ToArray()
                            .Distinct(new BL.ProductsRefCategoryComparer())
                            .Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage).OrderBy(p => p.Sort).ToList();
                }
                return _PageProducts;
            }
        }
        protected void DeleteProduct(object obj, EventArgs args)
        {
            Guid prodID;
            if(Guid.TryParse(hf.Value, out prodID))
                pbl.DeleteProduct(prodID);
        }
    }
}