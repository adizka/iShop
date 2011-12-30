using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BL
{
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
        NotPaid  = 1,
        Paid = 2
    }
    public enum DeliveryTypes
    {
        NonameType = 1
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
        public const string ProductDescription = "ProductDescription";
    }
}