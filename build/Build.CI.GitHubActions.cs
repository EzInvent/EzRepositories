using Nuke.Common.CI.GitHubActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[GitHubActions(
        "run-tests",
        GitHubActionsImage.MacOsLatest,
        OnPushBranches = new[] { MasterBranch},
        OnPullRequestBranches = new[] { DevelopmentBranch },
        InvokedTargets = new[] { nameof(RunTests) }
        )]
[GitHubActions(
        "approve-pull",
        GitHubActionsImage.UbuntuLatest,
        OnPullRequestBranches = new[] { DevelopmentBranch, MasterBranch },
        InvokedTargets = new[] { nameof(ApproveRequest) },
        EnableGitHubToken = true
        )]
[GitHubActions(
    "close-issue",
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] { DevelopmentBranch},
    InvokedTargets = new[] { nameof(CloseIssueOnDevPush) },
    EnableGitHubToken = true
    )]
partial class Build
{
}