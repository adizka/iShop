using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;



namespace BL.Modules.Products
{
    public class Products
    {
        public bool AddProduct(string name, string unit, decimal price, bool isVisible, int count, decimal tax, decimal shipping, out BL.Product product)
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
                product.Tax = tax;
                product.Shipping = shipping;
                db.Products.InsertOnSubmit(product);
                db.SubmitChanges();
                addProduct = ppbl.AddProductPhoto(BL.Site.DefaultPhotoPreview, BL.Site.DefaultPhotoOriginal, product.ProductID);
                addProduct = true;

            }
            return addProduct;
        }

        public bool UpdateProduct(Guid productId, string name, string unit, decimal price, bool isVisible, int count, decimal tax, decimal shipping)
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
                        product.Tax = tax;
                        product.Shipping = shipping;
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
                    prc.Sort = category.ProductsRefCategories.Count();
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

            foreach (var item in refs)
            {
                var catRef = item.Category.ProductsRefCategories.Where(s => s.Sort > item.Sort);
                foreach (var refs1 in catRef)
                {
                    refs1.Sort--;
                }
            }

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
            return db.Products.FirstOrDefault(p => p.ProductID == productId);
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

        public void CopyProperties(Guid fromID, Guid toID)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var fromProd = db.Products.First(p => p.ProductID == fromID);
                var toProd = db.Products.First(p => p.ProductID == toID);

                List<BL.ProductProperty> newProps = new List<BL.ProductProperty>();
                List<BL.ProductsRefProperty> newRefs = new List<ProductsRefProperty>();

                foreach (var item in fromProd.ProductProperties.Where(p =>
                        p.PropertyName != ProductPropertyConstants.ProductPhotoPreview
                        && p.PropertyName != ProductPropertyConstants.ProductPhotoOriginal).OrderBy(pp => pp.Sort))
                {
                    var newProp = new ProductProperty()
                    {
                        IsImportant = item.IsImportant,
                        PropertyID = Guid.NewGuid(),
                        PropertyName = item.PropertyName,
                        PropertyValue = item.PropertyValue,
                        Sort = newProps.Count
                    };
                    newProps.Add(newProp);
                    newRefs.Add(new ProductsRefProperty()
                    {
                        ID = Guid.NewGuid(),
                        ProductPropertiesID = newProp.PropertyID,
                        Sort = newRefs.Count
                    });
                }
                toProd.ProductProperties.AddRange(newProps);
                toProd.ProductsRefProperies.AddRange(newRefs);

                db.SubmitChanges();
            }
        }

        public IQueryable<Product> GetAllProducts()
        {
            return (new ShopDataContext()).Products;
        }

        public IQueryable<BL.Product> GetMostPopularProducts()
        {
            var db = new ShopDataContext();
            return db.Products.Take(10);
        }

        public IQueryable<ProductData> GetProductsByProductName(string name)
        {
            return new ShopDataContext().GetProductDataByProductName(name);
        }

        public IQueryable<ProductData> GetProductsByCategoryName(string name)
        {
            return new ShopDataContext().GetProductDataByCategoryName(name);
        }
        public void DeleteProduct(Guid prodID)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var prod = db.Products.FirstOrDefault(p => p.ProductID == prodID);
                if (prod == null)
                    return;

                db.ProductsRefCategories.DeleteAllOnSubmit(prod.ProductsRefCategories);
                db.OrdersRefProducts.DeleteAllOnSubmit(prod.OrdersRefProducts);
                db.ProductsRefProperies.DeleteAllOnSubmit(prod.ProductsRefProperies);
                db.ProductProperties.DeleteAllOnSubmit(prod.ProductProperties);
                db.Stocks.DeleteAllOnSubmit(prod.Stocks);
                db.Products.DeleteOnSubmit(prod);
                db.SubmitChanges();
            }
        }
    }
}