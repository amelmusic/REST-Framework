import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HorizontalLayoutComponent } from './horizontal-layout/horizontal-layout.component';
import { MainComponent }   from './main/main.component';
import { CommingsoonComponent } from './pages/commingsoon/commingsoon.component';
import { MaintenanceComponent } from './pages/maintenance/maintenance.component';
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
      path: 'pages/comingsoon',
      component : CommingsoonComponent
   },
   {   
      path: 'pages/maintenance',
      component : MaintenanceComponent
   },
   {   
      path: 'horizontal/pages/comingsoon',
      component : CommingsoonComponent
   },
   {   
      path: 'horizontal/pages/maintenance',
      component : MaintenanceComponent
   },
   {
      path: '',
      component: MainComponent,
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      children: [
         { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
         { path: 'inbox', loadChildren : './inbox/inbox.module#InboxModule' },
         { path: 'chat', loadChildren : './chat/chat.module#ChatModule' },
         { path: 'calendar', loadChildren : './calendar/calendar.module#Calendar_Module'},
         { path: 'editor', loadChildren : './editor/editor.module#EditorModule' },
         { path: 'icons', loadChildren : './material-icons/material-icons.module#MaterialIconsModule' },
         { path: 'chart', loadChildren : './chart/chart.module#ChartModule'},
         { path: 'components', loadChildren: './components/components.module#ComponentsModule'},
         { path: 'dragndrop', loadChildren: './drag-drop/drag-drop.module#DragDropModule'},
         { path: 'tables', loadChildren:  './tables/tables.module#TablesModule'},
         { path: 'forms', loadChildren: './forms/forms.module#FormModule'},	
         { path: 'maps', loadChildren: './maps/maps.module#MapsModule' },
         { path: 'pages', loadChildren: './pages/pages.module#PagesModule' },
         { path: 'users', loadChildren: './users/users.module#UsersModule' },
         { path: 'ecommerce', loadChildren : './ecommerce/ecommerce.module#EcommerceModule'},
         { path: 'video-player', loadChildren : './video-player/video-player.module#VideoPlayerModule'},
         { path: 'taskboard', loadChildren : './task-board/task-board.module#TaskBoardModule'},
         { path: 'courses', loadChildren : './courses/courses.module#CoursesModule'},
         { path: 'user-management', loadChildren : './user-management/user-management.module#UserManagementModule'},
         { path: 'crypto', loadChildren : './crypto/crypto.module#CryptoModule'},
         { path: 'crm', loadChildren : './crm/crm.module#CrmModule'}
      ]
   },
   {
      path: 'horizontal',
      component: HorizontalLayoutComponent,
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      children: [
         { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
         { path: 'inbox', loadChildren : './inbox/inbox.module#InboxModule' },
         { path: 'chat', loadChildren : './chat/chat.module#ChatModule' },
         { path: 'calendar', loadChildren : './calendar/calendar.module#Calendar_Module'},
         { path: 'editor', loadChildren : './editor/editor.module#EditorModule' },
         { path: 'icons', loadChildren : './material-icons/material-icons.module#MaterialIconsModule' },
         { path: 'chart', loadChildren : './chart/chart.module#ChartModule'},
         { path: 'components', loadChildren: './components/components.module#ComponentsModule'},
         { path: 'dragndrop', loadChildren: './drag-drop/drag-drop.module#DragDropModule'},
         { path: 'tables', loadChildren:  './tables/tables.module#TablesModule'},
         { path: 'forms', loadChildren: './forms/forms.module#FormModule'},   
         { path: 'maps', loadChildren: './maps/maps.module#MapsModule' },
         { path: 'pages', loadChildren: './pages/pages.module#PagesModule' },
         { path: 'users', loadChildren: './users/users.module#UsersModule' },
         { path: 'ecommerce', loadChildren : './ecommerce/ecommerce.module#EcommerceModule'},
         { path: 'video-player', loadChildren : './video-player/video-player.module#VideoPlayerModule'},
         { path: 'taskboard', loadChildren : './task-board/task-board.module#TaskBoardModule'},
         { path: 'courses', loadChildren : './courses/courses.module#CoursesModule'},
         { path: 'user-management', loadChildren : './user-management/user-management.module#UserManagementModule'},
         { path: 'crypto', loadChildren : './crypto/crypto.module#CryptoModule'},
         { path: 'crm', loadChildren : './crm/crm.module#CrmModule'}
      ]
   }
]

@NgModule({
  	imports: [RouterModule.forRoot(appRoutes)],
 	exports: [RouterModule],
  	providers: []
})
export class RoutingModule { }
