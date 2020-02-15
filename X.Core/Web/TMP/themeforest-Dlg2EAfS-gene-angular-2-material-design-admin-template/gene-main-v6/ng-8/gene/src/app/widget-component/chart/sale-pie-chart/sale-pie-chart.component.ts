import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'ms-sale-pie-chart',
	templateUrl: './sale-pie-chart.component.html',
	styleUrls: ['./sale-pie-chart.component.scss']
})
export class SalePieChartComponent implements OnInit {

	pieChartOptions = {
		tooltip : {
			trigger: 'item',
			formatter: "{a} <br/>{b} : {c} ({d}%)"
	   },
		legend: {
			bottom: 10,
        	left: 'center',
        	x : 'center',
        	y : 'bottom'
	    },
		series : [
			{
				type: 'pie',
				radius : '75%',
				center: ['50%', '50%'],
				selectedMode: 'single',
				itemStyle : {
					normal : {
						label : {
							show : false
						},
						labelLine : {
							show : false
						}
					}
				},
				data:[
					{
						label: {
							normal: {
								backgroundColor: '#eee',
								borderColor: '',
								borderWidth: 1,
								borderRadius: 4
							}
						}
					},
					{value:535, name: 'Product 1', itemStyle: {color: '#67b7dc'}},
					{value:510, name: 'Product 2', itemStyle: {color: '#8067dc'}},
					{value:634, name: 'Product 3', itemStyle: {color: '#6771dc'}},
					{value:735, name: 'Product 4', itemStyle: {color: '#6794dc'}}
				]
			}
		]
	};

	constructor() { }

	ngOnInit() {
		
	}

}
