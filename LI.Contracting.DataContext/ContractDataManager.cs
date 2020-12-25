using LI.Contracting.EntityDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LI.Contracting.DataContext
{
   public class ContractDataManager<TObject> : IContractDataManager<TObject> where TObject : class
    {
        private ContractingContext _context;

        public ContractDataManager(DbContextOptions options)
        {
            _context = new ContractingContext(options);
        }
        public async Task<int> Create(TObject model)
        {
            int ret = -1;
            try
            {

                _context.Set<TObject>().Add(model);
                ret = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ret;
        }
        public async Task<int> Delete(string id)
        {
            var entity = await _context.Set<TObject>().FindAsync(Guid.Parse(id));
            int ret = -1;

            // Delete All Dependent COntract of Entity
            var mgacontracts = await _context.Contract.Where(ct => ct.FirstPartyId == id || ct.SecondPartyId == id ).ToListAsync();
            var transaction = _context.Database.BeginTransaction();
            foreach (ContractEntity item in mgacontracts)
            {
                var contract = await _context.Contract.Where(st => st.ContractId == item.ContractId).SingleOrDefaultAsync();
                _context.Contract.Attach(contract);
                _context.Contract.Remove(contract);
                await _context.SaveChangesAsync();
            }
            //----------------------------------------
            _context.Set<TObject>().Remove(entity);
            ret = await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return ret;
        }
        public async Task<List<TObject>> GetAll()
        {
            List<TObject> objects = new List<TObject>();
            try
            {
                objects = await _context.Set<TObject>().ToListAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return objects;

        }
        public async Task<TObject> GetById(string id)
        {
            return await _context.Set<TObject>().FindAsync(Guid.Parse(id));
        }
        public async Task<int> Update(TObject newmodel, string id)
        {
            int ret = -1;
            TObject existing = await _context.Set<TObject>().FindAsync(Guid.Parse(id));
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(newmodel);
                ret = await _context.SaveChangesAsync();
            }
            return ret;
        }
        public async Task<List<ContractDTO>> GetAllContracts()
        {
            List<ContractDTO> contractsmodel = new List<ContractDTO>();
            try
            {
                List<ContractEntity> contracts = await _context.Contract.ToListAsync();

                foreach (ContractEntity item in contracts)
                {
                    StringBuilder sbname = new StringBuilder();
                    switch (item.FirstParty)
                    {
                        case "Carrier":
                            sbname.Append(_context.Carrier.Where(cr => cr.BusinessId.ToString() == item.FirstPartyId.ToString()).SingleOrDefault().BusinessName);
                            break;
                        case "MGA":
                            sbname.Append(_context.MGA.Where(cr => cr.BusinessId.ToString() == item.FirstPartyId.ToString()).SingleOrDefault().BusinessName);
                            break;
                        case "Advisor":
                            sbname.Append(_context.Advisor.Where(cr => cr.AdvisorId.ToString() == item.FirstPartyId.ToString()).SingleOrDefault().FirstName);
                            break;
                        default:
                            break;
                    }
                    sbname.Append("-->");
                    switch (item.SecondParty)
                    {
                        case "Carrier":
                            sbname.Append(_context.Carrier.Where(cr => cr.BusinessId.ToString() == item.SecondPartyId.ToString()).SingleOrDefault().BusinessName);
                            break;
                        case "MGA":
                            sbname.Append(_context.MGA.Where(cr => cr.BusinessId.ToString() == item.SecondPartyId.ToString()).SingleOrDefault().BusinessName);
                            break;
                        case "Advisor":
                            sbname.Append(_context.Advisor.Where(cr => cr.AdvisorId.ToString() == item.SecondPartyId.ToString()).SingleOrDefault().FirstName);
                            break;
                        default:
                            break;
                    }
                    if (!contractsmodel.Contains(new ContractDTO { ContractName = sbname.ToString() }))
                    {
                        contractsmodel.Add(new ContractDTO { ContractName = sbname.ToString() });
                    }

                    List<ContractEntity> lstcontract = _context.Contract.Where(ct => ct.FirstPartyId == item.SecondPartyId).ToList();
                    if (lstcontract != null)
                    {
                        foreach (ContractEntity contract in lstcontract)
                        {
                            sbname.Append("-->");
                            switch (contract.SecondParty)
                            {
                                case "Carrier":
                                    sbname.Append(_context.Carrier.Where(cr => cr.BusinessId.ToString() == contract.SecondPartyId.ToString()).SingleOrDefault().BusinessName);
                                    break;
                                case "MGA":
                                    sbname.Append(_context.MGA.Where(cr => cr.BusinessId.ToString() == contract.SecondPartyId.ToString()).SingleOrDefault().BusinessName);
                                    break;
                                case "Advisor":
                                    sbname.Append(_context.Advisor.Where(cr => cr.AdvisorId.ToString() == contract.SecondPartyId.ToString()).SingleOrDefault().FirstName);
                                    break;
                                default:
                                    break;
                            }
                            if (!contractsmodel.Contains(new ContractDTO { ContractName = sbname.ToString() }))
                            {
                                contractsmodel.Add(new ContractDTO { ContractName = sbname.ToString() });
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return contractsmodel;

        }
    }
}
