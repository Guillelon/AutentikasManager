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
            var packages = packageRepository.GetAll();
            return new JavaScriptSerializer().Serialize(packages);
        }

        public ActionResult Index()
        {
            var packages = packageRepository.GetAll();
            if (TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(packages);
        }

        public ActionResult Edit(int id)
        {
            var package = packageRepository.GetById(id);
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Package package)
        {
            packageRepository.Edit(package);
            TempData["SuccessMessage"] = "Se editó con éxito el paquete";
            return RedirectToAction("Index");
        }
    }
}