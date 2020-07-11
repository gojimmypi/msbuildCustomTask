This example was created as a supplement to [msbuild issue #5486: NET.Sdk build runs unexpectedly](https://github.com/microsoft/msbuild/issues/5486)

Open the [relentlessTask Project](./relentlessTask.csproj) in Visual Studio 2017 or 2019.

Note the `init.log` file is created immediately, even before build.

Delete the `init.log` file. Note how it comes back.

If you see an error:
```
Assets file project.assets.json not found. Run a NuGet package restore
```

From `Tools > NuGet Package Manager > Package Manager Console`:
```
dotnet restore
```
Thank you [stackoverflow](https://stackoverflow.com/questions/48440223/assets-file-project-assets-json-not-found-run-a-nuget-package-restore)

