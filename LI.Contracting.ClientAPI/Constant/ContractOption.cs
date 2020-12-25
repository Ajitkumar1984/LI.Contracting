using System;
using System.Collections.Generic;
using System.Text;

namespace LI.Contracting.ClientAPI
{
    public class ContractOption
    {
        public string BaseUrl { get; set; }
        public MGAEndpoint MGAEndpoint { get; set; }
        public CarrierEndpoint CarrierEndpoint { get; set; }
        public AdvisorEndpoint AdvisorEndpoint { get; set; }
        public ContractEndpoint ContractEndpoint { get; set; }
    }
}
