using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Text.RegularExpressions;

namespace iStore.Orders
{
    public partial class FormOrder : System.Web.UI.Page
    {

        iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (auth.CurrentUser == null)
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx");

            if (UserOrder == null)
                Response.Redirect("~/Orders/OrdersList.aspx");


            if (!IsPostBack)
            {
                countryDdl.DataSource = (new BL.Modules.Countries.Countries().GetAllCountries());
                countryDdl.DataTextField = "Name";
                countryDdl.DataValueField = "ID";
                countryDdl.DataBind();

                address1Txt.Text = UserOrder.Address1;
                address2Txt.Text = UserOrder.Address2;
                FirstNameTxt.Text = UserOrder.FirstName;
                LastNameTxt.Text = UserOrder.LastName;
                cityTxt.Text = UserOrder.City;
                provinceTxt.Text = UserOrder.StateProvinceRegion;
                zipTxt.Text = UserOrder.zipcode;
                countryDdl.SelectedValue = UserOrder.CountryID.ToString();
                phoneTxt.Text = UserOrder.PhoneNumber;
                emailTxt.Text = UserOrder.email;
            }

            IsDataAccepted = false;
        }

        BL.Order _UserOrder;
        protected BL.Order UserOrder
        {
            get
            {
                if (_UserOrder == null)
                    _UserOrder = obl.GetUserOrderedProducts(auth.CurrentUser.UserID).FirstOrDefault(o => o.IsActive);

                return _UserOrder;
            }
        }
        static string emailPatern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        static Regex emailValidator = new Regex(emailPatern);
        private bool CheckData(string firstName, string lastName, string address1, string address2, string city, string province, string zip, string phone, string email, int countryID)
        {

            errMsg.Visible = false;
            errMsg.InnerHtml = "Please fill:<br/>";

            if (string.IsNullOrWhiteSpace(firstName))
            {
                errMsg.InnerHtml += "first name<br/>";
                errMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                errMsg.InnerHtml += "first name<br/>";
                errMsg.Visible = true;
            }

            if (string.IsNullOrWhiteSpace(address1))
            {
                errMsg.InnerHtml += "address 1<br/>";
                errMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                errMsg.InnerHtml += "City<br/>";
                errMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(province))
            {
                errMsg.InnerHtml += "State/Province/Region<br/>";
                errMsg.Visible = true;
            }
            int temp;
            if (!int.TryParse(zip, out temp))
            {
                errMsg.InnerHtml += "Zip code<br/>";
                errMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                errMsg.InnerHtml += "Phone number<br/>";
                errMsg.Visible = true;
            }
            if (new BL.Modules.Countries.Countries().GetCountryByID(countryID) == null)
            {
                errMsg.InnerHtml += "Select correct country<br/>";
                errMsg.Visible = true;
            }

            if (!emailValidator.IsMatch(email) && !string.IsNullOrWhiteSpace(email))
            {
                errMsg.InnerHtml += "fill out valid e-mail<br/>";
                errMsg.Visible = true;
            }
            if (errMsg.Visible)
                return !errMsg.Visible;

<<<<<<< HEAD

=======
            
>>>>>>> origin/master
            if (address1.Length > 150)
            {
                errMsg.InnerHtml += "Address 1 max length 150<br/>";
                errMsg.Visible = true;
            }

            if (address2.Length > 150)
            {
                errMsg.InnerHtml += "Address 2 max length 150<br/>";
                errMsg.Visible = true;
            }

            if (city.Length > 50)
            {
                errMsg.InnerHtml += "City max length 50<br/>";
                errMsg.Visible = true;
            }

            if (province.Length > 50)
            {
                errMsg.InnerHtml += "State/Province/Region max length 150<br/>";
                errMsg.Visible = true;
            }

            if (zip.Length > 50)
            {
                errMsg.InnerHtml += "Zip code max length 50<br/>";
                errMsg.Visible = true;
            }

            if (phone.Length > 50)
            {
                errMsg.InnerHtml += "Phone max length 50<br/>";
                errMsg.Visible = true;
            }

            if (email.Length > 150)
            {
                errMsg.InnerHtml += "Email max length 50<br/>";
                errMsg.Visible = true;
            }

            if (lastName.Length > 50)
            {
                errMsg.InnerHtml += "Last name max length 50<br/>";
                errMsg.Visible = true;
            }

            if (firstName.Length > 50)
            {
                errMsg.InnerHtml += "First name max length 50<br/>";
                errMsg.Visible = true;
            }

            return !errMsg.Visible;
        }

        protected void Pay(object obj, EventArgs args)
        {

            var firstName = HttpUtility.HtmlEncode(FirstNameTxt.Text.Trim());
            var lastName = HttpUtility.HtmlEncode(LastNameTxt.Text.Trim());
            var address1 = HttpUtility.HtmlEncode(address1Txt.Text.Trim());
            var address2 = HttpUtility.HtmlEncode(address2Txt.Text.Trim());
            var city = HttpUtility.HtmlEncode(cityTxt.Text.Trim());
            var email = emailTxt.Text.Trim();
            var province = HttpUtility.HtmlEncode(provinceTxt.Text.Trim());
            var zip = HttpUtility.HtmlEncode(zipTxt.Text.Trim());
            var phone = HttpUtility.HtmlEncode(phoneTxt.Text.Trim());
            var strCountryID = countryDdl.SelectedValue;
            int countryID;
            if (!int.TryParse(strCountryID, out countryID))
                return;

            if (!CheckData(firstName, lastName, address1, address2, city, province, zip, phone, email, countryID))
                return;

            email = string.IsNullOrWhiteSpace(email) ? auth.CurrentUser.Email : HttpUtility.HtmlEncode(email);

            obl.UpdateOrderUserData(UserOrder.OrderID, firstName, lastName, address1, address2, city, province, zip, phone, email, countryID);
            _UserOrder = null;

            IsDataAccepted = true;
        }

        public bool IsDataAccepted { get; set; }
    }
}