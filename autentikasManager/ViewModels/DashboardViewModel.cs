using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace autentikasManager.ViewModels
{
    public class DashboardViewModel
    {
        public IList<DatesDTO> DatesInfo { get; set; }
        public int PendingOrders { get; set; }
        public int KookieCount { get; set; }
        public int KanelaCount { get; set; }
        public int KakaoCount { get; set; }
        public int TotalOrders { get; set; }
        public IList<PackageCountDTO> PackageCount { get; set; }
        public IList<MonthInfo> MonthInfo { get; set; }
    }
}