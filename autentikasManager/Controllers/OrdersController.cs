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

        public ActionResult Index()
        {
            var orders = _repo.GetAll();
            return View(orders);
        }

        public string GetData()
        {
            var orders = _repo.GetAll();
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
    }
}