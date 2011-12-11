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
    public partial class Product : IEquatable<BL.Product>
    {
        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(Product other)
        {
            return this.ProductID == other.ProductID;
        }
    }
}
