using PagedList;
using ShopAnDam.Models.Framework;
using ShopAnDam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Models.Dao
{
    public class OrderDao
    {
        AnDamDBContext db = null;
        public OrderDao()
        {
            db = new AnDamDBContext();
        }
        public long Insert(Order order)
        {

            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;

        }
        public IEnumerable<Order> ListAllPaging(string SearchHoaDon, string dateOfOrder, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(SearchHoaDon) && string.IsNullOrEmpty(dateOfOrder))
            {
                model = model.Where(x => x.NameShip.Contains(SearchHoaDon));
            }
            if (!string.IsNullOrEmpty(dateOfOrder) && string.IsNullOrEmpty(SearchHoaDon))
            {
                string[] datesplit = dateOfOrder.Split('-');
                var month = datesplit[1];
                var year = datesplit[0];
                model = model.Where(x => x.CreateDate.Value.Month.ToString().Contains(month) && x.CreateDate.Value.Year.ToString().Contains(year));
            }
            if (!string.IsNullOrEmpty(dateOfOrder) && !string.IsNullOrEmpty(SearchHoaDon))
            {
                string[] datesplit = dateOfOrder.Split('-');
                var month = datesplit[1];
                var year = datesplit[0];
                model = model.Where(x => x.CreateDate.Value.Month.ToString().Contains(month) && x.CreateDate.Value.Year.ToString().Contains(year) && x.NameShip.Contains(SearchHoaDon));

            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public Order getById(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<OrderViewModel> ListDetailByOrderID(int orderid, ref int totalRecord, int page=1 , int pageSize =1)
        {
            totalRecord = db.Order_Detail.Where(x => x.OrderID == orderid).Count();
            var model = (from a in db.Order_Detail
                         join b in db.Orders on a.OrderID equals b.ID
                         join c in db.PaymentStatus on b.Status equals c.ID
                         join d in db.Products on a.ProductID equals d.ID
                         where a.OrderID == orderid
                         select new
                         {
                             ID = a.OrderID,
                             NameShip = b.NameShip,
                             PhoneShip = b.PhoneShip,
                             AddressShip = b.AdressShip,
                             MailShip = b.MailShip,
                             CreateDate = b.CreateDate,
                             Quantity = a.Quantity,
                             Price = a.Price,
                             MotionPrice = d.MotionPrice,
                             ProductName = d.Name,
                             Status = c.ID,
                             FormOfPayment = b.FormOfPayment,
                             ProvintID = b.ProvinID,
                             DistrictID = b.DistricID,
                         })
                         .AsEnumerable().Select(x => new OrderViewModel()
                         {
                             ID = (int)x.ID,
                             NameShip = x.NameShip,
                             PhoneShip = x.PhoneShip,
                             AdressShip = x.AddressShip,
                             MailShip = x.MailShip,
                             CreateDate = x.CreateDate,
                             Quantity = (int)x.Quantity,
                             ProductName = x.ProductName,
                             Price = x.Price,
                             MotionPrice = x.MotionPrice,
                             Status = x.Status,
                             FormOfPayment = x.FormOfPayment,
                             ProvintID = x.ProvintID,
                             DistrictID = x.DistrictID,
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public bool Update(Order entity)
        {
            try
            {
                var order = db.Orders.Find(entity.ID);

                order.CreateDate = DateTime.Now;
                order.FormOfPayment = entity.FormOfPayment;
                order.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<Order> ListAllCancelOrderPaging(string SearchHoaDon, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(SearchHoaDon))
            {
                model = model.Where(x => x.NameShip.Contains(SearchHoaDon) && x.Status == 4);
            }
            return model.Where(x => x.Status == 4).OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public IEnumerable<Order> ListAllOrderPaging(string SearchHoaDon, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(SearchHoaDon))
            {
                model = model.Where(x => x.NameShip.Contains(SearchHoaDon));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<OrderViewModel> ListDetailByOrder(ref int totalRecord, int page = 1, int pageSize = 10)
        {
            //Hóa đơn đã gia thành công
            totalRecord = db.Orders.Count();
            var model = (from a in db.Orders
                         join b in db.Order_Detail on a.ID equals b.OrderID
                         join c in db.Products on b.ProductID equals c.ID
                         join d in db.Goods on b.ProductID equals d.ProductID
                         where a.Status == 3
                         group new { d.Prices, b.Price, b.Quantity, a.CreateDate } by new { a.ID } into g


                         select new
                         {
                             ID = g.Key.ID,
                             CreateDate = g.Select(e => e.CreateDate).FirstOrDefault(),
                             Prices = g.Sum(l => l.Prices * l.Quantity),
                             Quantity = g.Count(),
                             Price = g.Sum(w => w.Price * w.Quantity),
                         })
                         .AsEnumerable().Select(x => new OrderViewModel()
                         {
                             ID = (int)x.ID,
                             CreateDate = x.CreateDate,
                             Quantity = x.Quantity,
                             Price = x.Price,
                             Prices = x.Prices,
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public int CountPendingOrders()//đêm số lượng hóa đơn chưa xử lý
        {
            return db.Orders.Count(x => x.ID == 1);
        }
        public IEnumerable<Order> getOrderByIdUser(long id)
        {
            IQueryable<Order> model = db.Orders;
            return  model.Where(x => x.CustomersID == id).OrderByDescending(x => x.CreateDate).ToList();
        }
        public int CancelOrder(int id)
        {
            var cancel = db.Orders.Find(id);
            cancel.Status = 4;
            db.SaveChanges();
            return cancel.Status;
        }
    }
   
}