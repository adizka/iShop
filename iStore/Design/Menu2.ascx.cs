using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Design
{
    public partial class Menu2 : System.Web.UI.UserControl
    {
        public BL.Modules.Categories.Categories Cbl = new BL.Modules.Categories.Categories();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<BL.Category> ParentCategory
        {
            get
            {
                return Cbl.GetAllRootCatgories();
            }
        }

        public string FirstElementId
        {
            get
            {
                BL.Category category = ParentCategory.FirstOrDefault();
                if (category != null)
                {
                    return category.CategoryID.ToString();
                }
                return string.Empty;
            }

        }

        public IQueryable<BL.Category> GetCategoryByParent(Guid id)
        {
            return Cbl.GetCategoriesByParentId(id).OrderBy(c => c.Sort);
        }
    }
}