using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class DatesDTO
    {
        public DateTime TentativeDeliveryDate { get; set; }
        public string TentativeDeliveryDateFormattedLong
        {
            get
            {
                var format = "dddd, MMMM dd";
                return
                        TentativeDeliveryDate.ToString(format, new System.Globalization.CultureInfo("es-ES"));
            }
        }
        public int Counts { get; set; }
    }

    public class CookiesCount
    {
        public int KookieCount { get; set; }
        public int KanelaCount { get; set; }
        public int KakaoCount { get; set; }
    }

    public class PackageCountDTO
    {
        public string Name { get; set; }
        public int PackageCount { get; set; }
    }
}
