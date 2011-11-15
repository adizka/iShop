using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Modules.Controls.Validators
{
    public partial class ValidateErrors : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        public string Errors { get; set; }

        public void SetErrors()
        {
            if (Errors != null)
            {
                errorText.InnerHtml += "<br /><span class='Errors'>" + Errors + "</span><br />";
            }
        }

        public void ClearErrors()
        {
            errorText.InnerHtml = string.Empty;
        }
    }
}