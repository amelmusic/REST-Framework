import { OnInit, Component } from "@angular/core";
import { Router } from "@angular/router";


@Component({
	selector: 'ms-footer',
	templateUrl: './footer.component.html',
	styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

	constructor(public router : Router) { }

	ngOnInit() {		
	}

	onClick(){
		
	}

}
