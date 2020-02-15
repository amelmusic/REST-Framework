import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent }   from './main/main.component';
import { AuthGuard } from './core/guards/auth.guard';

const appRoutes: Routes = [
   {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full',
   },
   {	
      path: 'session',
      loadChildren:'./session/session.module#SessionModule' ,
   },
   {
      path: '',
      component: MainComponent,
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      children: [
         { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' }
      ]
   }
]

@NgModule({
  	imports: [RouterModule.forRoot(appRoutes)],
 	exports: [RouterModule],
  	providers: []
})
export class RoutingModule { }
