import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-publications',
  templateUrl: './publications.component.html',
  styleUrls: ['./publications.component.scss']
})
export class PublicationsComponent implements OnInit {

	@Input() publicationArray : any;
	
	constructor(private translate : TranslateService) { }

	ngOnInit() {
	}


}
