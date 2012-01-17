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
                        CountryID = db.Countries.First().ID
                    };
                    db.Orders.InsertOnSubmit(order);
                }

                foreach (var item in prodIDs)
                {
                    var ord = order.OrdersRefProducts.FirstOrDefault(r => r.ProductID == item.ID);
                    int oldCount = 0;
                    BL.Product prod = null;
                    if (ord != null)
                    {
                        oldCount = ord.Count;
                        //ord.Count = Math.Min(item.Count, ord.Product.Count);
                        ord.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        prod = db.Products.First(p => p.ProductID == item.ID);
                        if (prod == null)
                            continue;
                        ord = new OrdersRefProduct()
                        {
                            ProductID = item.ID,
                            ID = Guid.NewGuid(),
                            CreateDate = DateTime.Now,
                        };
                        oldCount = 0;
                        order.OrdersRefProducts.Add(ord);
                    }
                    prod = (prod == null) ? db.Products.First(p => p.ProductID == item.ID) : prod;
                    ord.Count += Math.Min(item.Count, prod.Count);
                    prod.Count -= ord.Count - oldCount;
                    prod.IsVisible = prod.Count > 0;
                    
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

                foreach (var item in order.OrdersRefProducts)
                {
                    item.Product.Count += item.Count;
                    item.Product.IsVisible = item.Product.Count > 0;
                }

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
                    var oldCount = item.Count;
                    item.Count = (Math.Min(newCounts.First(c => c.ID == item.ID).Count, item.Product.Count));
                    item.Product.Count -= item.Count - oldCount;
                    item.Product.IsVisible = item.Product.Count > 0;
                }

                db.SubmitChanges();
            }
        }

        public bool TryFormOrderIPN(PaymentTypes paymentType, string transactionID, Guid orderID, decimal payment_gross,
            DateTime payment_date, string specialNotes)
        {
            using (var db = new ShopDataContext())
            {
                if (db.Orders.Any(o => o.TransactionID == transactionID))
                    return true;

                var order = db.Orders.FirstOrDefault(o => o.OrderID == orderID && !o.IsPaid);

                if (order == null)
                    return false;

                var totalSum = order.OrdersRefProducts.Sum(r => (r.Product.Price + r.Product.Tax + r.Product.Shipping) * r.Count);
                if (totalSum != payment_gross)
                    return false;

                order.IsActive = false;
                order.IsPaid = true;
                order.PaymentTypeID = (int)paymentType;
                order.OrderStatusID = (int)BL.OrderStatus.Paid;
                order.TotalSum = Convert.ToDecimal(order.OrdersRefProducts.Sum(r => r.Product.Price * r.Count));
                order.CreateDate = payment_date;
                order.TransactionID = transactionID;
                order.DeliveryDate = DateTime.Now.AddHours(double.Parse(ConfigurationManager.AppSettings["DeliveryTime"]));
                order.DeliveryTypeID = (int)DeliveryTypes.NotDelivered;
                order.SpecialNote = specialNotes;
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

                foreach (var item in order.OrdersRefProducts)
                {
                    item.Product.Count += item.Count;
                    item.Product.IsVisible = item.Product.Count > 0;
                }
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
                    var oldCount = item.Count;
                    item.Count = (Math.Min(item.Count, item.Product.Count));
                    item.Product.Count += oldCount - item.Count;
                    item.Product.IsVisible = item.Product.Count > 0;
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

                foreach (var item in order.OrdersRefProducts)
                {
                    item.CreateDate = DateTime.Now;
                }

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

        public void UpadateOrderTime(Guid orderID)
        {
            using (var db = new ShopDataContext())
            {
                var refs = db.OrdersRefProducts.Where(o => o.OrderID == orderID);

                foreach (var item in refs)
                {
                    item.CreateDate = DateTime.Now;
                }

                db.SubmitChanges();
            }
        }

        public void UpadateOrdersCounts()
        {
            using (var db = new ShopDataContext())
            {
                var timeLimit = int.Parse(ConfigurationManager.AppSettings["OrderRefExpireTimeMin"]);
                var expireDate = DateTime.Now.AddHours(-timeLimit);

                var expiredOrders = db.OrdersRefProducts.Where(o => !o.Order.IsPaid && o.CreateDate > expireDate).ToList();
                if (expiredOrders.Count > 0)
                    expireDate = expireDate;

                foreach (var item in expiredOrders)
                {
                    item.Product.Count += item.Count;
                }

                db.OrdersRefProducts.DeleteAllOnSubmit(expiredOrders);
                db.SubmitChanges();
            }
        }
    }
}
