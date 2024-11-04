using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace TestConsole

{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await NorthwindGet();

            Console.WriteLine("Hello, World!");


        }
        static async Task NorthwindPost()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.now.sh");
            Category category = new Category()
            {
                Id = 20,
                description = "Test Category",
                name = "Test"
            };
            var categoryJson = JsonSerializer.Serialize<Category>(category);


            var response = await client.PostAsJsonAsync("/api/categories", categoryJson);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.WriteLine("Islem Basarili");
            }

        }
        static async Task NorthwindGet()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.now.sh");
            var response = await client.GetAsync("/api/categories");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var contentString = await content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(contentString);
                foreach (var item in categories)
                {
                    Console.WriteLine(item.Id + " " + item.name + " " + item.description);
                }
            }
        }
    }
}
