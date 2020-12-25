
using NUnit.Framework;
using System.Linq;
using AutoMapper;

using System.Threading.Tasks;
using LI.Contracting.WebApi.UnitTest.DataFixture;
using LI.Contracting.DataContext;
using LI.Contracting.EntityDTO;

namespace LI.Contracting.WebApi.UnitTest
{
   public class AdvisorControllerTest :ContractDataFixture
    {
        private IContractDataManager<AdvisorEntity> _manager;
        private IContractDataManager<AdvisorEntity> _seedmanager;
        private  IMapper _mapper;
        [SetUp]
        public void Setup()
        {            
            _manager = new ContractDataManager<AdvisorEntity>(DbContext());
            _seedmanager = new ContractDataManager<AdvisorEntity>(DbContext());
            _mapper = MapperProfile();
        }

        private async Task<string> SeedMGAData()
        {
            var guid = System.Guid.NewGuid();
            var advisor = new AdvisorDTO() { AdvisorId = guid.ToString(), Address = "Toronto", FirstName = "Advisor X1",LastName="X1",PhoneNumber = "444-444-4444" };
             await _seedmanager.Create(_mapper.Map<AdvisorEntity>(advisor));           
            return guid.ToString();
        }
        [Test]
        public async Task Test_AddedNewMGA()
        {
            var guid = System.Guid.NewGuid();
            var advisor = new AdvisorDTO() { AdvisorId = guid.ToString(), Address = "Toronto", FirstName = "Advisor X1", LastName = "X1", PhoneNumber = "444-444-4444" };
            int ret = await _manager.Create(_mapper.Map<AdvisorEntity>(advisor));
            Assert.AreEqual(1, ret);
            var newrec = await _manager.GetById(guid.ToString());
            Assert.AreEqual(advisor.AdvisorId, newrec.AdvisorId.ToString());           
            Assert.AreEqual(1, _manager.GetAll().Result.Count());
        }

        [Test]
        public async Task Test_UpdateMGA()
        {           
            var guid = await SeedMGAData();
            var advisor = new AdvisorDTO() { AdvisorId = guid.ToString(), Address = "Toronto", FirstName = "Advisor X2", LastName = "X2", PhoneNumber = "444-444-4444" };
            int ret = await _manager.Update(_mapper.Map<AdvisorEntity>(advisor),guid);
            Assert.AreEqual(1, ret);
            var newrec = await _manager.GetById(guid.ToString());
            Assert.AreEqual(advisor.AdvisorId, newrec.AdvisorId.ToString());
            Assert.AreEqual(advisor.FirstName, newrec.FirstName);
            Assert.AreEqual(advisor.LastName, newrec.LastName);
        }

        [Test]
        public async Task Test_DeleteMGA()
        {
            var guid = await SeedMGAData();
            var newmga = await _manager.GetById(guid.ToString());
            Assert.IsNotNull(newmga);
            int ret = await _manager.Delete(guid);
            Assert.AreEqual(1, ret);
             newmga = await _manager.GetById(guid.ToString());
            Assert.IsNull(newmga);
        }
    }
}
