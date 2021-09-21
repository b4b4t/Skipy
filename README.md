# Skipy

Tool to handle database updates on a project.
Build or use the provider adapted to your project and use the Skipy cli to update your database.

### Usage

**`status`** : display the list of the updates with their status. ex: ./skipy status

**`update <ID>`** : update the database to the specified update. ex: ./skipy update UpdateID 

### Use an update provider

Place the DLL file of the provider in the Module folder at the root directory of the Skipy application.

### Make a custom provider

Create a new project and add two classes :
- a class which implements the `IModule` interface to declare a new module
- a class which implements the `IUpdateProvider`

You can find an example in the test/Skipy.Provider.Test project in the repository.

### Make a custom provider using Entity Framework Core

Create the two needed classes for a module but use the Entity Framework implementations of the interfaces `IModule` and `IUpdateProvider`, respectively `EntityFrameworkModule` and `EntityFrameworkProvider`. 
