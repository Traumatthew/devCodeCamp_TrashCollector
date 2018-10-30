using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerAccountDetails
    {
        [Key]
        public int CustomerAccountId { get; set; }

        [ForeignKey("Customers")]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
 
        public double MoneyOwed { get; set; }
        [Display(Name = "Weekly Pickup Day")]
        public string WeeklyPickUpDay { get; set; }
        public bool CurrentlySuspended { get; set; }

  
    }
}