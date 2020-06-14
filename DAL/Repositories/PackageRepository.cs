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

        public IList<Package> GetActive()
        {
            return _context.Package.Where(p => p.Active).ToList();
        }

        public Package GetById(int id)
        {
            return _context.Package.Where(p => p.Id == id).FirstOrDefault();
        }

        public Package Edit(Package package)
        {
            _context.Entry(package).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return package;
        }
    }
}
