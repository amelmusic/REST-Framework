import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatTableDataSource, MatPaginator} from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { CoreService } from '../../service/core/core.service';
import { PageTitleService } from '../../core/page-title/page-title.service';

@Component({
  selector: 'ms-user-manage-list',
  templateUrl: './user-manage-list.component.html',
  styleUrls: ['./user-manage-list.component.scss']
})
export class UserManageListComponent implements OnInit {
   
   @ViewChild(MatPaginator) paginator : MatPaginator;
   popUpNewUserResponse               : any;
   popUpEditUserResponse              : any;
   popUpDeleteUserResponse            : any;

   color = {
      "Platinum" : "primary",
      "Gold"     : "accent",
      "Silver"   : "warn"
   }

	usermanagelist : any [] = [
      {
      	img:"assets/img/user-1.jpg",
      	firstName : "Steven",
         lastName : "Gonzalez",
      	isNewUser:"New",
       	emailAddress: "StevenAGonzalez@dayrep.com",
        	status: "Active",
         statusTime:"Since 1 hour",  
         accountTypeColor:"primary",
         accountType: "Platinum",
         dateCreated: "13 Aug 2018"
      },
      {
      	img:"assets/img/user-2.jpg",
         firstName : "Josephine",
         lastName : "Goodman",
         isNewUser:"New",
      	emailAddress: "JosephineKGoodman@jourrapide.com",
      	status: "Inactive",
         statusTime:"Since 30 min",
         accountType: "Gold",
         accountTypeColor:"accent",
         dateCreated: "22 Aug 2018"
     	},
      {
			img:"assets/img/user-3.jpg",
         firstName : "Mario",
         lastName : "Harmon",
			emailAddress: "MarioCHarmon@armyspy.com",
			status: "Active",
         statusTime:"Since 2 hour",
			accountType: "Silver",
         accountTypeColor:"warn",
			dateCreated: "13 Aug 2018"
      },
      {
			img:"assets/img/user-4.jpg",
         firstName : "Aleta",
         lastName : "Goodell",
			emailAddress: "AletaDGoodell@teleworm.us",
			status: "Inactive",
         statusTime:"Since 24 min",
			accountType: "Platinum",
         accountTypeColor:"primary",
			dateCreated: "22 Aug 2018",
      },
      {
			img:"assets/img/user-5.jpg",
         firstName : "Florence",
         lastName : "Smith",
			emailAddress: "FlorenceJSmith@rhyta.com",
			status: "Active",
         statusTime:"Since 10 hour",
         accountType:"Gold",
         accountTypeColor:"accent",
			dateCreated: "13 Aug 2018"
      },
      {
			img:"assets/img/user-6.jpg",
         firstName : "Helen",
         lastName : "Moronta",
			emailAddress: "HelenLMoronta@teleworm.us",
			status: "Inactive",
         statusTime:"Since 5 hour",
         accountType: "Silver",
         accountTypeColor:"warn",
			dateCreated: "22 Aug 2018"
      },
      {
			img:"assets/img/user-7.jpg",
         firstName : "Leanora",
         lastName : "Reed",
			emailAddress: "LeanoraTReed@rhyta.com",
			status: "Active",
         statusTime:"Since 10 min",
			accountType: "Platinum",
         accountTypeColor:"primary",
			dateCreated: "13 Aug 2018",
      },
      {
			img:"assets/img/user-8.jpg",
         firstName : "Judy",
         lastName : "Gallardo",
			emailAddress: "JudyPGallardo@dayrep.com",
			status: "Inactive",
         statusTime:"Since 4 hour",
         accountType: "Gold", 
         accountTypeColor:"accent", 
			dateCreated: "22 Aug 2018"
      },
      {
			img:"assets/img/user-9.jpg",
         firstName : "Goldie",
         lastName : "Carlson",
			emailAddress: "GoldieJCarlson@teleworm.us", 
			status: "Active",
         statusTime:"Since 9 hour",
         accountType: "Silver",
         accountTypeColor:"warn",
			dateCreated: "13 Aug 2018"
      },
      {
			img:"assets/img/user-3.jpg",
         firstName : "Bradley",
         lastName : "Bannon",
			emailAddress: "BradleyDBannon@teleworm.us",
			status: "Inactive",
         statusTime:"Since 5 min",
			accountType: "Platinum",
         accountTypeColor:"primary",
			dateCreated: "22 Aug 2018"
      }
   ];

   displayedColumns : string [] = ['select','user','emailAddress','status','accountType','dateCreated','action'];
   dataSource = new MatTableDataSource<any>(this.usermanagelist);
   selection = new SelectionModel<any>(true, []);

   constructor ( private coreService : CoreService,
                 private pageTitleService : PageTitleService ) { }

   ngOnInit () { 
      this.pageTitleService.setTitle("User Management")
      this.dataSource.paginator = this.paginator;
   }

   /** 
     * Whether the number of selected elements matches the total number of rows. 
     */
   isAllSelected() {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
   }

   /**
     * Selects all rows if they are not all selected; otherwise clear selection. 
     */  
   masterToggle() {
      this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
   }

   /** 
     * addNewUserDialog method is used to open a add new user dialog.
     */   
   addNewUserDialog() {
      this.coreService.addNewUserDailog().
         subscribe( res => {this.popUpNewUserResponse = res},
                    err => console.log(err),
                    ()  => this.getAddUserPopupResponse(this.popUpNewUserResponse))
   }

   /**
     *getAddUserPopupResponse method is used to get a new user dialog response.
     *if response will be get then add new user into user list.
     */
   getAddUserPopupResponse(response: any){
      if(response){
         let addUser = {
            img : "assets/img/user-4.jpg",
            firstName : response.firstName,
            lastName : response.lastName,
            emailAddress : response.emailAddress,
            accountType : response.accountType,
            status : "Active",
            statusTime:"Since 1 hour",
            dateCreated : new Date(),
            accountTypeColor : this.color[response.accountType]
         }
         this.usermanagelist.push(addUser);     
      }
   }

   /** 
     *onDelete method is used to open a delete dialog.
     */
   onDelete(i){
      this.coreService.deleteDialog("Are you sure you want to delete this user permanently?").
         subscribe( res => {this.popUpDeleteUserResponse = res},
                    err => console.log(err),
                    ()  => this.getDeleteResponse(this.popUpDeleteUserResponse,i))
   }

   /**
     * getDeleteResponse method is used to delete a user from the user list.
     */
   getDeleteResponse(response : string,i){
      if(response == "yes"){
         this.dataSource.data.splice(i,1);
         this.dataSource = new MatTableDataSource(this.dataSource.data);
      }
   }

   /**
     * onEdit method is used to open a edit dialog.
     */
   onEdit(data, index){
      this.coreService.editList(data).
         subscribe( res => {this.popUpEditUserResponse = res},
                    err => console.log(err),
                    ()  => this.getEditResponse(this.popUpEditUserResponse, data, index))
   }

   /**
     * getEditResponse method is used to edit a user data. 
     */
   getEditResponse(response : any , data, i){
      if(response) {
         this.usermanagelist[i].firstName = response.firstName,  
         this.usermanagelist[i].lastName = response.lastName,
         this.usermanagelist[i].emailAddress = response.emailAddress,
         this.usermanagelist[i].accountType = response.accountType,
         this.usermanagelist[i].accountTypeColor = this.color[response.accountType]

      }
   }
}
