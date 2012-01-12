using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace BL.Modules.Stock
{
    public class Stock
    {
        public BL.Stock GetStockByProductId(Guid productId)
        {
            ShopDataContext db = new ShopDataContext();
            return db.Stocks.Where(s => s.ProductID == productId).FirstOrDefault();
        }

    }
}
