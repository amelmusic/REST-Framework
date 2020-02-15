import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-recent-trade-list',
	templateUrl: './recent-trade-list.component.html',
	styleUrls: ['./recent-trade-list.component.scss']
})
export class RecentTradeListComponent implements OnInit {

	@Input() recentTradeList : any;
	
	recentTradeColumns = ['currency', 'status', 'price', 'total'];

	constructor() { }

	ngOnInit() {
	}

}
