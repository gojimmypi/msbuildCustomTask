This example was created as a supplement to [msbuild issue #5486: NET.Sdk build runs unexpectedly](https://github.com/microsoft/msbuild/issues/5486)

Here, we note the [comment](https://github.com/dotnet/msbuild/issues/5486#issuecomment-655622281) regarding [design-time builds](https://github.com/dotnet/project-system/blob/master/docs/design-time-builds.md#designing-targets-for-use-in-design-time-builds).
This `noDesignTimeBuild` is an example of *not* relentlessing running. (e.g. the `init.log` is not found at startup time)

```
	<Target Name="AddAdditionalReferences" BeforeTargets="ResolveAssemblyReferences">
		<PropertyGroup Condition="'$(DesignTimeBuild)' == 'true' OR '$(BuildingProject)' != 'true'">
			<_AvoidExpensiveCalculation>true</_AvoidExpensiveCalculation>
		</PropertyGroup>
	</Target>	
	
	<Target Name="RelentlessCommand" BeforeTargets="CoreCompile" Condition=" $(_AvoidExpensiveCalculation)=='' ">
		<Exec Command="echo wow $(DesignTimeBuild) &gt; init.log">
		</Exec>
	</Target>
```

Note similar [relentlessTask Project](../relentlessTask/relentlessTask.csproj) in Visual Studio 2017 or 2019 will create the `init.log` file immediately, even before build.

Delete the `init.log` file in `relentlessTask`. Note how it comes back. Build this `noDesignTimeBuild`. Note the `init.log` file. Delete the `init.log`. It does _not_ come back automatically.

If you see an error:
```
Assets file project.assets.json not found. Run a NuGet package restore
```

From `Tools > NuGet Package Manager > Package Manager Console`:
```
dotnet restore
```
Thank you [stackoverflow](https://stackoverflow.com/questions/48440223/assets-file-project-assets-json-not-found-run-a-nuget-package-restore)

