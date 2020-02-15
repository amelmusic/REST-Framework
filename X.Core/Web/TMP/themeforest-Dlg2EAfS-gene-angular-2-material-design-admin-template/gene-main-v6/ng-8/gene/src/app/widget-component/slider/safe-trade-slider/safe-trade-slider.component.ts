import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-safe-trade-slider',
	templateUrl: './safe-trade-slider.component.html',
	styleUrls: ['./safe-trade-slider.component.scss']
})
export class SafeTradeSliderComponent implements OnInit {

	@Input() safeTradeContent : any;
	@Input() safeTradeConfig : any;
	
	constructor() { }

	ngOnInit() {
	}

}
