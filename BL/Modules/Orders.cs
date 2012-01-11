using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace BL.Modules.Orders
{
    public class Orders
    {
        public IQueryable<BL.Order> GetUserOrderedProducts(Guid userID)
        {
            var db = new ShopDataContext();
            return db.Orders.Where(o => o.UserID == userID);
        }

        public void AddToCart(List<ProductCounter> prodIDs, Guid userID)
        {

            using (var db = new ShopDataContext())
            {
                var user = db.Users.First(u => u.UserID == userID);

                var order = user.Orders.FirstOrDefault(o => o.IsActive);

                if (order == null)
                {
                    order = new Order()
                    {
                        DeliveryTypeID = (int)DeliveryTypes.NotDelivered,
                        IsActive = true,
                        IsPaid = false,
                        OrderID = Guid.NewGuid(),
                        OrderStatusID = (int)OrderStatus.NotPaid,
                        PaymentTypeID = (int)PaymentTypes.PayPal,
                        UserID = userID,
                        CreateDate = DateTime.Now,
                        DeliveryDate = DateTime.Now,
                        CountryID = 1
                    };
                    db.Orders.InsertOnSubmit(order);
                }

                foreach (var item in prodIDs)
                {
                    var ord = order.OrdersRefProducts.FirstOrDefault(r => r.ProductID == item.ID);
                    if (ord != null)
                    {
                        ord.Count = Math.Min(ord.Count + item.Count, ord.Product.Count);
                    }
                    else
                    {
                        var prod = db.Products.First(p => p.ProductID == item.ID);
                        if (prod == null)
                            continue;
                        ord = new OrdersRefProduct()
                        {
                            Count = Math.Min(item.Count, prod.Count),
                            ProductID = item.ID,
                            ID = Guid.NewGuid(),
                            CreateDate = DateTime.Now,
                            //ProductAndProperyRefID =????????????
                        };
                        order.OrdersRefProducts.Add(ord);
                    }
                }
                db.SubmitChanges();
            }
        }

        public void Remove(List<Guid> toDelete, Guid userID, Guid orderID)
        {
            using (var db = new ShopDataContext())
            {
                var user = db.Users.First(u => u.UserID == userID);
                var order = user.Orders.FirstOrDefault(o => o.OrderID == orderID && o.IsActive);

                if (order == null)
                    return;
                db.OrdersRefProducts.DeleteAllOnSubmit(order.OrdersRefProducts.Where(r => toDelete.Contains(r.ID)));

                db.SubmitChanges();
            }
        }


        public void UpdateCounts(List<ProductCounter> newCounts, Guid userID, Guid orderID)
        {
            using (var db = new ShopDataContext())
            {
                var user = db.Users.First(u => u.UserID == userID);
                var order = user.Orders.FirstOrDefault(o => o.OrderID == orderID && o.IsActive);

                if (order == null)
                    return;

                var refs = order.OrdersRefProducts.Where(or => newCounts.Any(c => c.ID == or.ID));

                foreach (var item in refs)
                {
                    item.Count = (Math.Min(newCounts.First(c => c.ID == item.ID).Count, item.Product.Count));
                }

                db.SubmitChanges();
            }
        }

        public bool TryFormOrder(PaymentTypes paymentType, Helpers.PayPalPayerInfo paymentInfo)
        {
            using (var db = new ShopDataContext())
            {
                if (db.Orders.Any(o => o.TransactionID == paymentInfo.txn_id))
                    return true;
                
                var order = db.Orders.FirstOrDefault(o => o.OrderID == paymentInfo.OrderID && !o.IsPaid);

                if (order == null)
                    return false;

                var totalSum = order.OrdersRefProducts.Sum(r => (r.Product.Price + r.Product.Tax + r.Product.Shipping) * r.Count);
                if (totalSum != paymentInfo.mc_gross)
                    return false;

                foreach (var item in order.OrdersRefProducts)
                {
                    item.Product.Count -= item.Count;
                    item.Product.IsVisible = item.Product.Count > 0;
                }

                order.IsActive = false;
                order.IsPaid = true;
                order.PaymentTypeID = (int)paymentType;
                order.OrderStatusID = (int)BL.OrderStatus.Paid;
                order.TotalSum = Convert.ToDecimal(order.OrdersRefProducts.Sum(r => r.Product.Price * r.Count));
                order.CreateDate = paymentInfo.payment_date;
                order.TransactionID = paymentInfo.txn_id;
                order.DeliveryDate = DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["DeliveryTime"]));
                order.DeliveryTypeID = (int)DeliveryTypes.NotDelivered;
                order.SpecialNote = paymentInfo.SpecialNote;
                db.SubmitChanges();
                BL.Modules.Mail.Mail.OrderAccepted(order.User);
            }
            return true;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return new ShopDataContext().Orders;
        }

        public Order GetOrderById(Guid orderID)
        {
            return new ShopDataContext().Orders.FirstOrDefault(o => o.OrderID == orderID);
        }

        public void ClearCart(Guid userID, Guid orderID)
        {
            using (var db = new ShopDataContext())
            {
                var user = db.Users.First(u => u.UserID == userID);
                var order = user.Orders.FirstOrDefault(o => o.OrderID == orderID && o.IsActive);

                if (order == null)
                    return;
                db.OrdersRefProducts.DeleteAllOnSubmit(order.OrdersRefProducts);
                db.SubmitChanges();
            }
        }

        public Order CreateOrder(Guid userID)
        {
            var db = new ShopDataContext();
            var user = db.Users.First(u => u.UserID == userID);
            var order = user.Orders.FirstOrDefault(o => o.IsActive);

            if (order != null)
                return order;

            order = new Order()
            {
                DeliveryTypeID = (int)DeliveryTypes.NotDelivered,
                IsActive = true,
                IsPaid = false,
                OrderID = Guid.NewGuid(),
                OrderStatusID = (int)OrderStatus.NotPaid,
                PaymentTypeID = (int)PaymentTypes.PayPal,
                UserID = userID,
                CreateDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                CountryID = db.Countries.First().ID
            };
            user.Orders.Add(order);
            db.SubmitChanges();

            return order;
        }

        public void UpdateOrder(Guid orderID, int statusID, int deliveryID)
        {
            using (var db = new ShopDataContext())
            {
                var ord = db.Orders.First(o => o.OrderID == orderID);
                ord.IsPaid = (statusID == (int)BL.OrderStatus.Paid);
                ord.IsActive = ord.IsPaid;
                ord.DeliveryDate = DateTime.Now;
                ord.DeliveryTypeID = deliveryID;
                ord.OrderStatusID = statusID;
                db.SubmitChanges();
            }
        }

        public void UpdateCounts(Guid orderID)
        {
            using (var db = new ShopDataContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.IsActive && o.OrderID == orderID);

                if (order == null)
                    return;

                foreach (var item in order.OrdersRefProducts)
                {
                    item.Count = (Math.Min(item.Count, item.Product.Count));
                }

                db.SubmitChanges();
            }
        }

        public void UpdateOrderUserData(Guid orderID, string firstName, string lastName, string address1, string address2, string city, string province, string zip, string phone, string email, int countryID)
        {
            using (var db = new ShopDataContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.OrderID == orderID);

                if (order == null)
                    return;

                order.CountryID = countryID;
                order.FirstName = firstName;
                order.LastName = lastName;
                order.Address1 = address1;
                order.Address2 = address2;
                order.City = city;
                order.PhoneNumber = phone;
                order.StateProvinceRegion = province;
                order.zipcode = zip;
                order.email = email;

                db.SubmitChanges();
            }
        }
    }
}
