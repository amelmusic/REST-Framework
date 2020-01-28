import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'ms-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {

  constructor() { }
  
  isHeading = true;
  isSubheading = true;
  isHeadingBtn = true;

  projectList= [{
    title: "Project",
    subtitle: "Subtitle",
    image: "https://material.angular.io/assets/img/examples/shiba2.jpg",
    content: " The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog from Japan.A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was originally bred for hunting."
  },
  {
    title: "Project 2",
    subtitle: "Subtitle",
    image: "https://material.angular.io/assets/img/examples/shiba2.jpg",
    content: " The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog from Japan.A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was originally bred for hunting."
  },
  {
    title: "Project 3",
    subtitle: "Subtitle",
    image: "https://material.angular.io/assets/img/examples/shiba2.jpg",
    content: " The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog from Japan.A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was originally bred for hunting."
  }]
  ngOnInit() {
  }

}
