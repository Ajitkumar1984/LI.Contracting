using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LI.Contracting.ClientAPI;
using LI.Contracting.EntityDTO;
using Microsoft.AspNetCore.Mvc;

namespace LI.Contracting.WebUI.Controllers
{
    public class AdvisorController : Controller
    {
        private IContractClient _contractClient;
        public AdvisorController(IContractClient contractClient)
        {
            _contractClient = contractClient;
        }
        public IActionResult  Index()
        {
           
            return View(new AdvisorDTO());
        }
        [HttpPost]
        public async Task<IActionResult> SaveForm(AdvisorDTO advisorModel)
        {
            int ret = -1;
            if(string.IsNullOrEmpty(advisorModel.AdvisorId))
            {
                ret = await _contractClient.CreateAdvisor(advisorModel);
            }
            else
            {
                ret = await _contractClient.UpdateAdvisor(advisorModel.AdvisorId, advisorModel);
            }
            return Json(ret);
        }
        public IActionResult Create()
        {
            return PartialView("_Create", new AdvisorDTO());
        }
        public IActionResult Grid()
        {
            return PartialView("_Grid", new AdvisorDTO());
        }
        public async Task<IActionResult>  Edit([FromRoute]string id)
        {
            var mga = await _contractClient.GetAdvisorById(id);
            return PartialView("_Edit",mga);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            int ret = -1;
            ret = await _contractClient.DeleteAdvisor(id);
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
            var mgadata = await _contractClient.ListAllAdvisor();
            //total number of rows count   
            recordsTotal = mgadata.Count();
            //Paging   
            var data = mgadata.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            
        }
    }
}
