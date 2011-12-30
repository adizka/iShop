using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Helpers
{
    public class PayPalPayerInfo
    {
        public PayPalPayerInfo(string strResponse)
        {
            this.val = strResponse;
        }

        string val;

        public string GetPropertyByKey(string key)
        {
            if (key == string.Empty)
                return string.Empty;

            key = "\n" + key + "=";
            var ind = val.IndexOf(key);
            if (ind == -1)
                return string.Empty;
            else
            {
                var to = val.IndexOf('\n', ind + key.Length);
                return val.Substring(ind + key.Length, to - (ind + key.Length));
            }
        }

        public bool IsSucced { get { return val.StartsWith("SUCCESS"); } }
        public string mc_gross { get { return GetPropertyByKey("mc_gross"); } }
        public string protection_eligibility { get { return GetPropertyByKey("protection_eligibility"); } }
        public string address_status { get { return GetPropertyByKey("address_status"); } }
        public string item_number1 { get { return GetPropertyByKey("item_number1"); } }
        public string payer_id { get { return GetPropertyByKey("payer_id"); } }
        public string tax { get { return GetPropertyByKey("tax"); } }
        public string address_street { get { return GetPropertyByKey("address_street"); } }
        public string payment_date { get { return GetPropertyByKey("payment_date"); } }
        public string payment_status { get { return GetPropertyByKey("payment_status"); } }
        public string charset { get { return GetPropertyByKey("charset"); } }
        public string address_zip { get { return GetPropertyByKey("address_zip"); } }
        public string mc_shipping { get { return GetPropertyByKey("mc_shipping"); } }
        public string mc_handling { get { return GetPropertyByKey("mc_handling"); } }
        public string first_name { get { return GetPropertyByKey("first_name"); } }
        public string mc_fee { get { return GetPropertyByKey("mc_fee"); } }
        public string address_country_code { get { return GetPropertyByKey("address_country_code"); } }
        public string address_name { get { return GetPropertyByKey("address_name"); } }
        public string custom { get { return GetPropertyByKey("custom"); } }
        public string payer_status { get { return GetPropertyByKey("payer_status"); } }
        public string business { get { return GetPropertyByKey("business"); } }
        public string address_country { get { return GetPropertyByKey("address_country"); } }
        public string num_cart_items { get { return GetPropertyByKey("num_cart_items"); } }
        public string mc_handling1 { get { return GetPropertyByKey("mc_handling1"); } }
        public string address_city { get { return GetPropertyByKey("address_city"); } }
        public string payer_email { get { return GetPropertyByKey("payer_email"); } }
        public string mc_shipping1 { get { return GetPropertyByKey("mc_shipping1"); } }
        public string txn_id { get { return GetPropertyByKey("txn_id"); } }
        public string payment_type { get { return GetPropertyByKey("payment_type"); } }
        public string last_name { get { return GetPropertyByKey("last_name"); } }
        public string address_state { get { return GetPropertyByKey("address_state"); } }
        public string item_name1 { get { return GetPropertyByKey("item_name1"); } }
        public string receiver_email { get { return GetPropertyByKey("receiver_email"); } }
        public string payment_fee { get { return GetPropertyByKey("payment_fee"); } }
        public string quantity1 { get { return GetPropertyByKey("quantity1"); } }
        public string receiver_id { get { return GetPropertyByKey("receiver_id"); } }
        public string pending_reason { get { return GetPropertyByKey("pending_reason"); } }
        public string txn_type { get { return GetPropertyByKey("txn_type"); } }
        public string mc_gross_1 { get { return GetPropertyByKey("mc_gross_1"); } }
        public string mc_currency { get { return GetPropertyByKey("mc_currency"); } }
        public string residence_country { get { return GetPropertyByKey("residence_country"); } }
        public string transaction_subject { get { return GetPropertyByKey("transaction_subject"); } }
        public string payment_gross { get { return GetPropertyByKey("payment_gross"); } }
    }
}