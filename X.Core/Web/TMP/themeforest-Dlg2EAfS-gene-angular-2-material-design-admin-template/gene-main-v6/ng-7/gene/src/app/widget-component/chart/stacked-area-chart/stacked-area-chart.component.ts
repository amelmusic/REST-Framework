import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { D3, D3ChartService } from "../../../core/nvD3/nvD3.service";

@Component({
  selector: 'ms-stacked-area-chart',
  templateUrl: './stacked-area-chart.component.html',
  styleUrls: ['./stacked-area-chart.component.scss']
})
export class StackedAreaChartComponent implements OnInit {

  @Input('data')
  data: any;

  @Input('options')
  options: any;

  chartOptions: any;

  d3: D3;

  constructor(
    nvD3Service: D3ChartService,
  ) {
    this.d3 = nvD3Service.getD3();
  }

  ngOnInit() {

    let d3 = this.d3;

    this.chartOptions = {
      chart: {
        type: 'stackedAreaChart',
        height: 400,
        margin : {
          top: 0,
          right: 0,
          bottom: 20,
          left: 60
        },
        x: (d) => { return d.date; },
        y: (d) => { return d.value; },
        xAxis: {
          tickFormat: (d) => {
            return d3.time.format('%B')(new Date(d));
          },
          showMaxMin: false
        },
        yAxis: {
          tickFormat: d3.format(',.2f')
        },
        useInteractiveGuideline: false,
        showControls: false,

      },
    };
  }

}
