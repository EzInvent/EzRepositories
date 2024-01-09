using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitHub;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

partial class Build
{
    GitHubActions GitHubActions => GitHubActions.Instance;
    [GitRepository] readonly GitRepository Repository;
    readonly string RepositoryName = "EzRepositories";

    Target ApproveRequest => _ => _
        .Requires(() => GitHubActions.IsPullRequest)
        .Requires(() => Repository.Branch.StartsWith("refs/pull/"))
        .Executes(async () =>
        {
            var owner = Repository.GetGitHubOwner();
            var pullRequestNumber = GitHubActions.PullRequestNumber;
            var token = GitHubActions.Token;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var approvalUrl = $"https://api.github.com/repos/{owner}/{RepositoryName}/pulls/{pullRequestNumber}/reviews";
            Log.Information($"Approval Token: {token}");
            var reviewData = "{\"event\": \"APPROVE\"}";
            var content = new StringContent(reviewData, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(approvalUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Log.Information($"pull request #{pullRequestNumber} approved successfully");
            }
            else
            {
                Assert.Fail($"Failed to approve pull request #{pullRequestNumber}. Status code: {response.StatusCode}");
            }
            
        });
}