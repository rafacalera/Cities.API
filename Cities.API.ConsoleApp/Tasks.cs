using Cities.API.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cities.API.ConsoleApp
{
    internal partial class Tasks
    {

        public Tasks(HttpClient sharedClient)
        {
            SharedClient = sharedClient;
        }

        private HttpClient SharedClient { get; set; }


        public async Task GetAll()
        {
            using HttpResponseMessage response = await SharedClient.GetAsync("Cities");

            response.EnsureSuccessStatusCode();

            // string responseContent = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(responseContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"\n{jsonResponse}\n");
        }

        public async Task GetById()
        {
            Console.WriteLine("Digite o id da cidade:");
            int id = int.Parse(Console.ReadLine().Trim());

            using HttpResponseMessage response = await SharedClient.GetAsync($"Cities/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"{id}: {jsonResponse}");
        }

        public async Task Add()
        {
            Console.WriteLine("Digite o nome da cidade:");
            string city = Console.ReadLine();

            Console.WriteLine("Digite o nome do estado:");
            string state = Console.ReadLine();

            var body = JsonSerializer.Serialize(new City(city, state));
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await SharedClient.PostAsync("Cities", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"\nCriado com suscesso: {jsonResponse}\nLocation: {response.Headers.Location}");
        }

        public async Task Update()
        {
            Console.WriteLine("Digite id da cidade a ser alterada:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o nova cidade:");
            string city = Console.ReadLine();

            Console.WriteLine("Digite o novo do estado:");
            string state = Console.ReadLine();

            var body = JsonSerializer.Serialize(new City(city, state));
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await SharedClient.PutAsync($"Cities/{id}", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"\nCidade de id {id} alterada com sucesso!\n{jsonResponse}");
        }

        public async Task Delete()
        {
            Console.WriteLine("Digite o id da cidade a ser excluída dos registros:");
            int id = int.Parse(Console.ReadLine());

            using HttpResponseMessage response = await SharedClient.DeleteAsync($"Cities/{id}");
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"\nCidade de id {id} excluída com sucesso.");

        }
    }
}
