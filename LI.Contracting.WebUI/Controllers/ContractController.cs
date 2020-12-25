using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LI.Contracting.ClientAPI;
using LI.Contracting.EntityDTO;

namespace LI.Contracting.WebUI.Controllers
{
    public class ContractController : Controller
    {
        private IContractClient _contractClient;
        public ContractController(IContractClient contractClient)
        {
            _contractClient = contractClient;
        }
        public async Task<IActionResult> Index()
        {
            return View(await InitializeModel());
        }

        private async Task<ContractViewModel> InitializeModel()
        {
            var carrier = await _contractClient.ListAllCarrier();
            var mga = await _contractClient.ListAllMGA();
            var advisor = await _contractClient.ListAllAdvisor();

            ContractViewModel contractViewModel = new ContractViewModel();
            foreach (var item in carrier)
            {
                contractViewModel.ListEntity.Add(new System.Web.Mvc.SelectListItem { Text = item.BusinessName, Value = "Carrier|" + item.BusinessId });
            }
            foreach (var item in mga)
            {
                contractViewModel.ListEntity.Add(new System.Web.Mvc.SelectListItem { Text = item.BusinessName, Value = "MGA|" + item.BusinessId });
            }
            foreach (var item in advisor)
            {
                contractViewModel.ListEntity.Add(new System.Web.Mvc.SelectListItem { Text = item.LastName + ", " + item.FirstName, Value = "Advisor|" + item.AdvisorId });
            }

            return contractViewModel;
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContractViewModel contractViewModel)
        {
            int ret = -1;
            if (contractViewModel.FirstPartyId == contractViewModel.SecondPartyId) {
                ModelState.AddModelError("Error", "Both Entity Cannot be same");
                var returnmodel = await InitializeModel();
                returnmodel.FirstPartyId = contractViewModel.FirstPartyId;
                returnmodel.SecondPartyId = contractViewModel.SecondPartyId;
                return View(returnmodel);
            }
            var contract = new ContractDTO { 
                SecondPartyId = contractViewModel.SecondPartyId.Split('|')[1], 
                FirstPartyId = contractViewModel.FirstPartyId.Split('|')[1],
                SecondParty = contractViewModel.SecondPartyId.Split('|')[0],
                FirstParty = contractViewModel.FirstPartyId.Split('|')[0],
                ContractId =contractViewModel.ContractId 
            };
            if (string.IsNullOrEmpty(contractViewModel.ContractId))
            {
                ret = await _contractClient.CreateContract(contract);
            }
            else
            {
                ret = await _contractClient.UpdateContract(contractViewModel.ContractId, contract);
            }
            ModelState.AddModelError("Sucess", "Contract Saved.");
            return View(await InitializeModel());
            
        }
       

        public IActionResult Grid()
        {
            return PartialView("_Grid", new ContractDTO());
        }
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            var mga = await _contractClient.GetContractById(id);
            return PartialView("_Edit", mga);
        }
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            int ret = -1;
            ret = await _contractClient.DeleteContract(id);
            return Json(ret);

        }
        public async Task<IActionResult> LoadGrid()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            // Getting all Customer data  
            var mgadata = await _contractClient.ListAllContract();
            //total number of rows count   
            recordsTotal = mgadata.Count();
            //Paging   
            var data = mgadata.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });


        }

    }
}
