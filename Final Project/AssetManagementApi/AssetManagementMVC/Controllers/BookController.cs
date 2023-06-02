using AssetManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AssetManagementMVC.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            // https://localhost:7188/Book/GetAll
            List<BookModel> booklist = new List<BookModel>();
            client.BaseAddress = new Uri("https://localhost:7188/Book/GetAll");
            var response = client.GetAsync("GetAll");
            response.Wait();
            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<BookModel>>();
                display.Wait();
                booklist = display.Result;
            }
            return View(booklist);
        }
    }
}