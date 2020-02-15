import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

	allContacts : any [] =[
		{
			image : "assets/img/user-1.jpg",
			name : "Jerry Ried",
			e_mail : "JerryBRied@jourrapide.com",
			phone_number : "+1 207-589-4752",
			country : "Liberty"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Gustavo Stevenson",
			e_mail : "GustavoJStevenson@rhyta.com",
			phone_number : "+1 727-709-5505",
			country : "Tampa"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "John Shrum",
			e_mail : "JohnLShrum@jourrapide.com ",
			phone_number : "+1 650-722-2798",
			country : "San Francisco"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Julie Reno",
			e_mail : "JulieDReno@dayrep.com",
			phone_number : "+1 956-303-4288",
			country : "Harlingen"
		},
		{
			image : "assets/img/user-5.jpg",
			name : "Nancy Beck",
			e_mail : "NancyKBeck@teleworm.us",
			phone_number : "+1 423-954-4020",
			country : "Chattanooga"
		},
		{
			image : "assets/img/user-6.jpg",
			name : "Travis Klotz",
			e_mail : "TravisMKlotz@jourrapide.com",
			phone_number : "+1 312-405-5954",
			country : "Hickory Hills"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Anna Estes",
			e_mail : "AnnaLEstes@armyspy.com",
			phone_number : "+1 808-652-9469",
			country : "Waipahu"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "David Jones",
			e_mail : "DavidDJones@jourrapide.com",
			phone_number : "+1 407-343-1604",
			country : "Kissimmee"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Hayden Bower",
			e_mail : "HaydenMBower@armyspy.com",
			phone_number : "+1 601-298-5772",
			country : "Carthage"
		},
		{
			image : "assets/img/user-1.jpg",
			name : "Cathy Hagood",
			e_mail : "CathyWHagood@jourrapide.com",
			phone_number : "+1 325-660-7801",
			country : "Abilene"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Anna Estes",
			e_mail : "AnnaLEstes@armyspy.com",
			phone_number : "+1 808-652-9469",
			country : "Waipahu"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Mary Perez",
			e_mail : "MaryJPerez@teleworm.us",
			phone_number : "+1 626-374-4199",
			country : "Alhambra"
		}
	]

	favouriteContacts : any [] =[
		{
			image : "assets/img/user-2.jpg",
			name : "Gustavo Stevenson",
			e_mail : "GustavoJStevenson@rhyta.com",
			phone_number : "+1 727-709-5505",
			country : "Tampa"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "John Shrum",
			e_mail : "JohnLShrum@jourrapide.com ",
			phone_number : "+1 650-722-2798",
			country : "San Francisco"
		},
		{
			image : "assets/img/user-5.jpg",
			name : "Nancy Beck",
			e_mail : "NancyKBeck@teleworm.us",
			phone_number : "+1 423-954-4020",
			country : "Chattanooga"
		},
		{
			image : "assets/img/user-6.jpg",
			name : "Travis Klotz",
			e_mail : "TravisMKlotz@jourrapide.com",
			phone_number : "+1 312-405-5954",
			country : "Hickory Hills"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "David Jones",
			e_mail : "DavidDJones@jourrapide.com",
			phone_number : "+1 407-343-1604",
			country : "Kissimmee"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Hayden Bower",
			e_mail : "HaydenMBower@armyspy.com",
			phone_number : "+1 601-298-5772",
			country : "Carthage"
		}
	]

	recentlyAddedContacts : any [] =[
		{
			image : "assets/img/user-1.jpg",
			name : "Cathy Hagood",
			e_mail : "CathyWHagood@jourrapide.com",
			phone_number : "+1 325-660-7801",
			country : "Abilene"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Anna Estes",
			e_mail : "AnnaLEstes@armyspy.com",
			phone_number : "+1 808-652-9469",
			country : "Waipahu"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "David Jones",
			e_mail : "DavidDJones@jourrapide.com",
			phone_number : "+1 407-343-1604",
			country : "Kissimmee"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Hayden Bower",
			e_mail : "HaydenMBower@armyspy.com",
			phone_number : "+1 601-298-5772",
			country : "Carthage"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Gustavo Stevenson",
			e_mail : "GustavoJStevenson@rhyta.com",
			phone_number : "+1 727-709-5505",
			country : "Tampa"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "John Shrum",
			e_mail : "JohnLShrum@jourrapide.com ",
			phone_number : "+1 650-722-2798",
			country : "San Francisco"
		}
	]
	
	constructor(private pageTitleService: PageTitleService, private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Contact");
	}

}
