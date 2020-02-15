import { Component, OnInit, Input } from '@angular/core';
/* Imports */
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
/* Chart code */
// Themes begin
am4core.useTheme(am4themes_animated);
// Themes end

@Component({
	selector: 'ms-amcharts-pie-chart',
	templateUrl: './amcharts-pie-chart.component.html',
	styleUrls: ['./amcharts-pie-chart.component.scss']
})
export class AmchartsPieChartComponent implements OnInit {

	@Input() data : any;

	constructor() { }

	ngOnInit() {
		setTimeout(() =>{
			// Create chart instance
			let chart = am4core.create("chartdiv", am4charts.PieChart);

			// Add and configure Series
			let pieSeries = chart.series.push(new am4charts.PieSeries());
			pieSeries.dataFields.value = "value";
			pieSeries.dataFields.category = "title";

			// Let's cut a hole in our Pie chart the size of 30% the radius
			chart.innerRadius = am4core.percent(35);

			// Put a thick white border around each Slice
			pieSeries.slices.template.stroke = am4core.color("#fff");
			pieSeries.slices.template.strokeWidth = 2;
			pieSeries.slices.template.strokeOpacity = 1;
			pieSeries.slices.template
			// change the cursor on hover to make it apparent the object can be interacted with
			.cursorOverStyle = [
				{
					"property": "cursor",
					"value": "pointer"
				}
			];

			pieSeries.alignLabels = false;
			pieSeries.labels.template.bent = true;
			pieSeries.labels.template.radius = 3;
			pieSeries.labels.template.padding(0,0,0,0);

			pieSeries.ticks.template.disabled = true;

			// Create a base filter effect (as if it's not there) for the hover to return to
			let shadow = pieSeries.slices.template.filters.push(new am4core.DropShadowFilter);
			shadow.opacity = 0;

			// Create hover state
			let hoverState = pieSeries.slices.template.states.getKey("hover"); // normally we have to create the hover state, in this case it already exists

			// Slightly shift the shadow and make it more prominent on hover
			let hoverShadow = hoverState.filters.push(new am4core.DropShadowFilter);
			hoverShadow.opacity = 0.7;
			hoverShadow.blur = 5;

			// Add a legend
			chart.legend = new am4charts.Legend();
			chart.data =  this.data;
		},0)
	}

}
