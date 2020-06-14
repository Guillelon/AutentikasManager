using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int KakaoCount { get; set; }
        public int KanelaCount { get; set; }
        public int KookieCount { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
    }
}
