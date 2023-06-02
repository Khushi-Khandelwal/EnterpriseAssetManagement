using AssetManagement.Helper;
using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class HardwareController : Controller
    {
        HelperUrl helper = new HelperUrl();
        public ActionResult Option()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            List<HardwareModel> hardwares = new List<HardwareModel>();
            HttpClient client = helper.Initaial();

            HttpResponseMessage res = await client.GetAsync("/Hardware/GetAll");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                hardwares = JsonConvert.DeserializeObject<List<HardwareModel>>(result);
            }
            return View(hardwares);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HardwareModel hardware)
        {
            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<HardwareModel>("/Haredware/Add", hardware);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Hardware Details is Not Valid");

            return View(hardware);
        }

        public ActionResult Edit(int id)
        {
            HardwareModel hardware = null;
            HttpClient client = helper.Initaial();
            var responseTask = client.GetAsync("Hardware/Search?id=" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                hardware = JsonConvert.DeserializeObject<HardwareModel>(readTask);

            }

            return View(hardware);
        }
        [HttpPost]
        public ActionResult Edit(HardwareModel hardware)
        {
            HttpClient client = helper.Initaial();
            var putTask = client.PutAsJsonAsync<HardwareModel>($"/Hardware/Update/{hardware.HardwareId}", hardware);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            HttpClient client = helper.Initaial();
            var deleteTask = client.DeleteAsync($"/Hardware/Delete/{id}");
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
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
