using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BL.Modules.Products
{
    class ProductProperies
    {
        public bool AddProduct(string name, string unit, string price, bool isVisible, int count)
        {
            bool addProduct = false;
            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Product product = new Product();
                BL.Stock stock = new BL.Stock();
                using (var ts = new TransactionScope())
                {
                    product.ProductID = Guid.NewGuid();
                    product.Name = name;
                    product.CreateDate = DateTime.Now;
                    product.Unit = unit;
                    product.Price = price;
                    product.IsVisible = (count > 0);
                    product.IsVisible = isVisible;
                    db.Products.InsertOnSubmit(product);
                    db.SubmitChanges();
                    stock.StockItemID = Guid.NewGuid();
                    stock.ProductID = product.ProductID;
                    stock.Count = count;
                    db.Stocks.InsertOnSubmit(stock);
                    db.SubmitChanges();
                    ts.Complete();
                    addProduct = true;
                }
            }
            return addProduct;
        }

        public bool UpdateProduct(Guid productId, string name, string unit, string price, bool isVisible, int count)
        {
            bool updateProduct = false;

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Product product = GetProductById(productId);
                if (product != null)
                {
                    BL.Stock stock = db.Stocks.Where(s => s.ProductID == productId).FirstOrDefault();
                    if (stock != null)
                    {
                        using (var ts = new TransactionScope())
                        {
                            product.Name = name;
                            product.Unit = unit;
                            product.Price = price;
                            product.IsVisible = isVisible;
                            product.InStock = (count > 0);
                            updateProduct = true;
                            db.SubmitChanges();
                            stock.Count = count;
                            db.SubmitChanges();
                            ts.Complete();
                        }
                    }
                }
            }
            return updateProduct;
        }

        public bool AddCategoryToProduct(Guid categoryId, Guid productId)
        {
            bool addCategoryToProduct = false;

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Category category = db.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault();
                if (category != null)
                {
                    BL.Product product = db.Products.Where(p => p.ProductID == productId).FirstOrDefault();
                    if (product != null)
                    {
                        BL.ProductsRefCategory prc = new ProductsRefCategory();
                        prc.ID = Guid.NewGuid();
                        prc.CategoryID = categoryId;
                        prc.ProductID = productId;
                        db.ProductsRefCategories.InsertOnSubmit(prc);
                        db.SubmitChanges();
                        addCategoryToProduct = true;
                    }
                }
            }
            return addCategoryToProduct;
        }

        public bool DeleteCategoryFromProduct(Guid categoryId, Guid productId)
        {
            bool deleteCategoryFromProduct = false;
            using (ShopDataContext db = new ShopDataContext())
            {
                BL.ProductsRefCategory prc = db.ProductsRefCategories.Where(p => p.ProductID == productId && p.CategoryID == categoryId).FirstOrDefault();
                if (prc != null)
                {
                    db.ProductsRefCategories.DeleteOnSubmit(prc);
                    db.SubmitChanges();
                }
            }
            return deleteCategoryFromProduct;
        }

        public BL.Product GetProductById(Guid productId)
        { 
            ShopDataContext db = new ShopDataContext();
            return db.Products.Where(p => p.ProductID == productId).FirstOrDefault();
        }

        public IQueryable<BL.Product> GetProductByCategories(Guid categoryId)
        {
            ShopDataContext db = new ShopDataContext();
            List<Guid> ids = new List<Guid>();
            IQueryable<BL.ProductsRefCategory> rpcs = db.ProductsRefCategories.Where(r => r.CategoryID == categoryId);
            if (rpcs.Any())
            {
                foreach (var item in rpcs)
                {
                    ids.Add(item.ProductID);
                }
                List<BL.Product> products = new List<Product>();
                foreach (var item in ids)
                {
                    try 
                    {
                        products.Add(db.Products.Where(p => p.ProductID == item).FirstOrDefault());    
                    }
                    catch 
                    {
                        return null;
                    }
                }
                return products.AsQueryable();
            }
            return null;           
        }
    }
}
