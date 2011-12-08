using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BL.Modules.Products
{
    public class ProductProperies
    {
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

        public void AddProperties(Guid productID, List<BL.ProductProperty> props)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var prod = db.Products.FirstOrDefault(p => p.ProductID == productID);

                foreach (var item in props)
                {
                    BL.ProductProperty pr = new ProductProperty()
                    {
                        PropertyName = item.PropertyName,
                        PropertyValue = item.PropertyValue,
                        IsImportant = item.IsImportant,
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
                }
                db.SubmitChanges();
            }
        }

        public void DeleteProperty(Guid propertyID)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var pr = db.ProductProperties.FirstOrDefault(p=>p.PropertyID == propertyID);

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

                if (prod == null)
                    return;


                db.ProductsRefProperies.DeleteAllOnSubmit(prod.ProductsRefProperies);
                db.ProductProperties.DeleteAllOnSubmit(prod.ProductProperties);
                db.SubmitChanges();
            }
        }
    }
}
