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
                BL.ProductProperty pr = new ProductProperty();
                pr.ProductID = productID;
                pr.PropertyName = propertyName;
                pr.PropertyValue = propertyValue;
                pr.IsImportant = isImportant;
                pr.PropertyID = Guid.NewGuid();
                db.ProductProperties.InsertOnSubmit(pr);
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

    }
}
