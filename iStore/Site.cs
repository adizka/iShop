﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iStore
{
    public class Site
    {
        public static string SiteUrl = "http://localhost:36138/";
        public static string ProjectName = "Marvel Worldwide";
        public static string SiteAdminUrl = "http://localhost:36138/Admin/";

        public static string PreUrlProductPreviewImage = SiteUrl + "Content/Products/Preview/";
        public static string PreUrlProductOriginalImage = SiteUrl + "Content/Products/Original/";

        public static string FileUploadProductPhotoOriginal = HttpContext.Current.Request.MapPath("~/") + @"Content\Products\Original\";
        public static string FileUploadProductPhotoPreview = HttpContext.Current.Request.MapPath("~/") + @"Content\Products\Preview\";
    }
}