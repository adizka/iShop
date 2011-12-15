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
            errorText.Visible = true;
            errorText.InnerHtml = string.Empty;
        }
        
        public string Errors { get; set; }

        public void SetErrors()
        {
            if (Errors != null)
            {
                errorText.InnerHtml += "<br /><span class='Errors'>" + Errors + "</span><br />";
            }
        }
        public void SetErrors(string err)
        {
            errorText.InnerHtml += "<br /><span class='Errors'>" + err + "</span><br />";
            errorText.Visible = true;
            this.Visible = true;
        }
        public void ClearErrors()
        {
            errorText.InnerHtml = string.Empty;
            errorText.Visible = false;
        }
    }
}