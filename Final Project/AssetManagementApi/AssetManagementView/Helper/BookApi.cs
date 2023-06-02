 namespace AssetManagementView.Helper
{
    public class BookApi
    {
        public HttpClient Initaial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7188/");
            return Client;
        }
    }
}
