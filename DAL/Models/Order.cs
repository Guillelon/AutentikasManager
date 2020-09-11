using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? TentativeDeliveryDate { get; set; }
        [NotMapped]
        public string TentativeDeliveryDateFormatted { get {
                if (TentativeDeliveryDate.HasValue)
                    return TentativeDeliveryDate.Value.ToShortDateString();
                else
                    return string.Empty;
            } }
        [NotMapped]
        public string TentativeDeliveryDateFormattedLong
        {
            get
            {
                if (TentativeDeliveryDate.HasValue)
                {
                    var format = "dddd, MMMM dd, yyyy"; 
                    return
                         TentativeDeliveryDate.Value.ToString(format, new System.Globalization.CultureInfo("es-ES"));
                }
                    
                else
                    return string.Empty;
            }
        }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? PreparedDate { get; set; }
        public DateTime? PayedDate { get; set; }
        public bool Delivered { get; set; }
        public bool Paid { get; set; }
        public bool Prepared { get; set; }
        public string PaymentMethod { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public string ClientEmail { get; set; }
        public string ClientInstagram { get; set; }
        public string Notes { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryFee { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public int TotalCookiesCount
        {
            get
            {
                try
                {
                    return OrderDetails.Sum(o => o.KakaoCount + o.KanelaCount + o.KookieCount);
                }
                catch
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal Profit
        {
            get
            {
                return TotalPrice - TotalCost;
            }
        }

        public Order()
        {
            CreatedDate = DateTime.Now;            
        }

        public void Deliver()
        {
            Delivered = !Delivered;
            DeliveredDate = DateTime.Now;
        }

        public void Prepare()
        {
            Prepared = !Prepared;
            PreparedDate = DateTime.Now;
        }

        public void Pay()
        {            
            Paid = !Paid;
            PayedDate = DateTime.Now;
        }

        public void MarkAll()
        {
            Paid = true;
            Prepared = true;
            Delivered = true;
            PayedDate = DateTime.Now;
            PreparedDate = DateTime.Now;
            DeliveredDate = DateTime.Now;
        }
    }
}
