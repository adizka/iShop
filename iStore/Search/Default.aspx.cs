using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace iStore.Search
{
    public partial class Default : System.Web.UI.Page
    {
        enum SearchType
        {
            Product = 0,
            Category = 1
        }
        public BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductRefCategories prcbl = new BL.Modules.Products.ProductRefCategories();
        private BL.ProductDataComparer comparer = new BL.ProductDataComparer();
        protected void Page_Load(object sender, EventArgs e)
        {
            pager.EntityCount = AllProducts.Distinct(comparer).Count();
        }

        List<BL.ProductData> _allProducts;
        protected List<BL.ProductData> AllProducts
        {
            get
            {
                if (_allProducts != null)
                    return _allProducts;

                if (searchType == SearchType.Category)
                    _allProducts = pbl.GetProductsByCategoryName(SearchCriteria).ToList();
                else
                    _allProducts = pbl.GetProductsByProductName(SearchCriteria).ToList();

                return _allProducts;
            }
        }

        Dictionary<Guid, IGrouping<Guid, BL.ProductData>> _PageProducts;
        protected Dictionary<Guid, IGrouping<Guid, BL.ProductData>> PageProducts
        {
            get
            {
                if (_PageProducts == null)
                    _PageProducts = AllProducts.GroupBy(g => g.ProductID)
                        .Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage).ToDictionary(x => x.Key);

                return _PageProducts;
            }
        }

        protected List<BL.Category> Categories
        {
            get
            {
                return cbl.GetAllCategories().Where(c => c.Name.IndexOf(SearchCriteria) != -1).ToList();
            }
        }

        protected string GetPreviewUrl(BL.ProductData prod)
        {
            return (prod == null) ? BL.Site.DefaultPhotoPreview : prod.PropertyValue;
        }

        protected string GetRenderedControl(BL.ProductData item)
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

        SearchType searchType
        {
            get
            {
                var result = 0;
                return int.TryParse(Request.QueryString["t"], out result) ? (result >= 0 && result <= 1) ? (SearchType)result : SearchType.Category : SearchType.Category;
            }
        }

        string _searchCriteria;
        string SearchCriteria
        {
            get
            {
                if (_searchCriteria == null)
                    _searchCriteria = (Request.QueryString["s"] != null) ? Request.QueryString["s"] : string.Empty;

                return _searchCriteria;
            }
        }
    }
}