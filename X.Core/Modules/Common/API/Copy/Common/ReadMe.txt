Now that dotnet new xapi command has finished, you can set few options.
First, if needed change port. By default if will be 5001

Database:
	If you already have database, you can easily scaffold it usind db scaffold command:
	Scaffold-DbContext 'Data Source=localhost;Initial Catalog=Common; Integrated Security = true' Microsoft.EntityFrameworkCore.SqlServer -Context CommonContext -OutputDir Database -DataAnnotations -Force
	Please set inside Package Manager Console as startup project Common.Services project
	and set Common.API as startup project in solution explorer

	To enable migrations, you can use EF core migration commands through nuget package manager console:
		- Add-Migration InitialCreate
		- For seeding data, you can also add migration, just add code to OnModelCreating method (https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding)
		- For updating data, simply run Update-Database from visual studio
	More info: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/

Model:
	When you are done with designing DB model as classes inside Common.Services project,
	you can use dbmtool to copy these classes as model inside Common.Model project.
	This tool will automatically replace namespace.
	In order to use this tool, first, you need to install it:
	dotnet tool install --global dbmtool
	After that, you can execute command from package manager console or cli:
	dbmtool -r Common.Services\Database -d Common.Model
	You can run this tool multiple times, it will append new files if needed.

API:
	By default, every API will go through Permission filter. Default implementation will check app_data/permissions.json file in order to control access


Permissions:
- By default, permissions will be read from permission.json file. In order to have database driven permissions,
please use PermissionModule.All package and add following lines in Startup.cs
	-   var connectionPermission = Configuration.GetConnectionString("PermissionModule");
        services.AddDbContext<PermissionModuleContext>(options => options.UseSqlServer(connectionPermission));
		THIS HAS TO BE DONE BEFORE var builder = new ContainerBuilder(); line
	- builder.RegisterType<PermissionChecker>()
            .As<IPermissionChecker>()
            .InstancePerLifetimeScope()
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .EnableClassInterceptors()
            .InterceptedBy(typeof(LogInterceptorProxy))
            .InterceptedBy(typeof(CacheInterceptorProxy))
            .InterceptedBy(typeof(TransactionInterceptorProxy));
!!!!!!!!!!THIS HAS TO BE DONE BEFORE _container = builder.Build();



Development
Please check xcore hello class