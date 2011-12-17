using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Design
{
    public partial class Menu : System.Web.UI.UserControl
    {
        public BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<BL.Category> parentCategory
        {
            get
            {
                return cbl.GetAllRootCatgories();
            }
        }

        public string  FirstElementId
        {
            get
            {
                BL.Category category = parentCategory.FirstOrDefault();
                if (category != null)
                {
                    return category.CategoryID.ToString();
                }
                return string.Empty;
            }
        
        }

        public IQueryable<BL.Category> GetCategoryByParent(Guid id)
        {
            return cbl.GetCategoriesByParentId(id);
        }
    }
}