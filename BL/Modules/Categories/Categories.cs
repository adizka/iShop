using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BL.Modules.Categories
{
    public class Categories
    {
        public enum DeleteErrors { HasChilds = 1, HasProducts = 2, Invalid = 3, Success = 4 }

        public void AddCategory(string name, Guid? parentId)
        {
            using (var db = new ShopDataContext())
            {
                BL.Category category = new BL.Category();
                using (var ts = new TransactionScope())
                {
                    category.Name = name;
                    category.CreateDate = DateTime.Now;
                    category.ParentID = parentId;
                    category.CategoryID = Guid.NewGuid();
                    db.Categories.InsertOnSubmit(category);
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        public void UpdateCategory(Guid categoryId, string name, Guid? parentId)
        {
            using (var db = new ShopDataContext())
            {
                BL.Category category = GetCategoryById(categoryId);
                if (category != null)
                {
                    category.Name = name;
                    category.ParentID = parentId;
                    db.SubmitChanges();
                }
            }
        }

        public BL.Category GetCategoryById(Guid categoryId)
        {
            ShopDataContext db = new ShopDataContext();
            return db.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault();
        }

        public void UpdateCategoryName(Guid categoryId, string name)
        {
            using (var db = new ShopDataContext())
            {
                BL.Category category = db.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault();
                if (category != null)
                {
                    category.Name = name;
                    db.SubmitChanges();
                }
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
            }
        }

        public void UpdateAllCategories(IQueryable<BL.Category> categories)
        {
            using (var db = new ShopDataContext())
            {
                
                using (var ts = new TransactionScope())
                {
                    db.Categories.AttachAll(categories);
                    db.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, categories);
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        public IQueryable<BL.Category> GetAllCategories()
        {
            ShopDataContext db = new ShopDataContext();
            return db.Categories.OrderByDescending(c => c.Name);
        }

        public int DeleteCategoryByIdWithErrorMessage(Guid id)
        {
            if (CategoryHasChild(id))
            {
                return (int)DeleteErrors.HasChilds;            
            }
            if (CategoryHasProducts(id))
            {
                return (int)DeleteErrors.HasProducts;
            }
            
            ShopDataContext db = new ShopDataContext();
            BL.Category category = db.Categories.Where(c => c.CategoryID == id).FirstOrDefault();
            if (category == null)
            {
                return (int)DeleteErrors.Invalid;
            }

            db.Categories.DeleteOnSubmit(category);
            db.SubmitChanges();
            return (int)DeleteErrors.Success;
            
        }

        private static bool CategoryHasProducts(Guid id)
        {
            ShopDataContext db = new ShopDataContext();
            return db.ProductsRefCategories.Where(c => c.CategoryID == id).Any();
        }

        public static bool CategoryHasChild(Guid categoryId)
        { 
            ShopDataContext db = new ShopDataContext();
            return db.Categories.Where(c => c.ParentID == categoryId).Any();
        }

        public IQueryable<BL.Category> GetCategoriesByParentId(Guid? parentId)
        { 
            ShopDataContext db = new ShopDataContext();
            return db.Categories.Where(c => c.ParentID == parentId).OrderBy(c => c.Sort);
        }

        public bool NameInBD(string name)
        {
            return GetAllCategories().Where(c => c.Name == name).Any();
                
        }
    }
}
