using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;

namespace iStore
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        static DateTime lastUpdatePeriod;
        static double _updateInterval;
        static double UpdateInterval
        {
            get 
            {
                if(lastUpdatePeriod.AddSeconds(60) < DateTime.Now)
                {
                    lastUpdatePeriod = DateTime.Now;
                    _updateInterval = double.Parse( ConfigurationManager.AppSettings["UpdateOrderPeriodMin"]);
                }
                return _updateInterval;
            }
        }

        static object locker = new object();
        static DateTime LastUpdateOrders;

        void Application_BeginRequest(object sender, EventArgs e)
        {
            if (LastUpdateOrders.AddMinutes(UpdateInterval) < DateTime.Now)
            {
                lock (locker)
                {
                    if (LastUpdateOrders.AddMinutes(UpdateInterval) < DateTime.Now)
                    {
                        LastUpdateOrders = DateTime.Now;
                        BL.Modules.Orders.Orders ord = new BL.Modules.Orders.Orders();
                        ord.UpadateOrdersCounts();
                    }
                }
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
