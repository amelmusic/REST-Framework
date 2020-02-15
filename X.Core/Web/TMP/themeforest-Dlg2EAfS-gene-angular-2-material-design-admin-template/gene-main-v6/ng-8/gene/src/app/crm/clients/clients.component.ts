import { Component, OnInit } from '@angular/core';
import { CoreService } from '../../service/core/core.service';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {

	popUpDeleteResponse    : any;
	popUpNewClientResponse : any;
	popUpEditUserResponse  : any;

	//client content
    clientsContent : any [] =[
		{
			image : "assets/img/user-1.jpg",
			name : "Jerry Ried",
			e_mail : "JerryBRied@jourrapide.com",
			phone_number : "+1 207-589-4752",
			country : "Liberty",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Gustavo Stevenson",
			e_mail : "GustavoJStevenson@rhyta.com",
			phone_number : "+1 727-709-5505",
			country : "Tampa",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "John Shrum",
			e_mail : "JohnLShrum@jourrapide.com ",
			phone_number : "+1 650-722-2798",
			country : "San Francisco",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Julie Reno",
			e_mail : "JulieDReno@dayrep.com",
			phone_number : "+1 956-303-4288",
			country : "Harlingen",
			tag : "favourite"
		},
		{
			image : "assets/img/user-5.jpg",
			name : "Nancy Beck",
			e_mail : "NancyKBeck@teleworm.us",
			phone_number : "+1 423-954-4020",
			country : "Chattanooga",
			tag : "favourite"
		},
		{
			image : "assets/img/user-6.jpg",
			name : "Travis Klotz",
			e_mail : "TravisMKlotz@jourrapide.com",
			phone_number : "+1 312-405-5954",
			country : "Hickory Hills",
			tag : "favourite"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Anna Estes",
			e_mail : "AnnaLEstes@armyspy.com",
			phone_number : "+1 808-652-9469",
			country : "Waipahu",
			tag : "favourite"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "David Jones",
			e_mail : "DavidDJones@jourrapide.com",
			phone_number : "+1 407-343-1604",
			country : "Kissimmee",
			tag : "favourite"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Hayden Bower",
			e_mail : "HaydenMBower@armyspy.com",
			phone_number : "+1 601-298-5772",
			country : "Carthage",
			tag : "favourite"
		},
		{
			image : "assets/img/user-1.jpg",
			name : "Cathy Hagood",
			e_mail : "CathyWHagood@jourrapide.com",
			phone_number : "+1 325-660-7801",
			country : "Abilene",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Anna Estes",
			e_mail : "AnnaLEstes@armyspy.com",
			phone_number : "+1 808-652-9469",
			country : "Waipahu",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Mary Perez",
			e_mail : "MaryJPerez@teleworm.us",
			phone_number : "+1 626-374-4199",
			country : "Alhambra",
			tag : "recently_added"
		},{
			image : "assets/img/user-1.jpg",
			name : "Jerry Ried",
			e_mail : "JerryBRied@jourrapide.com",
			phone_number : "+1 207-589-4752",
			country : "Liberty",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-2.jpg",
			name : "Gustavo Stevenson",
			e_mail : "GustavoJStevenson@rhyta.com",
			phone_number : "+1 727-709-5505",
			country : "Tampa",
			tag : "recently_added"
		},
		{
			image : "assets/img/user-3.jpg",
			name : "John Shrum",
			e_mail : "JohnLShrum@jourrapide.com ",
			phone_number : "+1 650-722-2798",
			country : "San Francisco",
			tag : "favourite"
		},
		{
			image : "assets/img/user-4.jpg",
			name : "Julie Reno",
			e_mail : "JulieDReno@dayrep.com",
			phone_number : "+1 956-303-4288",
			country : "Harlingen",
			tag : "favourite"
		}
   ]

	constructor(public coreService : CoreService,
				private pageTitleService: PageTitleService,
             private translate : TranslateService) { }

	ngOnInit() {
		this.pageTitleService.setTitle("Clients");
	}

	/** 
     *onDelete method is used to open a delete dialog.
     */
	onDelete(i){
		this.coreService.deleteDialog("Are you sure you want to delete this Client permanently?").
			subscribe(res => {this.popUpDeleteResponse = res},
						 err => console.log(err),
						 ()  => this.getDeleteResponse(this.popUpDeleteResponse,i)
			);
	}

	/**
     * getDeleteResponse method is used to delete a client from the client list.
     */
	getDeleteResponse(response, i ) {
		if(response == 'yes'){
			this.clientsContent.splice(i,1);
		}
	}

	/** 
     * addNewClientDialog method is used to open a add new client dialog.
     */   
   addNewClientDialog() {
      this.coreService.addNewClientDialog().
         subscribe( res => {this.popUpNewClientResponse = res},
                    err => console.log(err),
                    ()  => this.getAddClientPopupResponse(this.popUpNewClientResponse))
   }

   /**
     *getAddClientPopupResponse method is used to get a new client dialog response.
     *if response will be get then add new client into client list.
     */
   getAddClientPopupResponse(response: any){
      if(response){
         let addUser = {
            image : "assets/img/user-4.jpg",
            name : response.name,
            e_mail : response.emailAddress,
            tag : "recently_added",
            country : response.location,
            phone_number : response.phoneNumber
         }
         this.clientsContent.push(addUser);     
      }
   }

   /**
     * onEdit method is used to open a edit dialog.
     */
   onEdit(data, index){
      this.coreService.editClientList(data).
         subscribe( res => {this.popUpEditUserResponse = res},
                    err => console.log(err),
                    ()  => this.getEditResponse(this.popUpEditUserResponse, data, index))
   }

   /**
     * getEditResponse method is used to edit a client data. 
     */
   getEditResponse(response : any , data, i){
      if(response) {
         this.clientsContent[i].name = response.name,  
         this.clientsContent[i].country = response.country,
         this.clientsContent[i].e_mail = response.e_mail,
         this.clientsContent[i].phone_number = response.phone_number
      }
   }
}
