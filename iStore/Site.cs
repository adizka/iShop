using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iStore
{
    public class Site
    {
        public static string SiteUrl = "http://test.wchsb.com/";
        public static string ProjectName = "Marvel Worldwide";
        public static string SiteAdminUrl = "http://test.wchsb.com/Admin/";

        public static string PreUrlProductPreviewImage = SiteUrl + "Content/Products/Preview/";
        public static string PreUrlProductOriginalImage = SiteUrl + "Content/Products/Original/";

        public static string FileUploadProductPhotoOriginal = HttpContext.Current.Request.MapPath("~/") + @"Content\Products\Original\";
        public static string FileUploadProductPhotoPreview = HttpContext.Current.Request.MapPath("~/") + @"Content\Products\Preview\";
    }
}