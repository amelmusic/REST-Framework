import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'ms-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit {

  @Input()
  public isLoading = false;
  
  constructor() { }

  ngOnInit() {
  }

}
