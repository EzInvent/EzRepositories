using Nuke.Common.IO;
using Nuke.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nuke.Common.Tools.DotNet;
using Serilog;

public interface ITest {
    
    Target RunTests { get; }
}
partial class Build : ITest
{
    public Target RunTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            Log.Information(TestProject);
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(TestProject)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetConfiguration(Configuration.Debug));
        });

    AbsolutePath TestProject => SourceDirectory / "EzRepositories.Tests" / "EzRepositories.Tests.csproj";
}
