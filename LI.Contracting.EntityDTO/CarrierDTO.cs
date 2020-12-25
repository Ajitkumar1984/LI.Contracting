using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LI.Contracting.EntityDTO
{  

    public class CarrierDTO
    {
        public string BusinessId { get; set; }
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required.")]
        public string BusinessName { get; set; }
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required.")]
        public string BusinessAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is Required.")]
        [MaxLength(10, ErrorMessage = "Phone Number must be of 10 digit")]
        [MinLength(10, ErrorMessage = "Phone Number must be of 10 digit")]
        public string BusinessPhoneNumber { get; set; }


    }
}
