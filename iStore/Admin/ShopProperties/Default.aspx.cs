using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.ShopProperties
{
    public partial class Default : System.Web.UI.Page
    {

        BL.Modules.ShopProperties.ShopProperties shbl = new BL.Modules.ShopProperties.ShopProperties();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        List<BL.ShopProperties> _pageProperties;
        protected List<BL.ShopProperties> PageProperties
        {
            get
            {
                if (_pageProperties == null)
                    _pageProperties = shbl.GetAllProperties().ToList();

                return _pageProperties;
            }
 
        }

    }
}