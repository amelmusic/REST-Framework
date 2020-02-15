import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UsersComponent } from './users.component';
import { AspNetUsersListComponent } from './asp-net-users-list/asp-net-users-list.component';
import { AspNetUsersDetailsComponent } from './asp-net-users-details/asp-net-users-details.component';

const routes: Routes = [ { path: 'all', component: AspNetUsersListComponent }
                        ,{ path: 'all/:id', component: AspNetUsersDetailsComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
