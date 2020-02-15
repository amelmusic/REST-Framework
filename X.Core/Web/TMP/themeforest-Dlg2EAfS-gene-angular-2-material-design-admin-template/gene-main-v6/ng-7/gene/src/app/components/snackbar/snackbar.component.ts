import { Component, ViewEncapsulation, ChangeDetectionStrategy, OnInit} from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
	// moduleId: module.id,
	selector: 'snackbar-material',
	templateUrl:'./snackbar-material.html',
	encapsulation: ViewEncapsulation.None,
  host: {
      "[@fadeInAnimation]": 'true'
  },
  animations: [ fadeInAnimation ],
	changeDetection: ChangeDetectionStrategy.Default
})
export class SnackbarOverviewComponent implements OnInit{
  	
   constructor( public snackBar: MatSnackBar, 
                private pageTitleService: PageTitleService, 
                private translate : TranslateService) {}

  	ngOnInit() {
 	   this.pageTitleService.setTitle("Snackbar");
	}

   /**
     * openSnackBar method is used to open a snack bar.
     */
  	openSnackBar() {
    	this.snackBar.open("Showing Snack", "Close",{
        duration : 2000
      });
	}

}


