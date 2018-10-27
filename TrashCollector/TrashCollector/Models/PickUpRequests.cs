using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class PickUpRequests
    {
        [Key]
        public int PickUpId { get; set; }
        [ForeignKey("Customers")]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public double Fee { get; set; }
        public bool complete { get; set; }
        public string notes { get; set; }
    }
}