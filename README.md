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
- Adding your own code
  - Adding dependencies
  - Centralising controller routing
  - Domain modelling
  - Dependency Injection
  - Validating input
    - simple scenarios - data annotations + ModelState
    - complex scenarios - FluentValidator
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

## Adding your own code

### Adding dependencies

Let's start by bringing in some useful libraries that we are likely to require when building out our API. We'll bring in the following nuget packages.

- FluentValidator
- Newtonsoft.Json
- EntityFramework.Core

### 3 ways to add dependencies

#### Change detection
1. csproj change detection both visual studio and vs code are smart enough to tell when you've changed your csproj file. 

In your csproj file you'll see an ItemGroup element that contains a PackageReference element. Add the following lines underneath the exsting PackageReference and watch what happens. Accept any suggestions to restore your packages

```
<PackageReference Include="FluentValidation" Version="7.5.2" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="2.4.0" />
<PackageReference Include="Serilog" Version="2.0.2" />
<PackageReference Include="Serilog.AspNetCore" Version="2.1.1" />
```
This approach is useful if you know what you want, or if you're copying from another project. You can always run 'dotnet restore' in a shell local to this file to force a restore.

Now pop open a shell. VS Code users can do this without extensions Using CTRL+<backtick> notice that has opened it in the root directory of your workspace.

Type in to the shell
```bash
cd src/Fun.Api.Host
dotnet add package Microsoft.EntityFrameworkCore
```

This will get the latest Microsoft.EntityFrameworkCore package from nuget.org. You could supply a semantic version with a --version parameter to fix the version number. You'll notice that this approach automated writing to the csproj file for us. Our package references now look like this: 

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.5" />
    <PackageReference Include="FluentValidation" Version="7.5.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="2.0.2" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="2.4.0" />
    <PackageReference Include="Serilog" Version="2.6.0" />
    <PackageReference Include="Serilog.AspNetCore" Version="2.1.1" />
  </ItemGroup>
```

Th

> You can use version wildcards. See [this article](https://docs.microsoft.com/en-us/nuget/reference/package-versioning#version-ranges-and-wildcards) for more details.

#### Visual Studio

Typically visual studio users and users of other IDE's don't typically do this, as you can right click a projects 'references' and use the GUI to find packages. However there are times when knowing the approach from first principles is important:

1. Fixing problems that are confusing because of tooling - go back to the CLI and try it there.
2. Script automation during continuous integration, or troubleshooting a CI build. 

## Centralising routing 

You'll notice that the ValuesController class that was generated in the Controllers folder of our source code, makes use of  meta-programming (Attributes) to designate the routing. This approach is fine for small jobs. 

For bigger things it can really help to have a single place to go to, to inspect the way your application performs routing.

So let's take a look at ASPNET Core [conventional routing](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1#conventional-routing) approach to building the routes for our API.

We will be modifying the UseMvc call in the Startup classes Configure method. Make your Configure method look like this one.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMvc(WithConventionalRoutes);
}

private void WithConventionalRoutes(IRouteBuilder routeBuilder)
{
    routeBuilder.MapRoute("default", "api/{controller}");
}
```
This route definition mounts any requests onto /api/ and directs the first segment of the route to a 'controller'. From here we can use HTTP Verbs in our controllers method names to do the rest.

We can also provide fine grained control of the parameters on each method on a controller, to tailor each route to the needs of our model. So now that we have this we can start thinking about the business problem we are trying to solve. 

### Domain modelling

> Domain modelling concepts can get pretty deep, so we'll build just enough to have something to work with, without going too far down the DDD rabbit hole.

Today we'll be building an API for a business that looks after Pets while their owners go on holidays. The name of the business is 'FUN' which stands for Fuzzy UNited.

To wrangle complexity, well use a Model to represent our problem domain. A 'Customer' makes a 'Booking' for one or more 'Pet's.

We are going to need to manage each of these three entities, to create, retrieve, update, delete & list them. 

A popular approach for creating these kinds of services is to use HTTP Verbs as a way to indicate to our API what kind of operation is required on our entities. The way to do this in aspnet core, is to create a Controller for each entity.

Lets start with the PetController
```csharp

    public class PetController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Pet pet)
        {
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Pet value)
        {
            return Ok();
        }

        // DELETE api/values/5
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }

```

Go ahead and make similar classes for Customer, and Booking.

This won't compile yet because we are missing a 'Pet' class. Because our controller operates with the outside world, this class forms part of our contract with any consumers of our API. 

**When building APIs we want to ensure two things:**
1. That we can provide the outside world with a stable interface (a 'data contract' if you like)
2. Have the freedom to change the implementation of that contract, without effecting consumers as much as possible

To do this we would normally make sure that our 'Domain' representation is decoupled from our 'data contract' to the outside world. In other words, we will provide two representations of the same thing: one for us, and on for our consumers. The downside is that we will have to map between these two representations, whenever we want to cross the boundary between our contract and our domain.

#### Create a data contract library
Sometimes we want to share our contract code with others, so we'll create a library to do this. Head on into our src directory and create a new folder called Fun.Api.DataContracts - then create a class library, don't forget to add it to your solution!

```
cd Fun.Api/src
mkdir Fun.Api.DataContracts && cd Fun.Api.DataContracts
dotnet new classlib 
cd ../..
dotnet sln add src/Fun.Api.DataContracts/Fun.Api.DataContracts.csproj
```

Now we need to add a reference to this project from our Host solution. This can also be done from the command line

```
dotnet add src/Fun.Api.Host/Fun.Api.Host.csproj reference src/Fun.Api.DataContracts/Fun.Api.DataContracts.csproj
```

Notice that this added some lines to our Host.csproj file?
```xml
  <ItemGroup>
    <ProjectReference Include="..\Fun.Api.DataContracts\Fun.Api.DataContracts.csproj" />
  </ItemGroup>
```
It is up to you how you choose to add references, but again, this is the first principles approach, just add it to the csproj file.

Now add these classes:

```
    public class CreateBookingRequest
    {
        string StartDate { get; set; }
        string EndDate { get; set; }
        
        int CustomerId { get; set; }
        List<int> Pets { get; set; }
    }
    
     public class CreateBookingResponse
    {
        int? Id { get; set; }
        decimal DepositRequired { get; set; }
        decimal TotalAmountOwing { get; set; }
        DateTime DueDate { get; set; }
    }
    
    public class CreateCustomerRequest
    {
        int Id { get; set; }
        string Name { get; set; }
    }
    
    public class CreatePetRequest
    {
        int Id { get; set; }
        int CustomerId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Type { get; set; }
    }
```

Let's do the same for our Domain model, instead calling it Fun.Api.Domain this time, and put the following classes in that namespace. Don't forget to reference it, and add it to your solution file.

Add these classes to the Domain Model:

```csharp
    public class Booking
    {
        int Id { get; set; }
        decimal DepositPercentage { get; set; }
        decimal TotalAmountOwing { get; set; }
        DateTime DueDate { get; set; }

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        
        Customer Customer { get; set; }
        List<Pet> Pets { get; set; }
    }
    
     public class Customer
    {
        int Id { get; set; }
        string Name { get; set; }
        List<Pet> Pets { get; set; }
    }
    
    public class Pet
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PetType Type { get; set; }
    }

    public class PetType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
```




