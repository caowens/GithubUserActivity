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
            else if (args.Length == 2)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", args[1]);
            }
            else if (args.Length > 2)
            {
                throw new Exception("Wrong number of arguments. Please provide only 1 or 2 arguments, for username and OAuth token respectively.");
            }

            await ProcessEventsAsync(client, args[0]);
        }

        static async Task ProcessEventsAsync(HttpClient client, string username)
        {
            await using Stream stream = await client.GetStreamAsync($"https://api.github.com/users/{username}/events");
            var events = await JsonSerializer.DeserializeAsync<List<Event>>(stream);

            foreach (var e in events ?? Enumerable.Empty<Event>())
            {
                switch (e.type)
                {
                    case "PushEvent":
                        Console.WriteLine($"- Pushed {e.payload.commits?.Count} commit(s) to {e.repo.name}");
                        break;
                    case "WatchEvent":
                        Console.WriteLine($"- Starred {e.repo.name}");
                        break;
                    case "CreateEvent":
                        Console.WriteLine($"- Created a {e.payload.ref_type} in {e.repo.name}");
                        break;
                    case "IssueCommentEvent":
                        Console.WriteLine($"- {e.payload.action} the comment \"{e.payload.comment?.body}\" in {e.repo.name}");
                        break;
                    case "PullRequestEvent":
                        Console.WriteLine($"- {e.payload.action} a pull request with the title \"{e.payload.pull_request?.title}\" in {e.repo.name}");
                        break;
                    case "IssuesEvent":
                        Console.WriteLine($"- {e.payload.action} an issue in {e.repo.name}");
                        break;
                    case "PullRequestReviewCommentEvent":
                        Console.WriteLine($"- {e.payload.action} the following comment in {e.repo.name}: \"{e.payload.comment?.body}\"");
                        break;
                    case "PullRequestReviewEvent":
                        Console.WriteLine($"- Reviewed a pull request in {e.repo.name}");
                        break;
                    case "ForkEvent":
                        Console.WriteLine($"- Forked a repository in {e.payload.forkee?.full_name}");
                        break;
                    default:
                        Console.WriteLine(e.type);
                        break;
                }
            }
        }
    }
}
