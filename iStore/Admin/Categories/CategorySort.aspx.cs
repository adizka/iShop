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
            rl.DataSource = SiblingCategories;
            rl.DataBind();

        }

        protected void lstReorder_Reorder(object sender, ReorderListItemReorderEventArgs e)
        {
            var lst = SiblingCategories.ToList();
            var moved = lst[e.OldIndex];
            lst.RemoveAt(e.OldIndex);
            lst.Insert(e.NewIndex, moved);


            this.rl.DataSource = lst;
            this.rl.DataBind();
        }


        IQueryable<BL.Category> _SiblingCategories;

        public IQueryable<BL.Category> SiblingCategories
        {
            get
            {
                if (_SiblingCategories == null)
                    _SiblingCategories = cbl.GetCategoriesByParentId(ParentID);

                return _SiblingCategories;
            }
        }

        Guid? ParentID
        {
            get
            {
                return new Guid(Request.QueryString["id"]);
            }
        }

    }
}