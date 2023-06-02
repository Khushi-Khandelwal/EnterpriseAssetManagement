using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AssetManagementWeb.Services
{
	public class BookService
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly ISession _session;
		public string token{ get; set; }
		public BookService(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
			_session = _contextAccessor.HttpContext.Session;
			token = _session.GetString("admintoken");
			HelperUrl helper = new HelperUrl();
		}

		public async Task<List<AssetModel>> GetBook()
		{
			HelperUrl helper = new HelperUrl();
			List<AssetModel> books = new List<AssetModel>();

            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
            HttpResponseMessage response = await client.GetAsync("/Book/GetAll");

			if (response.IsSuccessStatusCode)
			{
				var result = response.Content.ReadAsStringAsync().Result;
				books = JsonConvert.DeserializeObject<List<AssetModel>>(result);
			}
			return (books);
		}

		public bool AddBook(BookModel book)
		{
			HelperUrl helper = new HelperUrl();
			HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var postTask = client.PostAsJsonAsync<BookModel>("/Book/Add", book);
			postTask.Wait();

			var result = postTask.Result;
			if (result.IsSuccessStatusCode)
			{
				return true;
				
			}

			return false;
		}
		public bool DeleteBook(int id)
		{
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var deleteTask = client.DeleteAsync($"/Book/Delete/{id}");
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
				return true;
              
            }
			
			return false;
		}
		public AssetModel SearchBook(int id)
		{
			AssetModel asset = null;
			HelperUrl helper = new HelperUrl();
			HttpClient client = helper.Initaial();
			var responseTask = client.GetAsync("Book/Search?id=" + id.ToString());
			responseTask.Wait();

			var result = responseTask.Result;
			if (result.IsSuccessStatusCode)
			{
				var readTask = result.Content.ReadAsStringAsync().Result;
				asset = JsonConvert.DeserializeObject<AssetModel>(readTask);

			}
			return asset;
		}
		

		public bool EditBook(AssetModel book)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var putTask = client.PutAsJsonAsync<AssetModel>($"/Book/Update/{book.AssetId}", book);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
          
            }
            return false;
        }

      
    }
}
