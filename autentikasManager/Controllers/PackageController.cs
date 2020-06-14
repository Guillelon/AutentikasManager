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
    }
}