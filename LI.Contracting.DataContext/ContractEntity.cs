using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LI.Contracting.DataContext
{
   public class ContractEntity
    {
        public ContractEntity()
        {
            ContractId = Guid.NewGuid();
        }
        [Key]
        public Guid ContractId { get; set; }
        public string FirstPartyId { get; set; }
        public string SecondPartyId { get; set; }
        public string FirstParty { get; set; }
        public string SecondParty { get; set; }

    }
}
