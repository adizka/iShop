using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BL.Modules.Products
{
    public class ProductProperies
    {
        public enum ProductPhoto
        {
            ProductPhotoPreview,
            ProductPhotoOriginal
        }

        public void AddProperty(Guid productID, string propertyName, string propertyValue, bool isImportant)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var prod = db.Products.FirstOrDefault(p => p.ProductID == productID);
                BL.ProductProperty pr = new ProductProperty()
                                            {
                                                PropertyName = propertyName,
                                                PropertyValue = propertyValue,
                                                IsImportant = isImportant,
                                                PropertyID = Guid.NewGuid()
                                            };

                BL.ProductsRefProperty propRef = new ProductsRefProperty()
                                                     {
                                                         ID = Guid.NewGuid(),
                                                         ProductID = prod.ProductID,
                                                         ProductPropertiesID = pr.PropertyID,
                                                         Sort = prod.ProductProperties.Count
                                                     };
                prod.ProductProperties.Add(pr);
                prod.ProductsRefProperies.Add(propRef);
                db.SubmitChanges();
            }
        }

        public bool AddProperties(Guid productID, List<BL.ProductProperty> props)
        {
            bool allRight = false;
            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Product prod = db.Products.FirstOrDefault(p => p.ProductID == productID);
                if (prod != null)
                {
                    int index = 0;
                    foreach (var item in props)
                    {
                        BL.ProductProperty pr = new ProductProperty()
                                                    {
                                                        Sort = item.Sort,
                                                        PropertyName = item.PropertyName,
                                                        PropertyValue = item.PropertyValue,
                                                        IsImportant = item.IsImportant,
                                                        PropertyID = Guid.NewGuid()
                                                    };
                        prod.ProductProperties.Add(pr);
                        BL.ProductsRefProperty pref = new ProductsRefProperty()
                        {
                            ProductPropertiesID = pr.PropertyID,
                            Sort = index,
                            ID = Guid.NewGuid()
                        };
                        prod.ProductsRefProperies.Add(pref);


                        index++;
                    }
                    db.SubmitChanges();
                    allRight = true;
                }
            }
            return allRight;
        }

        public void DeleteProperty(Guid propertyID)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var pr = db.ProductProperties.FirstOrDefault(p => p.PropertyID == propertyID);

                if (pr != null)
                {
                    db.ProductProperties.DeleteOnSubmit(pr);
                    db.SubmitChanges();
                }
            }
        }


        public void DeleteAllProperties(Guid propertyID)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var prod = db.Products.FirstOrDefault(p => p.ProductID == propertyID);
                if (prod == null) return;
                db.ProductsRefProperies.DeleteAllOnSubmit(db.ProductsRefProperies);
                db.ProductProperties.DeleteAllOnSubmit(prod.ProductProperties);
                db.SubmitChanges();
            }
        }

        public bool AddProductPhoto(string previewUrl, string originalUrl, Guid productId)
        {
            bool allRight = false;
            using (var db = new ShopDataContext())
            {
                BL.ProductProperty productPropertyPreview = new BL.ProductProperty();
                BL.ProductProperty productPropertyOriginal = new BL.ProductProperty();
                try
                {
                    using (var ts = new TransactionScope())
                    {
                        productPropertyPreview.PropertyID = Guid.NewGuid();
                        productPropertyOriginal.PropertyID = Guid.NewGuid();

                        productPropertyPreview.ProductID = productId;
                        productPropertyOriginal.ProductID = productId;

                        productPropertyPreview.PropertyName = ProductPhoto.ProductPhotoPreview.ToString();
                        productPropertyOriginal.PropertyName = ProductPhoto.ProductPhotoOriginal.ToString();

                        productPropertyPreview.PropertyValue = previewUrl;
                        productPropertyOriginal.PropertyValue = originalUrl;

                        productPropertyPreview.IsImportant = true;
                        productPropertyOriginal.IsImportant = true;

                        productPropertyPreview.Sort = 0;
                        productPropertyOriginal.Sort = 0;

                        db.ProductProperties.InsertOnSubmit(productPropertyPreview);
                        db.SubmitChanges();

                        db.ProductProperties.InsertOnSubmit(productPropertyOriginal);
                        db.SubmitChanges();

                        ts.Complete();
                        allRight = true;
                    }
                }
                catch (Exception)
                {
                    allRight = false;
                }
                return allRight;
            }
        }

        public string GetProductPreviewByProductId(Guid productId)
        {
            ShopDataContext db = new ShopDataContext();
            BL.ProductProperty productProperty = db.ProductProperties.Where(p => p.ProductID == productId && p.PropertyName == ProductPhoto.ProductPhotoPreview.ToString()).FirstOrDefault();
            if (productProperty != null)
            {
                return productProperty.PropertyValue;
            }
            return string.Empty;
        }

        public string GetProductOriginalByProductId(Guid productId)
        {
            ShopDataContext db = new ShopDataContext();
            BL.ProductProperty productProperty = db.ProductProperties.Where(p => p.ProductID == productId && p.PropertyName == ProductPhoto.ProductPhotoOriginal.ToString()).FirstOrDefault();
            if (productProperty != null)
            {
                return productProperty.PropertyValue;
            }
            return string.Empty;
        }

        public bool UpdateProductPhotoPreview(string url, Guid productId)
        {
            bool allRight = false;
            ShopDataContext db = new ShopDataContext();
            BL.ProductProperty productProperty = db.ProductProperties.Where(p => p.ProductID == productId && p.PropertyName == ProductPhoto.ProductPhotoPreview.ToString()).FirstOrDefault();
            if (productProperty != null)
            {
                productProperty.PropertyValue = url;
                db.SubmitChanges();
                allRight = true;
            }
            return allRight;
        }

        public bool UpdateProductPhotoOriginal(string url, Guid productId)
        {
            bool allRight = false;
            ShopDataContext db = new ShopDataContext();
            BL.ProductProperty productProperty = db.ProductProperties.Where(p => p.ProductID == productId && p.PropertyName == ProductPhoto.ProductPhotoOriginal.ToString()).FirstOrDefault();
            if (productProperty != null)
            {
                productProperty.PropertyValue = url;
                db.SubmitChanges();
                allRight = true;
            }
            return allRight;
        }

        public void AddProductProperty(string propertyName, string propertyValue, Guid productID)
        {

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Product prod = db.Products.FirstOrDefault(p => p.ProductID == productID);
                if (prod == null)
                    return;

                BL.ProductsRefProperty refProp = new ProductsRefProperty()
                {
                    ID = Guid.NewGuid(),
                    Sort = prod.ProductsRefProperies.Count,
                    ProductID = productID
                };

                BL.ProductProperty prop = new ProductProperty()
                {
                    IsImportant = true,
                    PropertyID = Guid.NewGuid(),
                    PropertyName = BL.ProductPropertyConstants.ProductDescription,
                    PropertyValue = propertyValue,
                    Sort = prod.ProductProperties.Count,
                };
                prop.ProductsRefProperies.Add(refProp);
                prod.ProductProperties.Add(prop);
                db.SubmitChanges();
            }
        }
    }
}
