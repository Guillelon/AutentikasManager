using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PackageRepository
    {
        private autentikasDBContext _context;

        public PackageRepository()
        {
            _context = new autentikasDBContext();
        }

        public IList<Package> GetAll()
        {
            return _context.Package.ToList();
        }
    }
}
