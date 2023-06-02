using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Helper
{
	public class UserHelper
	{
		public async Task<List<BookModel>> GetBook()
		{
			HelperUrl helper = new HelperUrl();
			List<BookModel> books = new List<BookModel>();

			HttpClient client = helper.Initaial();

			HttpResponseMessage res = await client.GetAsync("/Book/GetAll");

			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				books = JsonConvert.DeserializeObject<List<BookModel>>(result);



			}
			return (books);
		}


		public async Task<List<HardwareModel>> GetHardware()
		{
			HelperUrl helper = new HelperUrl();
			List<HardwareModel> hardwares = new List<HardwareModel>();
			HttpClient client = helper.Initaial();

			HttpResponseMessage res = await client.GetAsync("/Hardware/GetAll");

			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				hardwares = JsonConvert.DeserializeObject<List<HardwareModel>>(result);
			}
			return (hardwares);
		}

		public async Task<List<SoftwareModel>> GetSoftware()
		{
			HelperUrl helper = new HelperUrl();
			List<SoftwareModel> softwares = new List<SoftwareModel>();
			HttpClient client = helper.Initaial();

			HttpResponseMessage res = await client.GetAsync("/Software/GetAll");

			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				softwares = JsonConvert.DeserializeObject<List<SoftwareModel>>(result);
			}
			return (softwares);
		}
		public List<string> GetBookCategory()
		{

/*
			HelperUrl helper = new HelperUrl();
			List<BookModel> books = new List<BookModel>();

			HttpClient client = helper.Initaial();

			HttpResponseMessage res = await client.GetAsync("/Book/GetAll");

			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				books = JsonConvert.DeserializeObject<List<BookModel>>(result);



			}
*/


			List<string> bookCategory = new List<string>();
			List<BookModel> books = GetBook().Result;
			foreach (var i in books)
			{
				if (!bookCategory.Contains(i.Genre))
				{
					bookCategory.Add(i.Genre);
				}

			}
			return bookCategory;
		}

		public List<string> GetHardwareCategory()
		{
			List<string> HardwareCategory = new List<string>();
			List<HardwareModel> hardwares = GetHardware().Result;
			foreach (var i in hardwares)
			{
				if (!HardwareCategory.Contains(i.Category))
				{
					HardwareCategory.Add(i.Category);
				}

			}
			return HardwareCategory;
		}


		public List<string> GetSoftwareCategory()
		{
			List<string> softwareCategory = new List<string>();
			List<SoftwareModel> softwares = GetSoftware().Result;
			foreach (var i in softwares)
			{
				if (!softwareCategory.Contains(i.TypeOfSoftware))
				{
					softwareCategory.Add(i.TypeOfSoftware);
				}

			}
			return softwareCategory;
		}
	}
}
