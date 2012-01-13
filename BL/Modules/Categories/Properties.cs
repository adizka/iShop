using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;



namespace BL.Modules.Categories
{
    public class Properties
    {
        public bool AddProperties(Guid categoryId, string name, string value, int sort)
        {

            bool saveProperty = false;
            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Category category = db.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault();
                if (categoryId != null)
                {
                    BL.CategoryProperty cp = new BL.CategoryProperty();
                    cp.CategoriesID = categoryId;
                    cp.PropertName = name;
                    cp.PropertyValue = value;
                    cp.Sort = (sort == -1) ? 0 : sort;
                    cp.CategoriesPropertyID = Guid.NewGuid();
                    db.CategoryProperties.InsertOnSubmit(cp);
                    db.SubmitChanges();
                    saveProperty = true;
                }
            }
            return saveProperty;
        }

        public bool UpdateProperties(Guid properyId, string name, string value)
        {
            bool updateProperty = false;

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.CategoryProperty cp = db.CategoryProperties.Where(c => c.CategoriesPropertyID == properyId).FirstOrDefault();
                if (cp != null)
                {
                    cp.PropertName = name;
                    cp.PropertyValue = value;
                    db.SubmitChanges();
                    updateProperty = true;
                }
            }
            return updateProperty;
        }

        public bool UpdateAllPropertiesByCategoryId(IQueryable<BL.CategoryProperty> properties, Guid categoryId)
        {
            bool updateProperties = false;

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Category category = db.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault();
                if (category != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        db.CategoryProperties.AttachAll(properties);
                        db.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, properties);
                        db.SubmitChanges();
                        ts.Complete();
                        updateProperties = true;
                    }
                }
            }
            return updateProperties;
        }

        public IQueryable<BL.CategoryProperty> GetAllPropertiesByCategoryId(Guid categoryId)
        {
            ShopDataContext db = new ShopDataContext();
            return db.CategoryProperties.Where(cp => cp.CategoriesID == categoryId).OrderByDescending(cp => cp.Sort);
        }

        public bool DeletePropertyById(Guid propertyId)
        {
            bool deletePropepry = false;

            using (ShopDataContext db = new ShopDataContext())
            {
                BL.CategoryProperty cp = db.CategoryProperties.Where(c => c.CategoriesPropertyID == propertyId).FirstOrDefault();
                if (cp != null)
                {
                    db.CategoryProperties.DeleteOnSubmit(cp);
                    db.SubmitChanges();
                    return true;
                }
            }
            return deletePropepry;
        }

        public BL.CategoryProperty GetCategoryById(Guid propertyId)
        {
            ShopDataContext db = new ShopDataContext();
            return db.CategoryProperties.Where(cp => cp.CategoriesPropertyID == propertyId).FirstOrDefault();
        }
    }
}
