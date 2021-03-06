﻿using System;
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

        public void UpdateAllCategories(List<Guid> categories)
        {
            using (var db = new ShopDataContext())
            {

                using (var ts = new TransactionScope())
                {
                    var index = 0;
                    foreach (var item in categories)
                    {
                        var categ = db.Categories.First(c => c.CategoryID == item);
                        categ.Sort = index;
                        index++;
                    }
                    db.SubmitChanges();
                    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                    ts.Complete();
                }
            }
        }

        public IQueryable<BL.Category> GetAllCategories()
        {
            ShopDataContext db = new ShopDataContext();
            return db.Categories.OrderByDescending(c => c.Name);
        }

        public IQueryable<BL.Category> GetCategoriesWithotChild(Guid? carId)
        {
            IQueryable<BL.Category> allCategories = GetAllCategories();
            if (carId == null) return allCategories;
            Guid id = carId.Value;
            IQueryable<BL.Category> childs = GetCategoriesByParentId(id);
            List<BL.Category> cats = new List<Category>();
            foreach (var item in allCategories)
            {
                BL.Category tempCat = childs.Where(c => c.CategoryID == item.CategoryID).FirstOrDefault();
                if (tempCat == null && item.CategoryID != id)
                {
                    cats.Add(item);
                }
            }
            return cats.AsQueryable();
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
            return db.Categories.Where(c => c.ParentID == null).OrderBy(c => c.Sort);
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
        public List<Guid> GetAllNestedCategories(Guid categoryID)
        {
            List<Guid> _childCategories = new List<Guid>();
            _childCategories.Add(categoryID);

            for (int i = 0; i < _childCategories.Count; i++)
            {
                _childCategories.AddRange(GetCategoriesByParentId(_childCategories[i]).OrderBy(s => s.Sort).Select(c => c.CategoryID));
                _childCategories = _childCategories.Distinct().ToList();
            }
         
            return _childCategories;
        }
    }
}
