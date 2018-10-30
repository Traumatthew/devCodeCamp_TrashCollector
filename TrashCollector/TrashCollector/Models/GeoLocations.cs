using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class GeoLocations
    {
        [Key]
        public int GeoID { get; set; }
        [ForeignKey("Customers")]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        public string key = Key.GKey;
        public string srcKey = "https://maps.googleapis.com/maps/api/js?libraries=places&key=" + Key.GKey + "&callback=initMap";

        public dynamic GetLatandLong(Customer cust)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address="+cust.Street.Replace(" ", "+") + cust.City.Replace(" ", "+") + ",+" + cust.State + ",+&key=" + Key.GeoKey;
            var result = new System.Net.WebClient().DownloadString(url);
            var items = JsonConvert.DeserializeObject<dynamic>(result);
            return items.results[0].geometry.location;
        }

        public dynamic GetLatandLong(Employee emp)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address="+emp.Zip+",+&key=" + Key.GeoKey;
            var result = new System.Net.WebClient().DownloadString(url);
            var items = JsonConvert.DeserializeObject<dynamic>(result);
            return items.results[0].geometry.location;
        }

    }
}