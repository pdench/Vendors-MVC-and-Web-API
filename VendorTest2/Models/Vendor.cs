using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VendorTest2.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        [Display(Name = "Vendor Code")]
        [Required]
        [StringLength(12, ErrorMessage = "Vendor Code cannot be longer than 12 characters")]
        public string VendorCode { get; set; }
        [Display(Name = "Vendor Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Vendor Name cannot be longer than 50 characters")]
        public string VendorName { get; set; }
        [Display(Name = "Valid From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ValidFrom { get; set; }
        [Display(Name = "Valid Thru")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ValidThru { get; set; }
    }
}