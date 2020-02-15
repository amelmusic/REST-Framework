import { Component, ChangeDetectionStrategy, ViewChild, TemplateRef, OnInit ,ViewEncapsulation } from '@angular/core';
import * as moment from 'moment';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation} from "../../core/route-animation/route.animation";
import { startOfDay,
         endOfDay,
         subDays,
         addDays,
         endOfMonth,
         isSameDay,
         isSameMonth,
         addHours
      } from 'date-fns';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent,
         CalendarEventAction,
         CalendarEventTimesChangedEvent
      } from 'angular-calendar';
import { CoreService } from '../../service/core/core.service';

const colors: any = {
   red: {
      primary: '#ad2121',
      secondary: '#FAE3E3'
   },
   blue: {
      primary: '#1e90ff',
      secondary: '#D1E8FF'
   },
   yellow: {
      primary: '#e3bc08',
      secondary: '#FDF1BA'
   }
};

@Component({
   selector: 'ms-full-calendar',
   changeDetection: ChangeDetectionStrategy.OnPush,
   templateUrl:'./calendar-material.html',
   styleUrls: ['./calendar-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class CalendarComponent implements OnInit {

   activeDayIsOpen: boolean = true;
   @ViewChild('modalContent') modalContent: TemplateRef<any>;

   view: string = 'month';

   viewDate: Date = new Date();

   modalData: {
      action: string,
      event: CalendarEvent
   };

   refresh: Subject<any> = new Subject();

   /**
     * actions is used to delete the event.
     */
   actions: CalendarEventAction[] = [ {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({event}: {event: CalendarEvent}): void => {
         this.handleEvent('Edited', event);
      }
   }, {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({event}: {event: CalendarEvent}): void => {
         this.events = this.events.filter(iEvent => iEvent !== event);
         this.handleEvent('Deleted', event);
      }
   }];

  
   /**
     *events is used to view the events
     */
   events: CalendarEvent[] = [{
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 1),
      title: 'A 3 day event',
      color: colors.red,
      actions: this.actions
   }, {
      start: startOfDay(new Date()),
      title: 'An event with no end date',
      color: colors.yellow,
      actions: this.actions
   }, {
      start: subDays(endOfMonth(new Date()), 3),
      end: addDays(endOfMonth(new Date()), 3),
      title: 'A long event that spans 2 months',
      color: colors.blue
   }, {
      start: addHours(startOfDay(new Date()), 2),
      end: new Date(),
      title: 'A draggable and resizable event',
      color: colors.yellow,
      actions: this.actions,
      resizable: {
      beforeStart: true,
      afterEnd: true
      },
      draggable: true
   }];

   constructor( private modal: NgbModal, 
                private pageTitleService: PageTitleService,
                private coreService : CoreService) {}

   ngOnInit() {
      this.pageTitleService.setTitle("Calendar");
   }

   /**
     * dayClicked method is used to open the active day.
     */
   dayClicked({date, events}: {date: Date, events: CalendarEvent[]}): void {
      if (isSameMonth(date, this.viewDate)) {
         if (
            (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
            events.length === 0
         ) {
            this.activeDayIsOpen = false;
         } else {
            this.activeDayIsOpen = true;
            this.viewDate = date;
         }
      }
   }

   /**
     * eventTimesChanged method is used to change the calendar Event time.
     */   
   eventTimesChanged({event, newStart, newEnd}: CalendarEventTimesChangedEvent): void {
      event.start = newStart;
      event.end = newEnd;
      this.handleEvent('Dropped or resized', event);
      this.refresh.next();
   }

   /**
     * handleEvent method is used to handle the event and action.
     */
   handleEvent(action: string, event: CalendarEvent): void {
      this.modalData = {event, action};
      this.modal.open(this.modalContent, {size: 'lg'});
   }

   /**
     * addEvent method is used to add a new event into calendar.
     */
   addEvent(): void {
      this.events.push({
         title: 'New event',
         start: startOfDay(new Date()),
         end: endOfDay(new Date()),
         color: colors.red,
         draggable: true,
         resizable: {
           beforeStart: true,
           afterEnd: true
         }
      });
      this.refresh.next();
   } 
}



