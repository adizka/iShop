using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Modules.ShopProperties
{
    public class ShopProperties
    {
        public BL.ShopProperties AddProperty(string key, string value)
        {
            using (var db = new BL.ShopDataContext())
            {
                var prop =new BL.ShopProperties()
                {
                    ID= Guid.NewGuid(),
                    Key = key,
                    Value = value
                };
                db.ShopProperties.InsertOnSubmit(prop);
                db.SubmitChanges();
                return prop;
            }
        }
        
        public void DeleteProperty(Guid id)
        {
            using (var db = new BL.ShopDataContext())
            {
                var prop = db.ShopProperties.FirstOrDefault(p => p.ID == id);
                if (prop == null)
                    return;
                db.ShopProperties.DeleteOnSubmit(prop);
                db.SubmitChanges();
            }
        }

        public void UpdateProperty(Guid id, string key, string value)
        {
            using (var db = new BL.ShopDataContext())
            {
                var prop = db.ShopProperties.FirstOrDefault(p => p.ID == id);
                if (prop == null)
                    return;
                prop.Key = key;
                prop.Value = value;
                db.SubmitChanges();
            }
        }
        public BL.ShopProperties GetPropertyByID(Guid id)
        {
            return new BL.ShopDataContext().ShopProperties.FirstOrDefault(p => p.ID == id);
        }

        public IQueryable<BL.ShopProperties> GetAllProperties()
        {
            return new BL.ShopDataContext().ShopProperties;
        }
    }
}
