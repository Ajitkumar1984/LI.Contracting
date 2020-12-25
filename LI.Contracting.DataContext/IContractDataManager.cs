using LI.Contracting.EntityDTO;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LI.Contracting.DataContext
{
   public interface IContractDataManager<TObject> where TObject : class
    {
        Task<int> Create(TObject modal);
        Task<int> Update(TObject modal, string id);
        Task<int> Delete(string id);
        Task<List<TObject>> GetAll();
        Task<TObject> GetById(string id);
        Task<List<ContractDTO>> GetAllContracts();
    }
}
