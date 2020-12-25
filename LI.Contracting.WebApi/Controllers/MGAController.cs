using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LI.Contracting.DataContext;
using LI.Contracting.EntityDTO;
using Microsoft.AspNetCore.Mvc;


namespace LI.Contracting.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MGAController : ControllerBase
    {
        private readonly IContractDataManager<MGAEntity> _mgamanager;
        private readonly IMapper _mapper;
        public MGAController(IContractDataManager<MGAEntity> mgamanager, IMapper mapper)
        {
            _mgamanager = mgamanager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<MGADTO>> Get()
        {            
            var lstmga = await _mgamanager.GetAll();
            var lstmgamodel = _mapper.Map<List<MGADTO>>(lstmga);
            return lstmgamodel;                    
        }
        [HttpGet("{id}")]       
        public async Task<MGADTO> Get(string id)
        {
            var mga = await _mgamanager.GetById(id);
            var mgamodel = _mapper.Map<MGADTO>(mga);
            return mgamodel;
        }
        [HttpPost]
        public async Task<int> Create([FromBody] MGADTO mga)
        {
            return await _mgamanager.Create(_mapper.Map<MGAEntity>(mga));
        }
        [HttpPut("{id}")]
        public async Task<int> Update([FromRoute]string id, [FromBody] MGADTO mga)
        {
            return await _mgamanager.Update(_mapper.Map<MGAEntity>(mga),id);
        }
        [HttpDelete("{id}")]
        public async Task<int> Delete(string id)
        {
            return await _mgamanager.Delete(id);
        }
    }
}
