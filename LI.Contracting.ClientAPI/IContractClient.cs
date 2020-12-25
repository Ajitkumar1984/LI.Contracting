using LI.Contracting.EntityDTO;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LI.Contracting.ClientAPI
{
   public interface IContractClient
    {
        Task<MGADTO> GetMGAById(string mgaid);
        Task<List<MGADTO>> ListAllMGA();
        Task<int> CreateMGA(MGADTO MGAModel);
        Task<int> UpdateMGA(string mgaid, MGADTO MGAModel);
        Task<int> DeleteMGA(string mgaid);

        Task<CarrierDTO> GetCarrierById(string id);
        Task<List<CarrierDTO>> ListAllCarrier();
        Task<int> CreateCarrier(CarrierDTO carrierModel);
        Task<int> UpdateCarrier(string id, CarrierDTO carrierModel);
        Task<int> DeleteCarrier(string id);

        Task<AdvisorDTO> GetAdvisorById(string id);
        Task<List<AdvisorDTO>> ListAllAdvisor();
        Task<int> CreateAdvisor(AdvisorDTO Model);
        Task<int> UpdateAdvisor(string id, AdvisorDTO Model);
        Task<int> DeleteAdvisor(string id);


        Task<ContractDTO> GetContractById(string id);
        Task<List<ContractDTO>> ListAllContract();
        Task<int> CreateContract(ContractDTO Model);
        Task<int> UpdateContract(string id, ContractDTO Model);
        Task<int> DeleteContract(string id);
    }
}
