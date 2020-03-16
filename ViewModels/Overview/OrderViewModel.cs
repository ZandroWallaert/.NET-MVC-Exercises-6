using System.ComponentModel.DataAnnotations;

namespace northwind_app.ViewModels.Overview
{
    public class OrderViewModel
    {
        [Required]
        public System.DateTime? OrderDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string CustomerId { get; set; }
    }
}