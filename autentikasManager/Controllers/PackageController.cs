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
    public class PackageController : Controller
    {
        private PackageRepository packageRepository;

        public PackageController()
        {
            packageRepository = new PackageRepository();
        }

        public string GetPackages()
        {
            var packages = packageRepository.GetActive();
            return new JavaScriptSerializer().Serialize(packages);
        }

        public ActionResult Index()
        {
            var packages = packageRepository.GetAll();
            if (TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return RedirectToAction("Error", "Home", null);
        }

        public ActionResult Edit(int id)
        {
            var package = packageRepository.GetById(id);
            return RedirectToAction("Error", "Home", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Package package)
        {
            packageRepository.Edit(package);
            TempData["SuccessMessage"] = "Se editó con éxito el paquete";
            return RedirectToAction("Index");
        }

        public ActionResult Activate(int id)
        {
            var package = packageRepository.GetById(id);
            package.Activate();
            packageRepository.Edit(package);
            TempData["SuccessMessage"] = "Se editó con éxito el paquete";
            return RedirectToAction("Index");
        }
    }
}