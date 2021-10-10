# Skipy

                 (`-').-> <-.(`-')    _       _  (`-')            
                 ( OO)_    __( OO)   (_)      \-.(OO )      .->   
                (_)--\_)  '-'. ,--.  ,-(`-')  _.'    \  ,--.'  ,-.
                /    _ /  |  .'   /  | ( OO) (_...--'' (`-')'.'  /
                \_..`--.  |      /)  |  |  ) |  |_.' | (OO \    / 
                .-._)   \ |  .   '  (|  |_/  |  .___.'  |  /   /) 
                \       / |  |\   \  |  |'-> |  |       `-/   /`  
                 `-----'  `--' '--'  `--'    `--'         `--'   

Skipy is a tool to handle database updates on a project.

You can build or use the provider adapted to your project and use the Skipy cli to update your database.

### CLI usage


**`status`** : display the list of the updates with their status. ex: `./skipy status` or `skipy.exe status`

**`update <ID>`** : update the database by selecting an update. ex: `./skipy update` or `skipy.exe update`

**`update -Id <ID>`** : update the database to the specified update. ex: `./skipy update -Id 20210827163757` or `skipy.exe update -Id 20210827163757`

**`update -Name <Name>`** : update the database to the specified update. ex: `./skipy update -Name MyUpdateName` or `skipy.exe update -Name MyUpdateName`

### How to create a Skipy console for a project


- Create a new application console project
- Include the `Skipy` nuget and the `Skipy.Core` nuget
- Add a new module class which implements the `IModule` interface or use directly the `Module<MyUpdateProvider>` class (the class MyUpdateProvider must implement the interface `IUpdateProvider`)
- In the `Main` method of your application, add the following lines to call the Skipy console :

```C#
public class Program
{
    static void Main(string[] args)
    {
        // Create a new console
        var console = new SkipyConsole();

        // Add the module
        console.AddProvider<Module<MyUpdateProvider>>();

        // Start the console
        console.Start(args);
    }
}
```

### Make a custom provider


Create a new project and add two classes :

- a class which implements the `IUpdateProvider` to handle the database updates

- a class which implements the `IModule` interface to declare a new module in the console. 

Example (with the Entity Framework provider available in the `Skipy.EntityFramework.Provider` nuget) :

```C#

public class MyModule : Module<EntityFrameworkProvider<MyDbContext>>
{
    public override void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        base.ConfigureServices(serviceCollection, configuration);

        // Database connection
        serviceCollection.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Update provider
        serviceCollection.AddScoped<IUpdateProvider, EntityFrameworkProvider<MyDbContext>>();
    }
}
```
