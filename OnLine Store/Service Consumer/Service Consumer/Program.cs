using System.Net;
using System.Net.Http.Json;
using Service_Consumer.Models;

namespace Service_Consumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpRequestHeader header = new HttpRequestHeader();
            Catg category =  await client.GetFromJsonAsync<Catg>("https://localhost:44308/api/Category/3");
            Console.WriteLine($"ID: {category.Id}  Name: {category.Name}");
        }
    }
}
