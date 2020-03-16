using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace northwind_app.ViewModels.Overview
{
    public class CustomersViewModel {
        public string TitlePage {get; set;}
        public string TitleName {get; set;}
        public string TitleCity {get; set;}
        public List<string> Searched {get; set;}
        public List<CustomerViewModel> Customers {get; set;}
    }
}