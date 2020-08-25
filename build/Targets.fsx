// load dependencies from source folder to allow bootstrapping
#r "/bin/Plainion.CI/FAKE/FakeLib.dll"
#load "/bin/Plainion.CI/bits/PlainionCI.fsx"

open Fake
open PlainionCI

Target "CreatePackage" (fun _ ->
    !! ( outputPath </> "*.*Tests.*" )
    ++ ( outputPath </> "*nunit*" )
    ++ ( outputPath </> "TestResult.xml" )
    ++ ( outputPath </> "Plainion.RI.*" )
    ++ ( outputPath </> "**/*.pdb" )
    |> DeleteFiles

    [
        ( projectName + ".*", Some "lib/netstandard2.0", None)
    ]
    |> PNuGet.Pack (projectRoot </> "build" </> projectName + ".Core.nuspec") (projectRoot </> "pkg")
)

Target "Deploy" (fun _ ->
    trace "Nothing to deploy"
)

Target "Publish" (fun _ ->
    PNuGet.PublishPackage (projectName + ".Core") (projectRoot </> "pkg")
)

RunTarget()
