import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { MatDialogRef, MatDialog } from "@angular/material";
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
   selector: 'ms-dialogs',
   templateUrl:'./dialog-material.html',
   styleUrls: ['./dialog-material.scss'],
   encapsulation: ViewEncapsulation.None,
   host: {
      "[@fadeInAnimation]": 'true'
   },
   animations: [ fadeInAnimation ]
})

export class DialogOverviewComponent implements OnInit{

   dialogRef : MatDialogRef<DemoDialog>;
   result    : string;

  	constructor( public dialog: MatDialog,
                private pageTitleService: PageTitleService, 
                public translate : TranslateService) { }

  	ngOnInit() {
    	this.pageTitleService.setTitle("Dialog");
   }

   dialogHTML : string = `
      <button mat-raised-button type="button" (click)="openDialog()" color="primary">Open Dialog</button>
      <p *ngIf="result">You chose: {{ result }}</p>`;

   /**
     * OpenDialog method is used to open a dialog.
     */
   openDialog() {
      this.dialogRef = this.dialog.open(DemoDialog, {
         disableClose: false
      });
      this.dialogRef.afterClosed().subscribe(result => {
         this.result = result;
         this.dialogRef = null;
      });
   }
}

@Component({
  	selector: 'ms-demo-dialog',
  	template: `
     	<h1>Would you like to order pizza?</h1>

     	<mat-dialog-actions align="end">
   	 	<button mat-button (click)="dialogRef.close('No!')">No</button>
   	 	<button mat-button color="primary" (click)="dialogRef.close('Yes!')">Yes</button>
     	</mat-dialog-actions>`
})

export class DemoDialog {
  	constructor(public dialogRef: MatDialogRef<DemoDialog>) { }
}



