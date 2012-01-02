using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Modules.Controls.Pager
{
    public partial class Pager : System.Web.UI.UserControl
    {
        public enum EntityType {  Categories, Products, Orders, Users,  Stock }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EntitiesPerPage = 5;
            NavigationNumbsCount = 5;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        string _NavigateUrl;
        protected string NavigateUrl
        {
            get
            {
                if (_NavigateUrl == null)
                    _NavigateUrl = Request.Url.AbsolutePath;
                return _NavigateUrl;

            }
        }

        private string _params;
        protected string Params
        {
            get
            {
                if(_params !=null)
                    return _params;

                foreach (var key in Request.QueryString.AllKeys)
                {
                    if(key == "p")
                        continue;
                    _params += "&" + key + "=" + Request.QueryString[key];
                }
                return _params;
            }
        }

        public int EntityCount { get; set; }

        public int PageIndex
        {
            get
            {
                try
                {
                    int index = int.Parse(Request.QueryString["p"]);
                    if (index < 0)
                        return 0;
                    else if (EntityCount / EntitiesPerPage < index)
                        return EntityCount / EntitiesPerPage - ((EntityCount % EntitiesPerPage != 0) ? 0 : 1);
                    return index;
                }
                catch { }
                return 0;
            }
        }

        public int EntitiesPerPage { get; set; }

        public int NavigationNumbsCount { get; set; }

        protected int FirstIdnex
        {
            get
            {

                if (PageIndex <= (NavigationNumbsCount - 1) / 2)
                    return 0;
                else if (PageIndex >= PagesCount - (NavigationNumbsCount - 1) / 2 - 1)
                    return Math.Max(0, PagesCount - NavigationNumbsCount);
                else
                    return PageIndex - (NavigationNumbsCount - 1) / 2;
            }
        }

        protected int LastIdnex
        {
            get
            {
                return ((FirstIdnex + NavigationNumbsCount < PagesCount)
                    ? FirstIdnex + NavigationNumbsCount
                    : PagesCount);
            }
        }
      
        protected int PagesCount
        {
            get
            {
                return EntityCount / EntitiesPerPage - ((EntityCount % EntitiesPerPage != 0) ? 0 : 1) + 1;
            }
        }

        //pager.AddInsParams = "cid=" + Request.QueryString["cid"];
    }
}