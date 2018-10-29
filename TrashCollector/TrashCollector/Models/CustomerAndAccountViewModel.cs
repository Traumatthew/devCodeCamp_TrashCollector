using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerAndAccountViewModel
    {
        public Customer cust { get; set; }
        public CustomerAccountDetails account { get; set; }
        public List<PickUpRequests> pickups { get; set; }
        public List<Suspensions> suspensions { get; set; }
    }
}