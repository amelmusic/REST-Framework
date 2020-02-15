import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';
import { CoreService } from '../../service/core/core.service';

@Component({
	selector: 'ms-icon',
	templateUrl:'./icons-component.html',
	styleUrls: ['./icons-component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class MaterialIconComponent implements OnInit {

	icons : any;

	constructor(private pageTitleService: PageTitleService,
					private translate : TranslateService,
					public service : CoreService) {}

	ngOnInit() {
		this.pageTitleService.setTitle("Material Icons");
		
		this.service.getMaterialIcons().
		   subscribe( res => { this.icons = res },
                    err => console.log(err),
                    ()  => this.icons
                  );
	} 
	 
}



