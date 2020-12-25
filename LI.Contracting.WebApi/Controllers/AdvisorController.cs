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
    public class AdvisorController : ControllerBase
    {
        private readonly IContractDataManager<AdvisorEntity> _advisorManager;
        private readonly IMapper _mapper;
        public AdvisorController(IContractDataManager<AdvisorEntity> advisorManager, IMapper mapper)
        {
            _advisorManager = advisorManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<List<AdvisorDTO>> Get()
        {
            var lstadvisor = await _advisorManager.GetAll();
            var lstadvisormodel = _mapper.Map<List<AdvisorDTO>>(lstadvisor);
            return lstadvisormodel;
        }
        [HttpGet("{id}")]
        public async Task<AdvisorDTO> Get(string id)
        {
            var advisor = await _advisorManager.GetById(id);
            var advisormodel = _mapper.Map<AdvisorDTO>(advisor);
            return advisormodel;
        }
        [HttpPost]
        public async Task<int> Create([FromBody] AdvisorDTO advisor)
        {
            return await _advisorManager.Create(_mapper.Map<AdvisorEntity>(advisor));
        }
        [HttpPut("{id}")]
        public async Task<int> Update([FromRoute]string id, [FromBody] AdvisorDTO advisor)
        {
            return await _advisorManager.Update(_mapper.Map<AdvisorEntity>(advisor),id);
        }
        [HttpDelete("{id}")]
        public async Task<int> Delete([FromRoute] string id)
        {
            return await _advisorManager.Delete(id);
        }
    }
}
