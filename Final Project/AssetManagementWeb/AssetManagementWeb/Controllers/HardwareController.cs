using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using AssetManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class HardwareController : Controller
    {
       
        HelperUrl helper = new HelperUrl();
        private readonly HardwareService hardwareService;

        private readonly IHttpContextAccessor httpContextAccessor;
        public HardwareController(HardwareService hardwareService, IHttpContextAccessor httpContextAccessor)
        {
            this.hardwareService = hardwareService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View(hardwareService.GetHardware().Result);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HardwareModel hardware)
        {
          
            if (hardwareService.AddHardware(hardware))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Hardware Detail is Not Valid");

            return View(hardware);
        }

        public ActionResult Edit(int id)
        {
          
            AssetModel assetClass = null;
          
            assetClass = hardwareService.SearchHardware(id);

            return View(assetClass);

        }
        [HttpPost]
        public ActionResult Edit(AssetModel hardware)
        {
           
            if (hardwareService.EditHardware(hardware))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Hardware Detail is Not Valid");

            return View(hardware);
        }

        public ActionResult Delete(int id)
        {
          
            if (hardwareService.DeleteHardware(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        public ActionResult Assign()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Assign(AssetDTO asset)
        {
            HttpClient client = helper.Initaial();
           
            var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/AssignHardwareAsset", asset);

            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Asset Id or User Id is Not valid");
            return View();
        }

        public ActionResult Unassign()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Unassign(AssetDTO asset)
        {
            HttpClient client = helper.Initaial();
           
            var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/UnassignHardwareAsset", asset);

            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Asset Id or User Id is Not valid");

            return View();
        }

    }
}
