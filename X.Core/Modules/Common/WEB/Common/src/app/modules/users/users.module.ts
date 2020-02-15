import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { SharedModule } from 'app/shared/shared.module';
import { AspNetUsersListComponent } from './asp-net-users-list/asp-net-users-list.component';
import { AspNetUsersDetailsComponent } from './asp-net-users-details/asp-net-users-details.component';


@NgModule({
  declarations: [UsersComponent, AspNetUsersListComponent, AspNetUsersDetailsComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedModule
  ],
  entryComponents: [AspNetUsersDetailsComponent]
})
export class UsersModule { }
