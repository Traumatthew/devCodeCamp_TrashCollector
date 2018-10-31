using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerAndAccountViewModel
    {
        public Customer cust { get; set; }
        public CustomerAccountDetails account { get; set; }
        public List<PickUpRequests> pickups { get; set; }
        public List<Suspensions> suspensions { get; set; }
        public List<Customer> customers { get; set; }
        public List<CustomerAccountDetails> accounts { get; set; }
        public string key = Key.GKey;
        public string srcKey = "https://maps.googleapis.com/maps/api/js?libraries=places&key=" + Key.GKey + "&callback=initMap";
        public GeoLocations geo { get; set; }
    }
}