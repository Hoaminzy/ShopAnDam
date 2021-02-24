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
                            join c in db.PaymentStatuss on b.Status equals c.ID
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
                             ProductName = d.Name,
                             Status = c.Name,
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
    }
   
}