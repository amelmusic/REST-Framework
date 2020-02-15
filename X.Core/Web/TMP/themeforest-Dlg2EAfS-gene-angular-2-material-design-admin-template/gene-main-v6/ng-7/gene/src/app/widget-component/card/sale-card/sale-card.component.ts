import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-sale-card',
	templateUrl: './sale-card.component.html',
	styleUrls: ['./sale-card.component.scss']
})
export class SaleCardComponent implements OnInit {

	@Input() saleCardContent : any ;
	
	constructor() { }

	ngOnInit() {
	}

}
