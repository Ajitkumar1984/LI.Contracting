using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LI.Contracting.ClientAPI;
using LI.Contracting.EntityDTO;
using Microsoft.AspNetCore.Mvc;

namespace LI.Contracting.WebUI.Controllers
{
    public class CarrierController : Controller
    {
        private IContractClient _contractClient;
        public CarrierController(IContractClient contractClient)
        {
            _contractClient = contractClient;
        }
        public IActionResult  Index()
        {
           
            return View(new CarrierDTO());
        }
        [HttpPost]
        public async Task<IActionResult> SaveForm(CarrierDTO carrierModel)
        {
            int ret = -1;
            if(string.IsNullOrEmpty(carrierModel.BusinessId.ToString()))
            {
                ret = await _contractClient.CreateCarrier(carrierModel);
            }
            else
            {
                ret = await _contractClient.UpdateCarrier(carrierModel.BusinessId.ToString(), carrierModel);
            }
            return Json(ret);
        }
        public IActionResult Create()
        {
            return PartialView("_Create", new CarrierDTO());
        }
        public IActionResult Grid()
        {
            return PartialView("_Grid", new CarrierDTO());
        }
        public async Task<IActionResult>  Edit([FromRoute]string id)
        {
            var mga = await _contractClient.GetCarrierById(id);
            return PartialView("_Edit",mga);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            int ret = -1;
            ret = await _contractClient.DeleteCarrier(id);
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
            var mgadata = await _contractClient.ListAllCarrier();
            //total number of rows count   
            recordsTotal = mgadata.Count();
            //Paging   
            var data = mgadata.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            
        }
    }
}
