using AutoMapper;
using LI.Contracting.DataContext;
using LI.Contracting.EntityDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LI.Contracting.WebApi.UnitTest.DataFixture
{
    public class ContractDataFixture : IDisposable
    {
       
        public ContractDataFixture()
        {
        }

        protected IMapper MapperProfile()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<MGAEntity, MGADTO>().ReverseMap();
                opts.CreateMap<AdvisorEntity, AdvisorDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            return mapper;
        }
        protected DbContextOptions<ContractingContext> DbContext()
        {
            var options = new DbContextOptionsBuilder<ContractingContext>()
                    .UseInMemoryDatabase("ContractDatabase")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;
            return options;
        }
       
        public void Dispose()
        {
            
        }
    }
}
