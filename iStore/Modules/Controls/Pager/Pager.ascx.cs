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
        public enum EntityType { Categories, Products, Orders, Users, Stock }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EntitiesPerPage = 10;
            NavigationNumbsCount = 3;
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
                if (_params != null)
                    return _params;

                foreach (var key in Request.QueryString.AllKeys)
                {
                    if (key == "p")
                        continue;
                    _params += "&" + key + "=" + Request.QueryString[key];
                }
                return _params;
            }
        }

        public int EntityCount { get; set; }

        object PageIndexObj;
        int _PageIndex;
        public int PageIndex
        {
            get
            {
                if (PageIndexObj == null)
                    try
                    {
                        _PageIndex = int.Parse(Request.QueryString["p"]);
                        if (_PageIndex < 0)
                            _PageIndex = 0;
                        else if (EntityCount / EntitiesPerPage < _PageIndex)
                            _PageIndex = EntityCount / EntitiesPerPage - ((EntityCount % EntitiesPerPage != 0) ? 0 : 1);
                    }
                    catch { _PageIndex = 0; }
                    finally
                    {
                        PageIndexObj = new object();
                    }
                return _PageIndex;
            }
        }

        public int EntitiesPerPage { get; set; }

        public int NavigationNumbsCount { get; set; }

        object FirstIdnexObj;
        int _FirstIdnex;
        protected int FirstIdnex
        {
            get
            {
                if (FirstIdnexObj == null)
                {
                    if (PageIndex <= (NavigationNumbsCount - 1) / 2)
                        _FirstIdnex = 0;
                    else if (PageIndex >= PagesCount - (NavigationNumbsCount - 1) / 2 - 1)
                        _FirstIdnex = Math.Max(0, PagesCount - NavigationNumbsCount);
                    else
                        _FirstIdnex = PageIndex - (NavigationNumbsCount - 1) / 2;
                    FirstIdnexObj = new object();
                }
                return _FirstIdnex;
            }
        }

        object LastIdnexObj;
        int _LastIdnex;
        protected int LastIdnex
        {
            get
            {
                if (LastIdnexObj == null)
                {
                    _LastIdnex = ((FirstIdnex + NavigationNumbsCount < PagesCount)
                        ? FirstIdnex + NavigationNumbsCount
                        : PagesCount);
                    LastIdnexObj = new object();
                }
                return _LastIdnex;
            }
        }

        object PagesCountObj;
        int _PagesCount;
        protected int PagesCount
        {
            get
            {
                if (PagesCountObj == null)
                {
                    _PagesCount = EntityCount / EntitiesPerPage - ((EntityCount % EntitiesPerPage != 0) ? 0 : 1) + 1;
                    PagesCountObj = new object();
                }
                return _PagesCount;
            }
        }
    }
}