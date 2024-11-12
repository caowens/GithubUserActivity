# GithubUserActivity

Github User Activity is a CLI that uses the GitHub API to fetch user activity and display it in the terminal.

## Installation

Clone the project to your local machine. See HTTPS version below.

```bash
git clone https://github.com/caowens/GithubUserActivity.git
```

## Usage

To use the cli, navigate to it's root directory and enter one of the following commands. If having issues with running, make sure you have [.NET](https://dotnet.microsoft.com/en-us/) installed.
```bash
# Basic usage
dotnet run <username>

# If wanting to use a github OAuth token for larger rate limits, can add it as a second argument.
dotnet run <username> <oauth_token>
```

You can learn more about generating a fine-grained personal access token [here](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens#creating-a-fine-grained-personal-access-token).

This project is a part of the backend developer roadmap journey on https://roadmap.sh/projects/github-user-activity.

Note: Not every Github event type is accounted for in the current version of this CLI. This project is a part of a learning journey, so I just accounted for event types that I've seen often rather than [all 16 event types](https://docs.github.com/en/rest/using-the-rest-api/github-event-types?apiVersion=2022-11-28). If the event type is not included, then the event type will be a part of the output. Feel free to make a branch and make a pull request or fork the repository to add ones you want. 
