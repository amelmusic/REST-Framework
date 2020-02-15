import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {

	aboutUs : any [] = [
		{
			image: "assets/img/about-1060x681.jpg",
			heading : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua, Ut enim ad minim veniam, quis nostrud exercitation.",
			content : "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse."
		}	
	]

	coreValue : any [] = [
		{  
         icon : "gavel",
			title : "Reliability",
			content : "Consectetur adipisicing elit, lorem ipsum dolor sit amet sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
		},
		{  
         icon : "all_inclusive",
			title : "Consistency",
			content : "Ut labore et dolore magna aliqua,lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt."
		},
		{  
         icon : "poll",
			title : "Efficiency",
			content : "Sed do eiusmod tempor incididunt consectetur adipisicing elit, ut labore et dolore magna aliqua lorem ipsum dolor sit amet."
		},
		{  
         icon : "extension",
			title : "Innovation",
			content : "Adipisicing elit, sed do eiusmod tempor lorem ipsum dolor sit amet, consectetur incididunt ut labore et dolore magna aliqua."
		},
		{  
         icon : "note_add",
			title : "Positivity",
			content : "Eiusmod tempor incididunt lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do ut labore et dolore magna aliqua."
		},
		{  
         icon : "my_location",
			title : "Spirit of adventure",
			content : "Adipisicing elit, sed do lorem ipsum dolor sit amet, consectetur eiusmod tempor incididunt ut labore et dolore magna aliqua."
		}
	]

	serviceWeb : any [] = [ 
		{
			serviceType : "New",
			serviceName : "100% Satisfaction",
			serviceCost : "75",
			serviceContent : "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. lorem ipsum dolor sit amet, consectetur, Ut enim ad minim veniam adipisicing elit."
		},
		{
			serviceType : "New",
			serviceName : "Safety First",
			serviceCost : "80",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam lorem ipsum dolor sit amet."
		},
		{
			serviceType : "New",
			serviceName : "Dedicated Experts",
			serviceCost : "55",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut lorem ipsum dolor sit amet, labore et dolore magna aliqua. Ut enim ad minim veniam."
		},
		{
			serviceType : "New",
			serviceName : "Are creative and memorable",
			serviceCost : "105",
			serviceContent : "labore et dolore magna lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut aliqua. Ut enim minim veniam ad."
		},
		{
			serviceType : "New",
			serviceName : "Are easy to find",
			serviceCost : "90",
			serviceContent : "Tempor incididunt lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod ut magna aliqua. Ut enim ad minim veniam labore et dolore."
		},
		{
			serviceType : "New",
			serviceName : "Lorem ipsum",
			serviceCost : "77",
			serviceContent : "Ut labore et lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt dolore magna aliqua. Ut enim ad minim veniam."
		}
	]

	serviceApps : any [] = [ 
		{
			serviceType : "New",
			serviceName : "100% Satisfaction",
			serviceCost : "75",
			serviceContent : "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. lorem ipsum dolor sit amet, consectetur, Ut enim ad minim veniam adipisicing elit."
		},
		{
			serviceType : "New",
			serviceName : "Safety First",
			serviceCost : "80",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam lorem ipsum dolor sit amet."
		},
		{
			serviceType : "New",
			serviceName : "Dedicated Experts",
			serviceCost : "55",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut lorem ipsum dolor sit amet, labore et dolore magna aliqua. Ut enim ad minim veniam."
		},
		{
			serviceType : "New",
			serviceName : "Are creative and memorable",
			serviceCost : "105",
			serviceContent : "labore et dolore magna lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut aliqua. Ut enim minim veniam ad."
		},
		{
			serviceType : "New",
			serviceName : "Are easy to find",
			serviceCost : "90",
			serviceContent : "Tempor incididunt lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod ut magna aliqua. Ut enim ad minim veniam labore et dolore."
		},
		{
			serviceType : "New",
			serviceName : "Lorem ipsum",
			serviceCost : "77",
			serviceContent : "Ut labore et lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt dolore magna aliqua. Ut enim ad minim veniam."
		}
	]

	serviceTechnique : any [] = [ 
		{
			serviceType : "New",
			serviceName : "100% Satisfaction",
			serviceCost : "75",
			serviceContent : "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. lorem ipsum dolor sit amet, consectetur, Ut enim ad minim veniam adipisicing elit."
		},
		{
			serviceType : "New",
			serviceName : "Safety First",
			serviceCost : "80",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam lorem ipsum dolor sit amet."
		},
		{
			serviceType : "New",
			serviceName : "Dedicated Experts",
			serviceCost : "55",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut lorem ipsum dolor sit amet, labore et dolore magna aliqua. Ut enim ad minim veniam."
		},
		{
			serviceType : "New",
			serviceName : "Are creative and memorable",
			serviceCost : "105",
			serviceContent : "labore et dolore magna lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut aliqua. Ut enim minim veniam ad."
		},
		{
			serviceType : "New",
			serviceName : "Are easy to find",
			serviceCost : "90",
			serviceContent : "Tempor incididunt lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod ut magna aliqua. Ut enim ad minim veniam labore et dolore."
		},
		{
			serviceType : "New",
			serviceName : "Lorem ipsum",
			serviceCost : "77",
			serviceContent : "Ut labore et lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt dolore magna aliqua. Ut enim ad minim veniam."
		}
	]

	serviceTechnology : any [] = [ 
		{
			serviceType : "New",
			serviceName : "100% Satisfaction",
			serviceCost : "75",
			serviceContent : "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. lorem ipsum dolor sit amet, consectetur, Ut enim ad minim veniam adipisicing elit."
		},
		{
			serviceType : "New",
			serviceName : "Safety First",
			serviceCost : "80",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam lorem ipsum dolor sit amet."
		},
		{
			serviceType : "New",
			serviceName : "Dedicated Experts",
			serviceCost : "55",
			serviceContent : "Consectetur adipisicing elit, sed do eiusmod tempor incididunt ut lorem ipsum dolor sit amet, labore et dolore magna aliqua. Ut enim ad minim veniam."
		},
		{
			serviceType : "New",
			serviceName : "Are creative and memorable",
			serviceCost : "105",
			serviceContent : "labore et dolore magna lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut aliqua. Ut enim minim veniam ad."
		},
		{
			serviceType : "New",
			serviceName : "Are easy to find",
			serviceCost : "90",
			serviceContent : "Tempor incididunt lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod ut magna aliqua. Ut enim ad minim veniam labore et dolore."
		},
		{
			serviceType : "New",
			serviceName : "Lorem ipsum",
			serviceCost : "77",
			serviceContent : "Ut labore et lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt dolore magna aliqua. Ut enim ad minim veniam."
		}
	]

	counterArray : any [] = [
		{
			counterNumber : 53,
			counterTitle : "Award Winnings",
			counterDescription : "with applied services"
		},
		{
			counterNumber : 1280,
			counterTitle : "Total Projects",
			counterDescription : "on number of industries"
		},
		{
			counterNumber : 522,
			counterTitle : "Gov. Projects",
			counterDescription : "on various competitions"
		},
		{
			counterNumber : 800,
			counterTitle : "Satisfied Clients",
			counterDescription : "from all around the world"
		},
	]

	upcomingEvents : any [] = [
		{
			image : "assets/img/event1-700x450.jpg",
			heading : "Sed do eiusmod lorem ipsum dolor sit amet, consectetur adipisicing.",
			date : "January 10, 2019",
			content : "Sit amet, consectetur adipisicing elit, sed do eiusmod.lorem ipsum dolor."
		},
		{
			image : "assets/img/event2-700x450.jpg",
			heading : "Ut labore et dolore magna aliqua. Ut enim ad sed do eiusmod minim.",
			date : "Feb 02, 2019",
			content : "Lorem ipsum dolor sit amet, sed do eiusmod consectetur adipisicing elit"
		},
		{
			image : "assets/img/event3-700x450.jpg",
			heading : "Consectetur adipisicing elit lorem ipsum dolor sit minim amet.",
			date : "May 08, 2019",
			content : "Dolor ipsum sit amet lorem, consectetur adipisicing elit, sed do eiusmod."
		}
	]
	
	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("About");
	}

}
