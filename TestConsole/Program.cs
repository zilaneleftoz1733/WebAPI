using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace TestConsole

{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            await NorthwindPost();
             await NorthwindPut();
            await NorthwindGet();
            Console.WriteLine("***********Silme ****************");
            //await NorthwindDelete();
            //Console.WriteLine("*********** Get ****************");

            await NorthwindGet();

            Console.WriteLine("Hello, World!");


        }
        static async Task NorthwindDelete()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.now.sh");
            var response = await client.DeleteAsync("/api/categories/7");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Silme islemi Basarili");
            }
            Console.WriteLine(response.StatusCode);
        }
        static async Task NorthwindPut()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.now.sh");
            Category category = new Category()
            {
                Id = 61,
                description = "Test Category",
                name = "Test 1"
            };
            //var categoryJson = JsonSerializer.Serialize<Category>(category);


            var response = await client.PutAsJsonAsync("/api/categories/1", category);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Islem Basarili");
            }
            else
            {
                Console.WriteLine("Islem Başarisiz");
            }

        }
        static async Task NorthwindPost()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.now.sh");
            Category category = new Category()
            {
                Id = 61,
                description = "Test Category",
                name = "Test"
            };
            //var categoryJson = JsonSerializer.Serialize<Category>(category);


            var response = await client.PostAsJsonAsync("/api/categories", category);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.WriteLine("Islem Basarili");
            }
            else
            {
                Console.WriteLine("Islem Başarisiz");
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