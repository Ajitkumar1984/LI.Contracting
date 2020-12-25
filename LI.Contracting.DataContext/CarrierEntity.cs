using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LI.Contracting.DataContext
{
   public class CarrierEntity
    {
        public CarrierEntity()
        {
            BusinessId = Guid.NewGuid();
        }
        [Key]
        public Guid BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhoneNumber { get; set; }
        
    }
}
