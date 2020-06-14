using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CookiesQuantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}
