﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.IO;
using System.Globalization;
namespace iStore.Orders
{
    /// <summary>
    /// Summary description for PayPalIPN
    /// </summary>
    public class PayPalIPN : IHttpHandler
    {
        BL.Modules.Orders.Orders obl = new BL.Modules.Orders.Orders();
        public void ProcessRequest(HttpContext context)
        {


            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/out.txt"), HttpContext.Current.Request.Url.AbsoluteUri);


            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            //string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            if (strResponse == "VERIFIED")
            {
                if (HttpContext.Current.Request.QueryString["receiver_email"] == WebConfigurationManager.AppSettings["Login"])
                {
                    obl.TryFormOrderIPN(BL.PaymentTypes.PayPal, TransactionID, OrderID, payment_gross, payment_date, string.Empty);
                }
            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
            }
            else
            {
                //log response/ipn data for manual investigation
            }
        }
        string TransactionID
        {
            get
            {
                return HttpContext.Current.Request.QueryString["txn_id"];
            }
        }

        decimal payment_gross
        {
            get
            {
                return decimal.Parse(HttpContext.Current.Request.QueryString["payment_gross"]);
            }
        }

        Guid OrderID
        {
            get
            {
                return new Guid(HttpContext.Current.Request.QueryString["custom"]);
            }
        }

        static string[] dateFormats = { "HH:mm:ss MMM dd, yyyy PST", "HH:mm:ss MMM. dd, yyyy PST", "HH:mm:ss MMM dd, yyyy PDT", "HH:mm:ss MMM. dd, yyyy PDT" };
        static CultureInfo cultInfo = new CultureInfo("en-US");
        public DateTime payment_date
        {
            get
            {
                DateTime outputDateTime;
                string payPalDateTime = HttpContext.Current.Request.QueryString["payment_date"];
                DateTime.TryParseExact(payPalDateTime, dateFormats, cultInfo, DateTimeStyles.None, out outputDateTime);
                return outputDateTime;
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}