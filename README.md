# aspnet-core-training
A training session for introducing ASPNET Core concepts. I built this for my team, but I thought I would share with the world, since it is pretty generic stuff. Slides, notes & code. Enjoy. 

Feel free to share and re-use, re-mix, whatever... No attribution necessary. Please do log an issue if you spot any glaring issues or omissions. 

If you used this for your own group, I'd love to hear about it! Tweet me: @burgomg

## Development Pre-requisites
- dotnet core SDK 2.1.4 installed
- access to nuget.org to pull down packages
- VS Code with C# extension installed

## Assumptions
- You are already comfortable with web development concepts
- You can build things with C#
- You're comfortable with using relational databases and relational modeling

### Get up to speed
If you think these assumptions mean that you can't participate with this tutorial, I would suggest some of the following courses to get up to speed. You might not need to watch to all of them. Microsoft's [Visual Studio Dev Essentials](https://www.visualstudio.com/dev-essentials/) gives you free 3 months worth of pluralsight!!

- [C# 6 from scratch - Jesse Liberty](https://app.pluralsight.com/library/courses/csharp-6-from-scratch/table-of-contents)
- [EF Core Getting Started - Julie Lerman](https://app.pluralsight.com/library/courses/entity-framework-core-getting-started/table-of-contents)

By the time you've finished those you should be well on your way.

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
### Installation of pre-requisites

- Download and install .NET core SDK by following [this link](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.4) and downloading the appropriate installer for your operating system
- Your browser tab will show some guidance on how to install the package you downloaded, follow that and confirm you can run dotnet CLI from a new shell.
```bash
$ dotnet --version
2.1.4
```
- Install VS Code by following [this link](https://code.visualstudio.com/) and downloading the appropriate installer for your operating system

### Solutions and projects
- Make a home for your new source code repository and create some boilerplate to get started on.

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

> The following is specific for non Visual Studio Users, and isn't necessary if you use Visual Studio to open the solution file.

### VS Code - Building with tasks.json
- Open your new solution in VS Code and use Ctrl+Shift+B (OSX: Cmd+Shift+B) to begin the process of setting this workspace up for building. 
- You'll notice a little window drops down from the top of the interface, with a single item informing you that no build task was found. 
- Hit enter to configure a build task. 
- Hit enter again to create a tasks.json file from a Template. 
- Finally, select .NET Core from the selector and hit Enter. This will create the file for you that is preconfigured to work.
    - You can modify this later if you nede to do something special.
- Use Ctrl+Shift+B (OSX: Cmd+Shift+B) again to build your solution.

### VS Code - Debugging with launch.json
- Click on the Bug icon on the left of screen to open the debugging window
- Click the selector to create a launch.json
- This will pop a new tab and immediately give you a choice. Choose '.NET: Launch a local .NET Core App'
- Now open Startup.cs and set a breakpoint in the constructor.
- Fit F5 to begin a debugging session

> To Run without the debugger - use Ctrl+F5

You're now all set - your default browser will open up localhost for you on the correct port.

### Running a watch 

Running a ['watch'](https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch?view=aspnetcore-2.1) is useful when you are making a lot of changes and you need to see things evolve, but you would like to automate the tedium of restarting your web server everytime you make a change. 

If you change a code file, the 'watch' will detect the changes and kick off a re-build, and restart your web server for you. Super handy.

Open your csproj file, find the ItemGroup XML element that has a DotNetCliToolReference element in it. Add this line underneath that:
```xml
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
```

Your complete csproj file should look a little bit like mine:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.5" />
  </ItemGroup>

  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.2" />
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
  </ItemGroup>
</Project>
```
Save it down and then pop a shell to begin watching your Host project:

```bash
cd src/Fun.Api.Host
dotnet restore
dotnet watch run
```
