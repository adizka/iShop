using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Modules.Products
{
    public class ProductRefCategories
    {
        public bool AddProductRefCategoryes(Guid productId, Guid categoryId)
        {
            bool add = false;
            using (ShopDataContext db = new ShopDataContext())
            {
                BL.ProductsRefCategory prc = new BL.ProductsRefCategory();
                prc.ID = Guid.NewGuid();
                prc.ProductID = productId;
                prc.CategoryID = categoryId;
                prc.Sort = 0;
                db.ProductsRefCategories.InsertOnSubmit(prc);
                db.SubmitChanges();
                add = true;
            }
            return add;
        }

        public bool AddCategoriesToProduct(List<Guid> categoriesIds, Guid productId)
        {
            bool add = true;

            BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

            ShopDataContext db = new ShopDataContext();
            
            foreach (Guid item in categoriesIds)
            {
                add = pbl.AddCategoryToProduct(item, productId, db);
            }
            return add;
        }

        public IQueryable<BL.ProductsRefCategory> GetProductRefCategoriesByProductId(Guid productId)
        {
            ShopDataContext db = new ShopDataContext();
            return db.ProductsRefCategories.Where(p => p.ProductID == productId).OrderBy(p => p.Sort);
        }
    }
}
