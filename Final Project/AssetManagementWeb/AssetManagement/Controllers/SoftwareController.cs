using AssetManagement.Helper;
using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class SoftwareController : Controller
    {
        HelperUrl helper = new HelperUrl();

        public ActionResult Option()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            List<SoftwareModel> softwares = new List<SoftwareModel>();
            HttpClient client = helper.Initaial();

            HttpResponseMessage res = await client.GetAsync("/Software/GetAll");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                softwares = JsonConvert.DeserializeObject<List<SoftwareModel>>(result);
            }
            return View(softwares);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(SoftwareModel software)
        {
            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<SoftwareModel>("/Software/Add", software);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Software Detail is Not Valid");

            return View(software);
        }

        public ActionResult Edit(int id)
        {
            SoftwareModel software = null;
            HttpClient client = helper.Initaial();
            var responseTask = client.GetAsync("Software/Search?id=" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                software = JsonConvert.DeserializeObject<SoftwareModel>(readTask);

            }

            return View(software);
        }
        [HttpPost]
        public ActionResult Edit(SoftwareModel software)
        {
            HttpClient client = helper.Initaial();
            var putTask = client.PutAsJsonAsync<SoftwareModel>($"/Software/Update/{software.SoftwareId}", software);
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
            var deleteTask = client.DeleteAsync($"/Software/Delete/{id}");
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
            var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/AssignSoftwareAsset", asset);

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
            var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/UnassignSoftwareAsset", asset);

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
