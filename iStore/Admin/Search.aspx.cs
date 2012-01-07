using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin
{
    public partial class Search : System.Web.UI.Page
    {
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        List<BL.Product> _products;
        protected List<BL.Product> Products
        {
            get
            {
                var keys = Key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct();


                if (_products == null)
                {
                    _products = new List<BL.Product>();

                    var all = pbl.GetAllProducts().ToList();
                    foreach (var item in keys)
                    {
                        _products.AddRange(pbl.GetAllProducts().Where(p => p.Name.IndexOf(item) > 0
                            && p.Price <= PriceTo && p.Price >= PriceFrom));
                    }
                    _products = _products.Distinct(new BL.ProductComparer()).ToList();
                }
                return _products;
            }
        }

        public int ProductsCount
        {
            get
            {
                return Products.Count;
            }
        }


        object _priceToInd;
        decimal _priceTo;
        decimal PriceTo
        {
            get
            {
                if (_priceToInd == null)
                {
                    if (!decimal.TryParse(Request.QueryString["prt"], out _priceTo))
                        _priceTo = decimal.MaxValue;
                    _priceToInd = new object();
                }
                return _priceTo;
            }
        }

        object _priceFromInd;
        decimal _priceFrom;
        decimal PriceFrom
        {
            get
            {
                if (_priceFromInd == null)
                {
                    if (!decimal.TryParse(Request.QueryString["prf"], out _priceFrom))
                        _priceFrom = 0;
                    _priceFromInd = new object();
                }
                return _priceFrom;
            }
        }

        string _Key;
        public string Key
        {
            get
            {
                if (_Key == null)
                {
                    _Key = Request.QueryString["key"];
                    _Key = (_Key == null) ? string.Empty : _Key;
                }

                return _Key;
            }
        }
    }
}