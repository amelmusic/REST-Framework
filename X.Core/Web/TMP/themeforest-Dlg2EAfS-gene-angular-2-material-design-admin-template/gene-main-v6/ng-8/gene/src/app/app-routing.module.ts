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
      loadChildren: () => import('./session/session.module').then(m => m.SessionModule)
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
         {  path: 'dashboard', loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)},
         {  path: 'inbox', loadChildren: () => import('./inbox/inbox.module').then(m => m.InboxModule)},
         {  path: 'chat', loadChildren: () => import('./chat/chat.module').then(m => m.ChatModule)},
         {  path: 'calendar', loadChildren: () => import('./calendar/calendar.module').then(m => m.Calendar_Module)},
         {  path: 'editor', loadChildren: () => import('./editor/editor.module').then(m => m.EditorModule)},
         {  path: 'icons', loadChildren: () => import('./material-icons/material-icons.module').then(m => m.MaterialIconsModule)},
         {  path: 'chart', loadChildren: () => import('./chart/chart.module').then(m => m.ChartModule)},
         {  path: 'components', loadChildren: () => import('./components/components.module').then(m => m.ComponentsModule)},
         {  path: 'dragndrop', loadChildren: () => import('./drag-drop/drag-drop.module').then(m => m.DragDropModule)},
         {  path: 'tables', loadChildren: () => import('./tables/tables.module').then(m => m.TablesModule)},
         {  path: 'forms', loadChildren: () => import('./forms/forms.module').then(m => m.FormModule)},
         {  path: 'maps', loadChildren: () => import('./maps/maps.module').then(m => m.MapsModule)},
         {  path: 'pages', loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule)},
         {  path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule)},
         {  path: 'ecommerce', loadChildren: () => import('./ecommerce/ecommerce.module').then(m => m.EcommerceModule)},
         {  path: 'video-player', loadChildren: () => import('./video-player/video-player.module').then(m => m.VideoPlayerModule)},
         {  path: 'taskboard', loadChildren: () => import('./task-board/task-board.module').then(m => m.TaskBoardModule)},
         {  path: 'courses', loadChildren: () => import('./courses/courses.module').then(m => m.CoursesModule)},
         {  path: 'user-management', loadChildren: () => import('./user-management/user-management.module').then(m => m.UserManagementModule)},
         {  path: 'crypto', loadChildren: () => import('./crypto/crypto.module').then(m => m.CryptoModule)},
         {  path: 'crm', loadChildren: () => import('./crm/crm.module').then(m => m.CrmModule)}
      ]
   },
   {
      path: 'horizontal',
      component: HorizontalLayoutComponent,
      canActivate: [AuthGuard],
      runGuardsAndResolvers: 'always',
      children: [
         {  path: 'dashboard', loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)},
         {  path: 'inbox', loadChildren: () => import('./inbox/inbox.module').then(m => m.InboxModule)},
         {  path: 'chat', loadChildren: () => import('./chat/chat.module').then(m => m.ChatModule)},
         {  path: 'calendar', loadChildren: () => import('./calendar/calendar.module').then(m => m.Calendar_Module)},
         {  path: 'editor', loadChildren: () => import('./editor/editor.module').then(m => m.EditorModule)},
         {  path: 'icons', loadChildren: () => import('./material-icons/material-icons.module').then(m => m.MaterialIconsModule)},
         {  path: 'chart', loadChildren: () => import('./chart/chart.module').then(m => m.ChartModule)},
         {  path: 'components', loadChildren: () => import('./components/components.module').then(m => m.ComponentsModule)},
         {  path: 'dragndrop', loadChildren: () => import('./drag-drop/drag-drop.module').then(m => m.DragDropModule)},
         {  path: 'tables', loadChildren: () => import('./tables/tables.module').then(m => m.TablesModule)},
         {  path: 'forms', loadChildren: () => import('./forms/forms.module').then(m => m.FormModule)},
         {  path: 'maps', loadChildren: () => import('./maps/maps.module').then(m => m.MapsModule)},
         {  path: 'pages', loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule)},
         {  path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule)},
         {  path: 'ecommerce', loadChildren: () => import('./ecommerce/ecommerce.module').then(m => m.EcommerceModule)},
         {  path: 'video-player', loadChildren: () => import('./video-player/video-player.module').then(m => m.VideoPlayerModule)},
         {  path: 'taskboard', loadChildren: () => import('./task-board/task-board.module').then(m => m.TaskBoardModule)},
         {  path: 'courses', loadChildren: () => import('./courses/courses.module').then(m => m.CoursesModule)},
         {  path: 'user-management', loadChildren: () => import('./user-management/user-management.module').then(m => m.UserManagementModule)},
         {  path: 'crypto', loadChildren: () => import('./crypto/crypto.module').then(m => m.CryptoModule)},
         {  path: 'crm', loadChildren: () => import('./crm/crm.module').then(m => m.CrmModule)}
      ]
   },
   {
      path: '**',
      redirectTo: 'session/loginV2'
   }
]

@NgModule({
  	imports: [RouterModule.forRoot(appRoutes)],
 	exports: [RouterModule],
  	providers: []
})
export class RoutingModule { }
