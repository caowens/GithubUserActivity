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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            // # https://api.github.com/users/<username>/events
            // # Example: https://api.github.com/users/kamranahmedse/events
            
            if (args.Length < 1)
            {
                throw new Exception("Please give a username.");
            }

            await ProcessEventsAsync(client, args[0]);
        }

        static async Task ProcessEventsAsync(HttpClient client, string username)
        {
            await using Stream stream = await client.GetStreamAsync($"https://api.github.com/users/{username}/events");
            var events = await JsonSerializer.DeserializeAsync<List<Event>>(stream);

            foreach (var e in events ?? Enumerable.Empty<Event>())
            {
                Console.WriteLine(e.type);
            }
        }
    }
}
