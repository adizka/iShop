using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BL.Modules.Products
{
    public class Products
    {
        public bool AddProduct(string name, string unit, float price, bool isVisible, int count, out BL.Product product, Guid? categoryID)
        {
            bool addProduct = false;
            using (ShopDataContext db = new ShopDataContext())
            {
                product = new BL.Product();
                BL.Modules.Products.ProductProperies ppbl = new ProductProperies();

               
                    product.ProductID = Guid.NewGuid();
                    product.Name = name;
                    product.CreateDate = DateTime.Now;
                    product.Unit = unit;
                    product.Price = price;
                    product.InStock = (count > 0);
                    product.IsVisible = isVisible;
                    product.ProductTypeID = (int)ProductType.Types.Real;
                    product.Count = count;
                    if (categoryID.HasValue)
                    {
                        var prodRefCat = new BL.ProductsRefCategory()
                        {
                            ID = Guid.NewGuid(),
                            CategoryID = categoryID.Value,
                            Sort = product.ProductsRefCategories.Count,
                        };
                        product.ProductsRefCategories.Add(prodRefCat);
                    }
                    db.Products.InsertOnSubmit(product);
                    db.SubmitChanges();
                    addProduct = ppbl.AddProductPhoto(BL.Site.DefaultPhotoPreview, BL.Site.DefaultPhotoOriginal, product.ProductID);
                    addProduct = true;
                
            }
            return addProduct;
        }

        public bool UpdateProduct(Guid productId, string name, string unit, float price, bool isVisible, int count)
        {
            bool updateProduct = false;

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Product product = GetProductById(productId);
                if (product != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        product.Name = name;
                        product.Unit = unit;
                        product.Price = price;
                        product.IsVisible = isVisible;
                        product.InStock = (count > 0);
                        product.Count = count;
                        updateProduct = true;
                        db.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
            return updateProduct;
        }

        public bool AddCategoryToProduct(Guid categoryId, Guid productId, ShopDataContext db)
        {
            bool addCategoryToProduct = false;

            BL.Category category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
            if (category != null)
            {
                BL.Product product = db.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product != null)
                {
                    BL.ProductsRefCategory prc = new ProductsRefCategory();
                    prc.ID = Guid.NewGuid();
                    prc.CategoryID = categoryId;
                    prc.ProductID = productId;
                    prc.Sort = product.ProductsRefCategories.Count;
                    product.ProductsRefCategories.Add(prc);
                    db.SubmitChanges();
                    addCategoryToProduct = true;
                 }
            }
            return addCategoryToProduct;
        }

        public void DeleteProductCategories(Guid productID)
        {

            BL.Product poducts = new BL.Product();

            BL.ShopDataContext ctx = new ShopDataContext();
            var refs = ctx.ProductsRefCategories.Where(r => r.ProductID == productID);

            ctx.ProductsRefCategories.DeleteAllOnSubmit(refs);
            ctx.SubmitChanges();
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

        public IQueryable<Product> GetAllProducts()
        {
            return (new ShopDataContext()).Products;
        }
    }
}
