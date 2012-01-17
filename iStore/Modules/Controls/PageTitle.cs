using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iStore.Modules.Controls
{
    public static class PageTitle
    {

        public static string Get(string title)
        {
            return title + " | " + iStore.Site.ProjectName;
        }
    }
}