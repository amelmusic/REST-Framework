import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-server-card',
  templateUrl: './server-card.component.html',
  styleUrls: ['./server-card.component.scss']
})
export class ServerCardComponent implements OnInit {

	@Input() serverCard : any;

	constructor() { }

	ngOnInit() {
	}

}
