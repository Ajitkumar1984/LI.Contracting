
using NUnit.Framework;
using System.Linq;
using AutoMapper;

using System.Threading.Tasks;
using LI.Contracting.WebApi.UnitTest.DataFixture;
using LI.Contracting.DataContext;
using LI.Contracting.EntityDTO;

namespace LI.Contracting.WebApi.UnitTest
{
   public class MGAControllerTest :ContractDataFixture
    {
        private IContractDataManager<MGAEntity> _manager;
        private IContractDataManager<MGAEntity> _seedmanager;
        private  IMapper _mapper;
        [SetUp]
        public void Setup()
        {            
            _manager = new ContractDataManager<MGAEntity>(DbContext());
            _seedmanager = new ContractDataManager<MGAEntity>(DbContext());
            _mapper = MapperProfile();
        }

        private async Task<string> SeedMGAData()
        {
            var guid = System.Guid.NewGuid();
            var mga = new MGADTO() { BusinessId = guid.ToString(), BusinessAddress = "Toronto", BusinessName = "MGA X1", BusinessPhoneNumber = "444-444-4444" };
            int ret = await _seedmanager.Create(_mapper.Map<MGAEntity>(mga));           
            return guid.ToString();
        }
        [Test]
        public async Task Test_AddedNewMGA()
        {
            var guid = System.Guid.NewGuid();
            var mga = new MGADTO() { BusinessId = guid.ToString(), BusinessAddress = "Toronto", BusinessName = "MGA X1", BusinessPhoneNumber = "444-444-4444" };
            int ret = await _manager.Create(_mapper.Map<MGAEntity>(mga));
            Assert.AreEqual(1, ret);
            var newmga = await _manager.GetById(guid.ToString());
            Assert.AreEqual(mga.BusinessId, newmga.BusinessId.ToString());           
            Assert.AreEqual(1, _manager.GetAll().Result.Count());
        }

        [Test]
        public async Task Test_UpdateMGA()
        {           
            var guid = await SeedMGAData(); 
            var mga = new MGADTO() { BusinessId = guid.ToString(), BusinessAddress = "Toronto", BusinessName = "MGA X2", BusinessPhoneNumber = "555-555-5555" };
           // _manager = new MGAManager(DbContext(), MapperProfile());
            int ret = await _manager.Update(_mapper.Map<MGAEntity>(mga),guid);
            Assert.AreEqual(1, ret);
            var newmga = await _manager.GetById(guid.ToString());
            Assert.AreEqual(mga.BusinessId, newmga.BusinessId.ToString());
            Assert.AreEqual(mga.BusinessName, newmga.BusinessName);
            Assert.AreEqual(mga.BusinessPhoneNumber, newmga.BusinessPhoneNumber);
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
