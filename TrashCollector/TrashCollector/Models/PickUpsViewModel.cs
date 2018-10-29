using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class PickUpsViewModel
    {
        public List<Customer> standardPickups { get; set; }
        public List<PickUpRequests> specialPickups { get; set; }
    }
}