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
        public bool Active { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? DeactiveDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CookiesQuantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }

        public void Activate()
        {
            Active = !Active;
            if (Active)
                ActiveDate = DateTime.Now;
            else
                DeactiveDate = DateTime.Now;
        }
    }
}
