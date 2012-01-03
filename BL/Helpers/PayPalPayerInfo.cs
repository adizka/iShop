using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;

namespace BL.Helpers
{
    public class ItemInfo
    {
        public string name;
        public int number;
        public double handling;
        public double shipping;
        public int quantity;
        public double gross;
    }

    public class Items
    {
        private PayPalPayerInfo ppinf;
        public Items(PayPalPayerInfo inf)
        {
            this.ppinf = inf;
        }
        public ItemInfo this[long index]
        {
            get
            {
                var inf = new ItemInfo();
                double.TryParse(ppinf.GetPropertyByKey("mc_gross_" + (index + 1).ToString()), out inf.gross);
                double.TryParse(ppinf.GetPropertyByKey("mc_handling" + (index + 1).ToString()), out inf.handling);
                double.TryParse(ppinf.GetPropertyByKey("mc_shipping" + (index + 1).ToString()), out inf.shipping);
                int.TryParse(ppinf.GetPropertyByKey("item_number" + (index + 1).ToString()), out inf.number);
                int.TryParse(ppinf.GetPropertyByKey("quantity" + (index + 1).ToString()), out inf.quantity);
                inf.name = ppinf.GetPropertyByKey("item_name" + (index + 1).ToString());
                return inf;
            }
        }
    }
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

        public Items Items { get; set; }

        public bool IsSucced { get { return val.StartsWith("SUCCESS"); } }
        public decimal mc_gross { get { return decimal.Parse(GetPropertyByKey("mc_gross").Replace('.', ',')); } }
        public string protection_eligibility { get { return GetPropertyByKey("protection_eligibility"); } }
        public string receiver_email { get { return GetPropertyByKey("receiver_email"); } }
        public string address_status { get { return GetPropertyByKey("address_status"); } }
        public string tax { get { return GetPropertyByKey("tax"); } }
        public string payer_id { get { return GetPropertyByKey("payer_id"); } }
        public string address_street { get { return GetPropertyByKey("address_street"); } }

        static string[] dateFormats = { "HH:mm:ss MMM dd, yyyy PST", "HH:mm:ss MMM. dd, yyyy PST", "HH:mm:ss MMM dd, yyyy PDT", "HH:mm:ss MMM. dd, yyyy PDT" };
        static CultureInfo cultInfo = new CultureInfo("en-US");
        public DateTime payment_date
        {
            get
            {
                DateTime outputDateTime;
                string payPalDateTime = GetPropertyByKey("payment_date");
                DateTime.TryParseExact(payPalDateTime, dateFormats, cultInfo, DateTimeStyles.None, out outputDateTime);
                return outputDateTime;
            }
        }

        public string payment_status { get { return GetPropertyByKey("payment_status"); } }
        public string charset { get { return GetPropertyByKey("charset"); } }
        public string address_zip { get { return GetPropertyByKey("address_zip"); } }
        public string first_name { get { return GetPropertyByKey("first_name"); } }
        public string mc_fee { get { return GetPropertyByKey("mc_fee"); } }
        public string address_country_code { get { return GetPropertyByKey("address_country_code"); } }
        public string address_name { get { return GetPropertyByKey("address_name"); } }
        public string custom { get { return GetPropertyByKey("custom"); } }
        public string payer_status { get { return GetPropertyByKey("payer_status"); } }
        public string business { get { return GetPropertyByKey("business"); } }
        public string address_country { get { return GetPropertyByKey("address_country"); } }
        public string num_cart_items { get { return GetPropertyByKey("num_cart_items"); } }
        public string address_city { get { return GetPropertyByKey("address_city"); } }
        public string payer_email { get { return GetPropertyByKey("payer_email"); } }
        public string txn_id { get { return GetPropertyByKey("txn_id"); } }
        public string payment_type { get { return GetPropertyByKey("payment_type"); } }
        public string last_name { get { return GetPropertyByKey("last_name"); } }
        public string address_state { get { return GetPropertyByKey("address_state"); } }
        public string payment_fee { get { return GetPropertyByKey("payment_fee"); } }
        public string receiver_id { get { return GetPropertyByKey("receiver_id"); } }
        public string pending_reason { get { return GetPropertyByKey("pending_reason"); } }
        public string txn_type { get { return GetPropertyByKey("txn_type"); } }
        public string mc_currency { get { return GetPropertyByKey("mc_currency"); } }
        public string residence_country { get { return GetPropertyByKey("residence_country"); } }
        public string transaction_subject { get { return GetPropertyByKey("transaction_subject"); } }
        public string payment_gross { get { return GetPropertyByKey("payment_gross"); } }
    }
}