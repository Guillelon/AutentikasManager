using autentikasManager.ViewModels;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace autentikasManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private OrdersRepository ordersRepository;

        public HomeController()
        {
            ordersRepository = new OrdersRepository();
        }

        public ActionResult Dashboard()
        {
            var viewModel = new DashboardViewModel();
            viewModel.DatesInfo = ordersRepository.GetNextDeliveryDatesInfo();
            viewModel.PendingOrders = ordersRepository.PendingOrders();
            var dto = ordersRepository.GetCookiesCounts();
            viewModel.KakaoCount = dto.KakaoCount;
            viewModel.KanelaCount = dto.KanelaCount;
            viewModel.KookieCount = dto.KookieCount;
            viewModel.PackageCount = ordersRepository.GetPackageCount();
            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}