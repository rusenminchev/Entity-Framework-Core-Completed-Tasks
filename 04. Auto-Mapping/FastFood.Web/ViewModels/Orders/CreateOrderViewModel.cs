namespace FastFood.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public List<int> Items { get; set; }
        public string ItemName { get; set; }

        public List<int> Employees { get; set; }
        public string EmployeeName { get; set; }
    }
}
