using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LI.Contracting.DataContext
{
   public class AdvisorEntity
    {
        public AdvisorEntity()
        {
            AdvisorId = Guid.NewGuid();
        }
        [Key]
        public Guid AdvisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string HealthStatus { get; set; }
    }
}
