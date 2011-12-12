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
                    category.Sort = db.Categories.Count(c => c.ParentID == parentId);
                    db.Categories.InsertOnSubmit(category);
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        public void UpdateCategory(Guid categoryId, string name, Guid? parentId)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                BL.Category category = db.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault();
                if (category != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        category.Name = name;
                        category.ParentID = parentId;
                        db.SubmitChanges();
                        ts.Complete();
                        db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                    }
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

        public void UpdateAllCategories(List<BL.Category> categories)
        {
            using (var db = new ShopDataContext())
            {

                using (var ts = new TransactionScope())
                {
                    foreach (var item in categories)
                    {
                        var categ = db.Categories.First(c => c.CategoryID == item.CategoryID);
                        categ.Sort = item.Sort;
                    }
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
            return db.Categories.Where(c => c.ParentID == parentId);
        }

        public IQueryable<BL.Category> GetAllRootCatgories()
        {
            ShopDataContext db = new ShopDataContext();
            return db.Categories.Where(c => c.ParentID == null);
        }

        public bool NameInBD(string name)
        {
            return GetAllCategories().Where(c => c.Name == name).Any();

        }

        public Category GetCategoryById(Guid? parentId)
        {
            ShopDataContext db = new ShopDataContext();
            if (parentId.HasValue)
            {
                Guid id = parentId.Value;
                return db.Categories.Where(c => c.CategoryID == id).FirstOrDefault();
            }
            return null;
        }

        public bool CategoryNameInParentList(string name, string sparentId)
        {
            Guid? parentId;
            if (string.IsNullOrEmpty(sparentId))
            {
                parentId = null;
            }
            else
            {
                if (sparentId == "parent")
                {
                    parentId = null;
                }
                else
                {
                    parentId = new Guid(sparentId);
                }
                
            }
            IQueryable<BL.Category> currentCategories = GetCategoriesByParentId(parentId);
            if (currentCategories == null) return false;
            if (currentCategories.Any(c => c.Name == name))
            {
                return true;
            }
            return false;
        }
    }
}
