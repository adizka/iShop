using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;

namespace BL
{

    public class ProductInfo
    {
        public string name;
        public int quantity;
    }
    partial class ProductType
    {
        enum Type
        {
            Real = 1,
            Web = 2
        }
    }

    public enum OrderStatus
    {
        NotPaid = 1,
        Paid = 2
    }
    public enum DeliveryTypes
    {
        Delivered = 1,
        NotDelivered = 2
    }
    public enum PaymentTypes
    {
        PayPal = 1
    }

    public class ProductCounter
    {
        public Guid ID;
        public int Count;
    }

    public class ProductDataComparer : IEqualityComparer<BL.ProductData>
    {
        public bool Equals(BL.ProductData x, BL.ProductData y)
        {
            return (x.ProductID == y.ProductID);
        }

        public int GetHashCode(BL.ProductData obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }

    public class ProductComparer : IEqualityComparer<BL.Product>
    {
        public bool Equals(BL.Product x, BL.Product y)
        {
            return (x.ProductID == y.ProductID);
        }

        public int GetHashCode(BL.Product obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }

    public class ProductsRefCategoryComparer : IEqualityComparer<ProductsRefCategory>
    {
        public bool Equals(BL.ProductsRefCategory x, BL.ProductsRefCategory y)
        {
            return (x.ProductID == y.ProductID);
        }

        public int GetHashCode(ProductsRefCategory obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }

    public class ProductPropertyConstants
    {
        public const string ProductPhotoPreview = "ProductPhotoPreview";
        public const string ProductPhotoOriginal = "ProductPhotoOriginal";
        public const string ProductPhotoOriginal2 = "ProductPhotoOriginal2";
        public const string ProductPhotoOriginal3 = "ProductPhotoOriginal3";
        public const string ProductDescription = "ProductDescription";
    }

    public class SiteProperties
    {
        public static string SiteUrl = "http://localhost:36138/";
        public static string ProjectName = "Marvel Worldwide";
        public static string SiteAdminUrl = "http://localhost:36138/Admin/";

        public static string PreUrlProductPreviewImage = SiteUrl + "Content/Products/Preview/";
        public static string PreUrlProductOriginalImage = SiteUrl + "Content/Products/Original/";

        public static string FileUploadProductPhotoOriginal = HttpContext.Current.Request.MapPath("~/") + @"Content\Products\Original\";
        public static string FileUploadProductPhotoPreview = HttpContext.Current.Request.MapPath("~/") + @"Content\Products\Preview\";

        public static string SiteName = "marvelworldwide.com";
    }
}
