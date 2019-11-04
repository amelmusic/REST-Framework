import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryDetailsComponent } from './country-details/country-details.component';


const routes: Routes = [{
  path: '',
  children: [
     {
      path: 'country',
      component: CountryListComponent
     },
     {
      path: 'country/:id',
      component: CountryDetailsComponent
     }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class XhelloRoutingModule { }
