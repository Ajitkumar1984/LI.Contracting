using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LI.Contracting.DataContext;
using LI.Contracting.EntityDTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LI.Contracting.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractDataManager<ContractEntity> _contractsManager;
        private readonly IMapper _mapper;
        public ContractController(IContractDataManager<ContractEntity> contractsManager, IMapper mapper)
        {
            _contractsManager = contractsManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<List<ContractDTO>> Get()
        {
            var lstcontracts = await _contractsManager.GetAllContracts();
           // var lstcontractmodel = _mapper.Map<List<ContractModel>>(lstcontracts);
            return lstcontracts;
        }
        [HttpGet("{id}")]
        public async Task<ContractDTO> Get(string id)
        {
            var contract = await _contractsManager.GetById(id);
            var contractmodel = _mapper.Map<ContractDTO>(contract);
            return contractmodel;
        }
        [HttpPost]
        public async Task<int> Create([FromBody] ContractDTO contract)
        {
            return await _contractsManager.Create(_mapper.Map<ContractEntity>(contract));
        }

        [HttpPut("{id}")]
        public async Task<int> Update([FromRoute] string id, [FromBody] ContractDTO contract)
        {
            return await _contractsManager.Update(_mapper.Map<ContractEntity>(contract),id);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete([FromRoute] string id)
        {
            return await _contractsManager.Delete(id);
        }
    }
}
