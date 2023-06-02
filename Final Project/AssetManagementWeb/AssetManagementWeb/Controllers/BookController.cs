using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using AssetManagementWeb.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Reflection.Metadata.BlobBuilder;

namespace AssetManagement.Controllers
{

    public class BookController : Controller
    {
        HelperUrl helper = new HelperUrl();
        private readonly BookService bookService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public BookController(BookService bookService, IHttpContextAccessor httpContextAccessor)
        {
            this.bookService = bookService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View(bookService.GetBook().Result);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(BookModel book)
        {
            if (bookService.AddBook(book))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Book Detail is Not Valid");

            return View(book);
        }

        public ActionResult Edit(int id)
        {
            AssetModel assetClass = null;
            assetClass = bookService.SearchBook(id);
            return View(assetClass);
        }
        [HttpPost]
        public ActionResult Edit(AssetModel book)
        {
          
            if (bookService.EditBook(book))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Book Detail is Not Valid");

            return View(book);
           
        }

        public ActionResult Delete(int id)
        {
          
            if (bookService.DeleteBook(id))
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
        public IActionResult Assign(AssetDTO asset)
        {
            HttpClient client = helper.Initaial();

            var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/AssignBookAsset", asset);

            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Asset Id or User Id is Not Valid");

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

            var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/UnassignBookAsset", asset);

            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Asset Id or User Id is Not Valid");

            return View();
        }

    }
}
