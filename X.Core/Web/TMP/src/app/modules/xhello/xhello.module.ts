import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { XhelloRoutingModule } from './xhello-routing.module';
import { SharedModule } from 'app/shared/shared.module';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryDetailsComponent } from './country-details/country-details.component';



@NgModule({
  declarations: [CountryListComponent, CountryDetailsComponent],
  imports: [
    CommonModule,
    XhelloRoutingModule,
    SharedModule
  ],
  entryComponents: [CountryDetailsComponent]
})
export class XhelloModule { }
