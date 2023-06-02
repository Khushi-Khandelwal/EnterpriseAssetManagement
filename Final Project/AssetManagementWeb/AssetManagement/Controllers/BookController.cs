using AssetManagement.Helper;
using AssetManagement.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace AssetManagement.Controllers
{
    public class BookController : Controller
    {
       

        HelperUrl helper = new HelperUrl();

        public ActionResult Option()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            List<BookModel> books = new List<BookModel>();
           
            HttpClient client = helper.Initaial();

            HttpResponseMessage res = await client.GetAsync("/Book/GetAll");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookModel>>(result);
               
               
               
            }
            return View(books);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BookModel book)
        {
              HttpClient client = helper.Initaial();
                var postTask = client.PostAsJsonAsync<BookModel>("/Book/Add", book);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            
            ModelState.AddModelError(string.Empty, "Book Detail is Not Valid");

            return View(book);
        }

        public ActionResult Edit(int id)
        {
            BookModel book = null;
            HttpClient client = helper.Initaial();
            var responseTask = client.GetAsync("Book/Search?id="+id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<BookModel>(readTask);
           
            }

            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(BookModel book)
        {
                 HttpClient client = helper.Initaial();
                 var putTask = client.PutAsJsonAsync<BookModel>($"/Book/Update/{book.BookId}", book);
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
                var deleteTask = client.DeleteAsync($"/Book/Delete/{id}");
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            return RedirectToAction("Index");
        }
       
/*
        public IActionResult Mark1()
        {*//*
            List<BookModel> books = new List<BookModel>();
            List<UserModel> users = new List<UserModel>();
            //
            List<string> l = new List<string>();
            //
            HttpClient client = helper.Initaial();

            HttpResponseMessage res = await client.GetAsync("/Book/GetAll");
            HttpResponseMessage user = await client.GetAsync("/User/GetAll");


            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookModel>>(result);
                users = JsonConvert.DeserializeObject<List<UserModel>>(result);
                SelectList usersSelectLIst = new SelectList(users);
                SelectList booksSelectLIst = new SelectList(books);
                ViewBag.users = usersSelectLIst;
                ViewBag.books = booksSelectLIst;

                foreach (var i in books)
                {
                    l.Add(i.BookName);
                }

                TempData["BookName"] = l;
                Ironman = l;
                return View(books);

            }



            return View();*/
            /*List<string> items = new List<string>
            {
              "Item 1",
              "Item 2",
               "Item 3"
            };

            ViewBag.Items = new SelectList(items);

            return View();*//*
            var model = new MyViewModel();
            model.Values = new[]
            {
                new SelectListItem { Value = "1", Text = "item 1" },
                new SelectListItem { Value = "2", Text = "item 2" },
                new SelectListItem { Value = "3", Text = "item 3" },
            };
            return View(model);

        }
        public ActionResult Mark2(string selectedValue)
        {

            return View();
        }
        [BindProperty]
        public string SelectedValue { get; set; }
*/
        //
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
