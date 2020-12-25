using System;
using System.Collections.Generic;
using System.Text;

namespace LI.Contracting.EntityDTO
{
   public class ContractDTO
    {
        public string ContractId { get; set; }
        public string FirstPartyId { get; set; }
        public string FirstParty { get; set; }
        public string SecondPartyId { get; set; }
        public string SecondParty { get; set; }

        public string ContractName { get; set; }
    }
}
