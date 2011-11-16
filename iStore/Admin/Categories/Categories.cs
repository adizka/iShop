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
            this.level = parent.level + 1;
            AllCategories = Root.AllCategories;
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

