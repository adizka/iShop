using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Statistics
{
    public partial class Userslist : System.Web.UI.Page
    {
        public BL.Modules.Categories.Categories cbl = new BL.Modules.Categories.Categories();
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Users.Users ubl = new BL.Modules.Users.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            pager.EntityCount = ubl.GetAllUsers().Count();
        }

        List<BL.User> _PageUsers;
        protected List<BL.User> PageUsers
        {
            get
            {
                if (_PageUsers == null)
                    _PageUsers = ubl.GetAllUsers().OrderBy(u => u.Login).Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage).OrderBy(u => u.Login).ToList();
             
                return _PageUsers;
            }
        }
    }
}