using Nuke.Common.CI.GitHubActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[GitHubActions(
        "run-tests",
        GitHubActionsImage.MacOsLatest,
        OnPullRequestBranches = new[] { DevelopmentBranch },
        InvokedTargets = new[] { nameof(ITest.RunTests) }
        )]
partial class Build
{
}