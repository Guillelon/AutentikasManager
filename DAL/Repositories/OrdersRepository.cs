using DAL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrdersRepository
    {
        private autentikasDBContext _context;

        public OrdersRepository()
        {
            _context = new autentikasDBContext();
        }

        public IList<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public IList<Order> GetPending(bool delivered, bool packaged, bool paid)
        {
            var orders = _context.Order.ToList();
            if (delivered)
                orders = orders.Where(o => o.Delivered == delivered).ToList();
            if (packaged)
                orders = orders.Where(o => o.Prepared == packaged).ToList();
            if (paid)
                orders = orders.Where(o => o.Paid == paid).ToList();
            return orders.OrderBy(o => o.TentativeDeliveryDate).ToList();
        }

        public IList<Order> GetByDate(DateTime date, bool delivered, bool packaged, bool paid)
        {
            var orders = _context.Order.Where(o => o.TentativeDeliveryDate.HasValue &&
                                             o.TentativeDeliveryDate == date).ToList();
            if (delivered)
                orders = orders.Where(o => o.Delivered == delivered).ToList();
            if (packaged)
                orders = orders.Where(o => o.Prepared == packaged).ToList();
            if (paid)
                orders = orders.Where(o => o.Paid == paid).ToList();
            return orders.OrderBy(o => o.TentativeDeliveryDate).ToList();
        }

        public Order AddOrEdit(Order order)
        {
            if(order.Id > 0)
            {
                var orderToEdit = Get(order.Id);

                orderToEdit.ClientName = order.ClientName;
                orderToEdit.ClientEmail = order.ClientEmail;
                orderToEdit.ClientAddress = order.ClientAddress;
                orderToEdit.PaymentMethod = order.PaymentMethod;
                orderToEdit.ClientPhone = order.ClientPhone;
                orderToEdit.TentativeDeliveryDate = order.TentativeDeliveryDate;
                orderToEdit.ClientInstagram = order.ClientInstagram;
                orderToEdit.Notes = order.Notes;
                orderToEdit.DeliveryMethod = order.DeliveryMethod;
                orderToEdit.TotalPrice = order.TotalPrice;
                orderToEdit.TotalCost = order.TotalCost;

                var ordersDetail = GetOrderDetailByOrderId(order.Id);
               
                var orderDetailsToRemove = new List<int>();
                foreach (var detail in ordersDetail)
                {
                    _context.Entry(detail).State = System.Data.Entity.EntityState.Detached;
                    var detailToRemove = order.OrderDetails.Where(d => detail.Id == d.Id).FirstOrDefault();
                    if (detailToRemove == null)
                    {
                        orderDetailsToRemove.Add(detail.Id);
                    }
                }

                foreach(var id in orderDetailsToRemove)
                {
                    var orderDetailToRemove = GetOrderDetail(id);
                    _context.OrderDetail.Remove(orderDetailToRemove);
                }

                var newDetails = order.OrderDetails.Where(od => od.Id == 0).ToList();

                foreach(var newDetail in newDetails)
                {
                    _context.OrderDetail.Add(newDetail);
                    _context.SaveChanges();
                }

                _context.Entry(orderToEdit).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();               
            }
            else
                _context.Order.Add(order);

            _context.SaveChanges();
            return order;
        }

        public Order Get(int id)
        {
            return _context.Order.Where(o => o.Id == id).FirstOrDefault();
        }

        public OrderDetail GetOrderDetail(int id)
        {
            return _context.OrderDetail.Where(o => o.Id == id).FirstOrDefault();
        }

        public IList<OrderDetail> GetOrderDetailByOrderId(int id)
        {
            return _context.OrderDetail.Where(o => o.OrderId == id).ToList();
        }

        public void DeleteOrder(int id)
        {
            var oderToDelete = Get(id);
            _context.Order.Remove(oderToDelete);
            _context.SaveChanges();
        }

        public Order SimpleEdit(Order order)
        {
            _context.Entry(order).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return order;
        }

        public IList<DatesDTO> GetNextDeliveryDatesInfo()
        {
            var query = "SELECT TentativeDeliveryDate, COUNT(*) as 'Counts' FROM [Orders] " +
                        "WHERE  TentativeDeliveryDate >= Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) " +
                        "GROUP BY TentativeDeliveryDate " +
                        "ORDER BY TentativeDeliveryDate ";
            return _context.Database.SqlQuery<DatesDTO>(query).ToList();
        }
        public int PendingOrders()
        {
            return _context.Order.Where(o => !o.Delivered).Count();
        }

        public CookiesCount GetCookiesCounts()
        {
            var query = "SELECT SUM(KookieCount) AS KookieCount," +
                         " SUM(KanelaCount) AS KanelaCount," +
                         " SUM(KakaoCount) AS KakaoCount" +
                         " FROM OrderDetails";
            return _context.Database.SqlQuery<CookiesCount>(query).FirstOrDefault();
        }

        public IList<PackageCountDTO> GetPackageCount()
        {
            var query = " SELECT P.Name, COUNT(*) PackageCount " +
                " FROM PACKAGES P, OrderDetails OD" +
                " WHERE OD.PackageId = P.Id" +
                " GROUP BY P.NAME" +
                " ORDER BY PackageCount DESC";
            return _context.Database.SqlQuery<PackageCountDTO>(query).ToList();
        }

        public IList<string> GetByClientName(string search)
        {
            var query = " SELECT CLIENTNAME FROM [dbo].[Orders] " +
                " WHERE CLIENTNAME LIKE '%" + search + "%'";
            return _context.Database.SqlQuery<string>(query).ToList();
        }

        public Order GetOrderByClientName(string clientName)
        {
            var query = " SELECT * FROM [dbo].[Orders] " +
                " WHERE CLIENTNAME = '" + clientName + "'";
            return _context.Database.SqlQuery<Order>(query).FirstOrDefault();
        }

        public IList<MonthInfo> MonthsInfo()
        {
            var query = " SELECT DATENAME(month, O.[CreatedDate]) as 'Month', COUNT(*), " +
                        " SUM(TotalCost) AS 'Cost', SUM(TotalPrice) AS 'Price', " +
                        " SUM(TotalPrice - TotalCost) AS 'Profit' " +
                        " FROM Orders O" +
                        " GROUP BY DATENAME(month, O.[CreatedDate])";
            return _context.Database.SqlQuery<MonthInfo>(query).ToList();
        }
    }
}
