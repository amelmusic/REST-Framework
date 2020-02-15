import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-ticker-slider',
  templateUrl: './ticker-slider.component.html',
  styleUrls: ['./ticker-slider.component.scss']
})
export class TickerSliderComponent implements OnInit {

	@Input() sliderContent : any;
	@Input() sliderConfig  : any;

	constructor() { }

	ngOnInit() {
	}

}
