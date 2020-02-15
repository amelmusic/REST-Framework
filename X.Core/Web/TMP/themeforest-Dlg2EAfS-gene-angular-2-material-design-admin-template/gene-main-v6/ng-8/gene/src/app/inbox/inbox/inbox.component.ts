import { Component, OnInit, OnDestroy, ViewChildren, QueryList, ElementRef, Renderer, AfterViewChecked } from '@angular/core';
import { Mail} from "./mail.model";
import { MailService} from "../../service/mail/mail.service";
import { MatDialog, MatSnackBar } from "@angular/material";
import { InboxComposeComponent} from "../../widget-component/pop-up/inbox-compose/inbox-compose.component";
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation } from "../../core/route-animation/route.animation";

@Component({
  selector: 'ms-inbox',
  templateUrl: './inbox.component.html',
  styleUrls: ['./inbox.component.scss'],
  host: {
    "[@fadeInAnimation]": 'true'
  },
  animations: [ fadeInAnimation ]
})
export class InboxComponent implements OnInit, OnDestroy, AfterViewChecked {

   length = 100;
   pageSize = 10;
   pageSizeOptions: number[] = [5, 10, 25, 100];

   shownMails: Mail[] = [ ];
   shownMailDetail: boolean;
   checked = false;

   selectedMails: Mail[] = [ ];
   selectedMail: Mail;

   respondActive: boolean;
   clickListeners: Function[] = [ ];

   @ViewChildren('mailList')
   mailList: QueryList<ElementRef>;

   constructor(
      private mailService: MailService,
      private renderer: Renderer,
      public composeDialog: MatDialog,
      private snackBar: MatSnackBar,
      private pageTitleService: PageTitleService
   ) { }

   ngOnInit() {
      this.getMessages();
      this.pageTitleService.setTitle("Inbox");
   }

   ngAfterViewChecked() {
      this.createMailListClickListeners();
   }

   /**
     * getMessages method is used to get the messages.
     */
   getMessages(): void {
      this.mailService.getMessages().then(shownMails => this.shownMails = shownMails);
   }

   /**
     * createMailListClickListeners method is used to show the mail detail.
     */
   createMailListClickListeners() {
      this.clickListeners.forEach((listener) => {
         listener();
      });

      this.mailList.forEach((elem, index) => {
         this.clickListeners.push(
            this.renderer.listen(elem.nativeElement, 'click', (event) => {
               if (event.target.className != 'mat-checkbox-inner-container' && event.target.className != 'mat-ripple-background') {
                  this.showMailDetail();
               }
            })
         );
      });
   }

   /**
     * openComposeDialog mmethod is used to open a compose box.
     */
   openComposeDialog() {
      let dialogRef = this.composeDialog.open(InboxComposeComponent);
      dialogRef.afterClosed().subscribe(result => {
         if (result) {
            this.snackBar.open(result, "close",{
            duration: 2000,
          });
         }
      });
   }

   /**
     * resetTemporaries method is used to back to the email list.
     */
   resetTemporaries() {
      this.shownMailDetail = null;
      this.respondActive = false;
   }

   /**
     * showMailDetail method is used to show the mail detail.
     */
   showMailDetail() {
      this.shownMailDetail = true;
   }

   ngOnDestroy() {
      this.clickListeners.forEach((listener) => {
         listener();
      })
   }
}
