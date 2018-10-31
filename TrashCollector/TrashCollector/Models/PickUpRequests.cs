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
        [Required(ErrorMessage = "Location Required")]
        public string Place { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Of Pickup is Required")]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Time Required")]
        public string Time { get; set; }
        public double Fee { get; set; }
        public bool complete { get; set; }
        [Display(Name = "Description of items to be picked up:")]
        public string notes { get; set; }
    }


}