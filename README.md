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
