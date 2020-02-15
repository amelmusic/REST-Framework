import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TemplateListComponent } from './template-list/template-list.component';
import { TemplateDetailsComponent } from './template-details/template-details.component';
import { EmailListComponent } from './email-list/email-list.component';
import { EmailDetailsComponent } from './email-details/email-details.component';
import { StaticDataListComponent } from './static-data-list/static-data-list.component';
import { StaticDataDetailsComponent } from './static-data-details/static-data-details.component';

const routes: Routes = [
{ path: 'template', component: TemplateListComponent },
{ path: 'template/:id', component: TemplateDetailsComponent },
{ path: 'emails', component: EmailListComponent },
{ path: 'emails/:id', component: EmailDetailsComponent },
{ path: 'static-data', component: StaticDataListComponent },
{ path: 'static-data/:id', component: StaticDataDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CodetablesRoutingModule { }
