using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

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

        public bool UpdateCategoriesToProduct(List<Guid> categoriesIds, Guid productId)
        {
            
            BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();

            pbl.DeleteProductCategories(productId);
            return AddCategoriesToProduct(categoriesIds, productId); 
        }

        public  IQueryable<BL.ProductsRefCategory> GetAllProductsRefCategories()
        {
            ShopDataContext db = new ShopDataContext();
            return db.ProductsRefCategories.OrderBy(p => p.CategoryID);
        }

        public IQueryable<BL.ProductsRefCategory> GetProductRefCategoriesByProductId(Guid productId)
        {
            return GetAllProductsRefCategories().Where(p => p.ProductID == productId).OrderBy(p => p.Sort);
        }

        public  IQueryable<BL.ProductsRefCategory> GetProductRefCategoriesByCategoryId(Guid categoryId)
        {
            return GetAllProductsRefCategories().Where(p => p.CategoryID == categoryId).OrderBy(s => s.Sort);
        }

        public void UpdateProductsRefCategoriesSort(List<Guid> prodIds)
        {
            using (var db = new ShopDataContext())
            {
                using (var ts = new TransactionScope())
                {
                    var index = 0;
                    foreach (var item in prodIds)
                    {
                        var categ = db.ProductsRefCategories.First(c => c.ID == item);
                        categ.Sort = index;
                        index++;
                    }
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }
    }
}
