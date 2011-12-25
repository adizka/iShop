using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.ShopProperty
{
    public partial class PropertyEdit : System.Web.UI.Page
    {
        BL.Modules.ShopProperties.ShopProperties shbl = new BL.Modules.ShopProperties.ShopProperties();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PropertyID.HasValue)
            {
                var prop = shbl.GetPropertyByID(PropertyID.Value);
                if (prop != null)
                {
                    keyTxt.Text = prop.Key;
                    valTxt.Text = prop.Value;
                }
            }
        }

        Guid? PropertyID
        {
            get
            { 
                Guid pid;
                if (Guid.TryParse(Request.QueryString["pid"], out pid))
                    return pid;
                else
                    return null;
            }
        }

        protected void Save(object obj, EventArgs args)
        {
            keyTxt.Text = keyTxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyTxt.Text))
            {
                ErrorMsg.Visible = true;
                return;
            }

            if (PropertyID.HasValue && shbl.GetPropertyByID(PropertyID.Value) != null)
                shbl.UpdateProperty(PropertyID.Value, keyTxt.Text, valTxt.Text);
            else
                Response.Redirect(iStore.Site.SiteAdminUrl + "ShopProperties/PropertyEdit.aspx?pid=" + shbl.AddProperty(keyTxt.Text, valTxt.Text).ID.ToString());
        }
    }
}
