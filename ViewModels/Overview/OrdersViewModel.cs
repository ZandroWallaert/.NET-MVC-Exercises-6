using System.Collections.Generic;

namespace northwind_app.ViewModels.Overview
{
    public class OrdersViewModel {
        public string TitlePage {get; set;}
        public string TitleDate {get; set;}
        public string TitleAddress {get; set;}
        public List<OrderViewModel> Orders {get; set;}
    }
}