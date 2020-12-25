using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace LI.Contracting.EntityDTO
{
   public class ContractViewModel
    {

        public ContractViewModel()
        {
            ListEntity = new List<SelectListItem>();
        }

        public string ContractId { get; set; }
        public string FirstPartyId { get; set; }
        public string SecondPartyId { get; set; }

       // public List<ContractModel> Contracts { get; set; }

        public List<SelectListItem> ListEntity { get; set; }
    }
}
