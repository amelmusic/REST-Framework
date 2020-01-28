Now that dotnet new xweb has finished, run npm install and after that you can use schematics to create lists, tables, etc


Examples:

To create a new module, first navigate to src\app\modules and use:
ng g module MODULENAME --route=MODULEROUTE --routing=true --module App

If it's material based, add SharedModule as dependency in newly created one

After adding module, you can add security and host component as shown here:
{
      path: 'codetables',
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      component: MainComponent,
      loadChildren: () =>  import('./modules/codetables/codetables.module').then(m => m.CodetablesModule)
}
Make sure that your path is BEFORE path: '**', item inside app-routing.module.ts file, otherwise it wont work.


Create service:
First, navigate to src\app\services and use:
ng g @schematics/angular-xcore:s LegalEntities

Create list (table, list, card list):
First, navigate to desired module and use:
ng g @schematics/angular-xcore:c-l LegalEntitiesList


Create details (standard, dialog, tabbed):
First, navigate to desired module and use:
ng g @schematics/angular-xcore:c-d LegalEntitiesList
