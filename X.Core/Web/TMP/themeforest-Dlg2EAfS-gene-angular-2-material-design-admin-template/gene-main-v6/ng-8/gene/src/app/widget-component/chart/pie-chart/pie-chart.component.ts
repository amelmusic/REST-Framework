import {Component, OnInit, Input, Inject} from '@angular/core';
import {D3, D3ChartService} from "../../../core/nvD3/nvD3.service";

@Component({
  selector: 'ms-pie-chart',
  templateUrl: './pie-chart.component.html'
})

export class PieChartComponent implements OnInit {

  @Input('data') data: any;
  @Input('chartOptions') chartOptions: any;

  d3: D3;
  @Input('title') title: string;
  @Input('subtitle') subtitle: string;
  @Input('bgColor') bgColor: string;
  @Input('textColor') textColor: string;

  constructor( @Inject(D3ChartService) d3ChartService: D3ChartService) {
    this.d3 = d3ChartService.getD3();
  }

  ngOnInit() {
    let d3 = this.d3;

    if (!this.chartOptions) {
      this.chartOptions = {
        chart: {
          type: 'pieChart',
          height: 400,
          margin : {
            top: 0,
            right: 0,
            bottom: 0,
            left: 0
          },
          x: (d) => { return d.label; },
          y: (d) => { return d.value; },
          showXAxis: false,
          showYAxis: false,
          showLegend: true,
          useInteractiveGuideline: true,
          donut: true,
          color: ['#1565C0', '#E53935', '#00897B', '#26C6DA', '#37474F']
        },
      };
    }
  }

}
