using System.Net.Http.Headers;
using System.Text.Json;

namespace GithubUserActivity 
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            await ProcessRepositoriesAsync(client);
        }

        static async Task ProcessRepositoriesAsync(HttpClient client)
        {
            await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

            foreach (var repo in repositories ?? Enumerable.Empty<Repository>())
            {
                Console.WriteLine(repo.name);
            }
        }
    }
}
