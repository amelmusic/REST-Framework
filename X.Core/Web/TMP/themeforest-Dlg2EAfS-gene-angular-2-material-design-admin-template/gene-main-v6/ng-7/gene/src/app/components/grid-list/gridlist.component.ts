import { Component, OnInit,ViewEncapsulation }      from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'ms-grid',
	templateUrl:'./gridlist-material.html',
	styleUrls: ['./gridlist-material.scss'],
	encapsulation: ViewEncapsulation.None,
	host: {
		"[@fadeInAnimation]": 'true'
	},
	animations: [ fadeInAnimation ]
})

export class GridListComponent implements OnInit{

	fixedCols      = 4;
 	fixedRowHeight = 200;
  	ratio          = '4:1';

	tiles: any[] = [{
	 	text: 'One',
	 	cols: 3,
	 	rows: 1,
	 	color: '#1565c0'
 	}, {
	 	text: 'Two',
	 	cols: 1,
	 	rows: 2,
	 	color: '#e53935'
  	}, {
	 	text: 'Three',
	 	cols: 1,
	 	rows: 1,
	 	color: '#0097a7'
  	}, {
	 	text: 'Four',
	 	cols: 2,
	 	rows: 1,
	 	color: '#4caf50'
  	}];

	constructor( private pageTitleService: PageTitleService, 
					 public translate : TranslateService) {}

	ngOnInit() {
		this.pageTitleService.setTitle("Grid");
	}
}


