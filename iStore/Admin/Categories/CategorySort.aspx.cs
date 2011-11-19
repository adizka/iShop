using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace iStore.Admin.Categories
{
    public partial class CategorySort : System.Web.UI.Page
    {
        BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }



        IQueryable<BL.Category> _SiblingCategories;

        public IQueryable<BL.Category> SiblingCategories
        {
            get
            {
                if (_SiblingCategories == null)
                    _SiblingCategories = cbl.GetCategoriesByParentId(ParentID);

                return _SiblingCategories.OrderBy(c=>c.Sort);
            }
        }

        Guid? ParentID
        {
            get
            {
                return new Guid(Request.QueryString["id"]);
            }
        }

        protected void SaveCategoriesRate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hd.Value))
                return;
            var ids = hd.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(id => new Guid(id));
            int index = 0;
            foreach (var item in ids)
            {
                SiblingCategories.First(c => c.CategoryID == item).Sort = index;
                index++;
            }
            cbl.UpdateAllCategories(SiblingCategories.ToList());
        }

    }
}