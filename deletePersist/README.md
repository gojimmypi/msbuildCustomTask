# deletePersist

Example project for [microsoft/msbuild/issues/5486](https://github.com/microsoft/msbuild/issues/5486)

This is an example SDK project file to demonstrate how after files are created during a custom build process, they persist and 
cannot be deleted by the `clean` target, nor even manually using the DOS `del` command. No error is generated. The files _are_ 
removed, but moments later they are quietly restored.

This update on the the `tinyproject` branch demonstrates the smallest possible file that still runs automatically 
both at Visual Studio startup time, as well as whenever the `init.log` file is deleted.
