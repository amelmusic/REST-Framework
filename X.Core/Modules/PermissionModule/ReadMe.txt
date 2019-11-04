Now that dotnet new xapi command has finished, you can set few options.
First, if needed change port. By default if will be 5001

Database:
	If you already have database, you can easily scaffold it usind db scaffold command:
	Scaffold-DbContext 'Data Source=localhost;Initial Catalog=Core360_IdentityManager; Integrated Security = true' Microsoft.EntityFrameworkCore.SqlServer -Context PermissionModuleContext -DataAnnotations -Force -Tables permission.Permission, permission.PermissionGroup, permission.Role, permission.RolePermission, permission.RoleRelations, permission.RoleType
	Please set inside Package Manager Console as startup project [PermissionModule].Services project

	To enable migrations, you can use EF core migration commands through nuget package manager console:
		- Add-Migration InitialCreate
		- For seeding data, you can also add migration, just add code to OnModelCreating method (https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding)
	More info: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/
API:
	By default, every API will go through Permission filter. Default implementation will check app_data/permissions.json file in order to control access
