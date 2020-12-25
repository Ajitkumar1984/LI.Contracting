using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LI.Contracting.EntityDTO
{
   public class AdvisorDTO
    {
        public string AdvisorId { get; set; }
        [Display(Name = "Fisrt Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fisrt Name is Required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is Required.")]
        public string LastName { get; set; }
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required.")]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is Required.")]
        [MaxLength(10, ErrorMessage = "Phone Number must be of 10 digit")]
        [MinLength(10, ErrorMessage = "Phone Number must be of 10 digit")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Health Status")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required.")]
        public string HealthStatus { get; set; }

       
    }
}
