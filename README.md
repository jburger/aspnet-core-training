# aspnet-core-training
A training session for introducing ASPNET Core concepts to my team members, to encourage them to follow through with some self paced learning on pluralsight. Slides, notes & code. 

Feel free to share and re-use. Please do log an issue if you spot any glaring issues or omissions.

## Development Pre-requisites
- dotnet core SDK 2.1.4 installed
- access to nuget.org to pull down packages
- VS Code with C# extension installed

## Topics covered

- Environment setup
  - Installing dotnet core SDK 2.1.4
  - Install VS Code
  - Creating a new solution and adding new project
    - dotnet new
    - dotnet run
    - dotnet watch run
    - Building with tasks.json
    - Debugging with launch.json
- Adding dependencies
- Adding a controller
- Dependency Injection
- Validating input
  - simple scenarios - data annotations + ModelState
  - complex scenarios - FluentValidator + ModelState
- Configuration
  - Many sources
  - Hooking up a database
  - Injection with IOptions<T>
- Response conventions
- Logging with Serilog
- Testing with xunit
  
## Environment Setup

1. Download and install .NET core SDK by following [this link](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.4) and downloading the appropriate installer for your operating system
2. Your browser tab will show some guidance on how to install the package you downloaded, follow that and confirm you can run dotnet CLI from a new shell.
```bash
$ dotnet --version
2.1.4
```
3. Install VS Code by following [this link](https://code.visualstudio.com/) and downloading the appropriate installer for your operating system
4. Make a home for your new source code repository and create some boilerplate to get started on.

The following commands will create a directory structure, create a project, and then add it to a new solution file.
```bash
mkdir -p Fun.Api/src/Fun.Api.Host
cd Fun.Api/src/Fun.Api.Host
dotnet new webapi
cd ../../
dotnet new sln
dotnet sln add src/Fun.Api.Host/Fun.Api.Host.csproj
```
To check that everything is all good, now try to build your solution:
```bash
dotnet build Fun.Api.sln
```
