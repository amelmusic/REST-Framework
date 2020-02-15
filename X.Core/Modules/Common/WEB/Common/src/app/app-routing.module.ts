import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent }   from './main/main.component';
import { AuthGuard } from './shared/services/auth.guard';

const appRoutes: Routes = [
   {
      path: '',
      pathMatch: 'full',
      redirectTo: 'users/all'
   }
   ,
   { path: 'landing', component: MainComponent, loadChildren: () => import('./modules/landing/landing.module').then(m => m.LandingModule) }
   ,{ path: 'users', component: MainComponent, canActivate: [AuthGuard], loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule) },
   { path: 'codetables', component: MainComponent, canActivate: [AuthGuard], loadChildren: () => import('./modules/codetables/codetables.module').then(m => m.CodetablesModule) },
   {
      path: '**',
      redirectTo: ''
   },
]
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
