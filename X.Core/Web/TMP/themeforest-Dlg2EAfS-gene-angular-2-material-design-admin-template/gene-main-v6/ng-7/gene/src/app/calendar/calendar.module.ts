import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { MatCardModule, MatIconModule, MatButtonModule } from '@angular/material';
import { CalendarRoutes } from './calendar.routing';
import { CalendarComponent } from './calendar/calendar.component';

@NgModule({
	declarations: [CalendarComponent],

	imports: [
		CommonModule,
		RouterModule.forChild(CalendarRoutes),
		CalendarModule.forRoot({
   		provide: DateAdapter,
   		useFactory: adapterFactory
    	}),
    	MatIconModule,
    	MatCardModule,
    	MatButtonModule
	],
	entryComponents : [
		CalendarComponent
	]
})
export class Calendar_Module { }
