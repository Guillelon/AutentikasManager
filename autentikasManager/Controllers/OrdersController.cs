using autentikasManager.ViewModels;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace autentikasManager.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private OrdersRepository _repo;


        public OrdersController()
        {
            _repo = new OrdersRepository();
        }

        public ActionResult Index(int addedId = 0)
        {
            var queryDate = string.Empty;
            if (addedId > 0)
            {
                var newOrder = _repo.Get(addedId);
                queryDate = newOrder.TentativeDeliveryDateFormatted;
            }
            var viewModel = new IndexViewModel();
            viewModel.Date = queryDate;
            return View("Index", viewModel);
        }

        public string GetData(bool delivered, bool packaged, bool paid)
        {
            var orders = _repo.GetPending(delivered, packaged, paid);
            return new JavaScriptSerializer().Serialize(orders);
        }

        public string GetDataByDate(DateTime date, bool delivered, bool packaged, bool paid)
        {
            var orders = _repo.GetByDate(date, delivered, packaged, paid);
            return new JavaScriptSerializer().Serialize(orders);
        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View("Add", id);
        }

        public string GetOrder(int id)
        {
            var order = _repo.Get(id);
            return new JavaScriptSerializer().Serialize(order);
        }

        [HttpPost]
        public string Process(Order order)
        {
            order.TotalPrice = order.OrderDetails.Sum(d => d.Price);
            order.TotalCost = order.OrderDetails.Sum(d => d.Cost);
            if (order.DeliveryMethod == "Pickup")
                order.DeliveryFee = 0;
            order.TotalPrice += order.DeliveryFee;
            var newOrder = _repo.AddOrEdit(order);
            return newOrder.Id.ToString();
        }

        public string Delete(int id)
        {
            _repo.DeleteOrder(id);
            return "200";
        }

        public string MarkAsPrepared(int id)
        {
            var order = _repo.Get(id);
            order.Prepare();
            _repo.SimpleEdit(order);
            return "200";
        }

        public string MarkAsDelivered(int id)
        {
            var order = _repo.Get(id);
            order.Deliver();
            _repo.SimpleEdit(order);
            return "200";
        }

        public string MarkAsPayed(int id)
        {
            var order = _repo.Get(id);
            order.Pay();
            _repo.SimpleEdit(order);
            return "200";
        }

        public string MarkEverything(int id)
        {
            var order = _repo.Get(id);
            order.MarkAll();
            _repo.SimpleEdit(order);
            return "200";
        }

        public string GetClientsByName(string query)
        {
            var clients = _repo.GetByClientName(query);
            return new JavaScriptSerializer().Serialize(clients);
        }

        public string GetOrderByClientName(string query)
        {
            var order = _repo.GetOrderByClientName(query);
            return new JavaScriptSerializer().Serialize(order);
        }
    }
}