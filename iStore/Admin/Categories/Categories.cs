using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace iStore.Admin.Categories
{
    public class Categories
    {
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        
        public IQueryable<BL.Category> CategoriesHierarchy()
        {
            IQueryable<BL.Category> allCategories = cbl.GetAllCategories().OrderBy(c => c.Name);
            if (allCategories.Any())
            {
                //List<Guid?> Ids = new List<Guid?>();

                //foreach (var item in cbl.GetAllCategories())
                //{
                //    Ids.Add(item.ParentID);
                //}
                ////Удаляю повторяющиеся ПарентID
                //Ids.Distinct().ToList();
                
                //Заполняем дерево родителями.
                //IList<BL.Category> herarchy = allCategories.Where(c => c.ParentID == null).ToList();
                //IList<BL.Category> distinctParents = allCategories.Distinct().ToList();
                //foreach (var item in distinctParents)
                //{
                //    herarchy.Add(item);
                //}
                //herarchy = herarchy.Distinct().ToList().OrderBy(c => c.ParentID);


                
                //foreach (var item in Ids)
                //{
                //    IQueryable<BL.Category> currentChild = cbl.GetCategoriesByParentId(item.Value);
                //}
            }
            return allCategories;
        }
    }
}

