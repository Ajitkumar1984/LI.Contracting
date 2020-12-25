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
    public class CarrierController : ControllerBase
    {
        private readonly IContractDataManager<CarrierEntity> _carrierManager;
        private readonly IMapper _mapper;
        public CarrierController(IContractDataManager<CarrierEntity> carrierManager, IMapper mapper)
        {
            _carrierManager = carrierManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CarrierDTO>> Get()
        {
            var lstcarrier = await _carrierManager.GetAll();
            var lstcarriermodel = _mapper.Map<List<CarrierDTO>>(lstcarrier);
            return lstcarriermodel;
        }

        [HttpGet("{id}")]
        public async Task<CarrierDTO> Get(string id)
        {
            var carrier = await _carrierManager.GetById(id);
            var carriermodel = _mapper.Map<CarrierDTO>(carrier);
            return carriermodel;
           
        }

        [HttpPost]
        public async Task<int> Create([FromBody] CarrierDTO carrier)
        {
            return await _carrierManager.Create(_mapper.Map<CarrierEntity>(carrier));
        }

        [HttpPut("{id}")]
        public async Task<int> Update([FromRoute] string id, [FromBody] CarrierDTO carrier)
        {
            return await _carrierManager.Update(_mapper.Map<CarrierEntity>(carrier),id);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(string id)
        {
            return await _carrierManager.Delete(id);
        }
    }
}
