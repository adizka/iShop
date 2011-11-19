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

        public string Name { get; set; }
        public Guid? ID { get; set; }
        public Categories Parent { get; set; }
        public Categories Root { get; set; }
        public int level;
        public int sort;
        public Categories()
        {
            this.Parent = null;
            this.Name = "Categories";
            this.ID = null;
            this.level = 0;
            AllCategories = cbl.GetAllCategories().ToList();
            this.level = 0;
            this.Root = this;
        }

        private Categories(BL.Category category, Categories parent)
        {

            this.Parent = parent;
            this.Root = parent.Root;
            this.Name = category.Name;
            this.ID = category.CategoryID;
            this.sort = category.Sort;
            this.level = parent.level + 1;
            AllCategories = Root.AllCategories;
        }

        public List<Categories> GetCategoriesTree()
        {
            List<Categories> result = new List<Categories>();
            var childs = this.ChildCategories.OrderBy(c => c.sort).ThenBy(cc => cc.Name);

            foreach (var item in childs)
            {
                result.Add(item);
                result.AddRange(GetCategoriesTree(item));
            }

            return result;
        }
        List<Categories> GetCategoriesTree(Categories node)
        {
            List<Categories> result = new List<Categories>();
            var childs = node.ChildCategories;

            foreach (var item in childs)
            {
                result.Add(item);
                result.AddRange(GetCategoriesTree(item));
            }

            return result;
        }
        List<BL.Category> AllCategories;
        List<Categories> _ChildCategories;

        public IQueryable<Categories> ChildCategories
        {
            get
            {
                if (_ChildCategories != null)
                    return _ChildCategories.AsQueryable();

                _ChildCategories = new List<Categories>();


                this._ChildCategories.AddRange(
                    this.AllCategories.Where(c => c.ParentID == this.ID).Select(ct =>
                        new Categories(ct, this)
                        )
                );

                return _ChildCategories.AsQueryable();
            }
        }
    }
}

