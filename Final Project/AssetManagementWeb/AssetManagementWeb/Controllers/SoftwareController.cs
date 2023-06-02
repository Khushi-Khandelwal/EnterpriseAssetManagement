using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using AssetManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class SoftwareController : Controller
    {

        HelperUrl helper = new HelperUrl();
        private readonly SoftwareService softwareService;

        private readonly IHttpContextAccessor httpContextAccessor;
        public SoftwareController(SoftwareService softwareService, IHttpContextAccessor httpContextAccessor)
        {
            this.softwareService = softwareService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
         
            return View(softwareService.GetSoftware().Result);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(SoftwareModel software)
        {
          
            if (softwareService.AddSoftware(software))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Software Detail is Not Valid");

            return View(software);
        }

        public ActionResult Edit(int id)
        {
          
            AssetModel assetClass = null;
          
            assetClass = softwareService.SearchSoftware(id);
            return View(assetClass);

        }

        [HttpPost]
        public ActionResult Edit(AssetModel software)
        {
         
            if (softwareService.EditSoftware(software))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Software Detail is Not Valid");

            return View(software);
        }

        public ActionResult Delete(int id)
        {
          
            if (softwareService.DeleteSoftware(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

      
    }
}
