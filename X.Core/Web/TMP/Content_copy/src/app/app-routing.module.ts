import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent }   from './main/main.component';
import { AuthGuard } from './shared/services/auth.guard';

const appRoutes: Routes = [
   {
      path: '',
      pathMatch: 'full',
      redirectTo: 'landing'
   }
   //#if (!excludeDummyModule)
   ,{
      path: '',
      component: MainComponent,
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      children: [
         {  path: 'xcore', loadChildren: () => import('./modules/xhello/xhello.module').then(m => m.XhelloModule)}
      ]
   }
   //#endif
   ,
   { path: 'landing', component: MainComponent, loadChildren: () => import('./modules/landing/landing.module').then(m => m.LandingModule) }
   ,
   {
      path: '**',
      redirectTo: ''
   }]
//Example: how to add module
/*,{
      path: 'codetables',
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      component: MainComponent,
      loadChildren: () =>  import('./modules/codetables/codetables.module').then(m => m.CodetablesModule)
   } */

@NgModule({
  	imports: [RouterModule.forRoot(appRoutes)],
 	exports: [RouterModule],
  	providers: []
})
export class RoutingModule { }
