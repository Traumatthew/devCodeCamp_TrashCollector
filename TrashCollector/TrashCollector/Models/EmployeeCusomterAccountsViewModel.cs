using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class EmployeeCustomerAccountsViewModel
    {
        public CustomerAndAccountViewModel CustViewModel { get; set; }
        public Employee emp { get; set; }
    }
}