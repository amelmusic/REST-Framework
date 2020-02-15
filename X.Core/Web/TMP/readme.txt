Now that dotnet new xweb has finished, run npm install and after that you can use schematics to create lists, tables, etc


Examples:

Create module
ng g m LegalEntities --route legal --routing true --module app.module  

Create service:
ng g @schematics/angular-xcore:s LegalEntities

Create list
ng g @schematics/angular-xcore:c-tbl LegalEntitiesList 

