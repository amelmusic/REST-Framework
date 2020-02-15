import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CodetablesRoutingModule } from './codetables-routing.module';
import { CodetablesComponent } from './codetables.component';
import { TemplateListComponent } from './template-list/template-list.component';
import { SharedModule } from 'app/shared/shared.module';
import { TemplateDetailsComponent } from './template-details/template-details.component';
import { EmailListComponent } from './email-list/email-list.component';
import { EmailDetailsComponent } from './email-details/email-details.component';
import { StaticDataListComponent } from './static-data-list/static-data-list.component';
import { StaticDataDetailsComponent } from './static-data-details/static-data-details.component';
import { MonacoEditorModule } from 'ngx-monaco-editor';


@NgModule({
  declarations: [CodetablesComponent, TemplateListComponent, TemplateDetailsComponent, EmailListComponent, EmailDetailsComponent, StaticDataListComponent, StaticDataDetailsComponent],
  imports: [
    CommonModule,
    CodetablesRoutingModule,
    MonacoEditorModule,
    SharedModule
  ],
  entryComponents: [TemplateDetailsComponent, EmailDetailsComponent, StaticDataDetailsComponent]
})
export class CodetablesModule { }
