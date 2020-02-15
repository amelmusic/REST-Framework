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
   
   @ViewChild(MatPaginator,{static : false}) paginator : MatPaginator;
   popUpNewUserResponse               : any;
   popUpEditUserResponse              : any;
   popUpDeleteUserResponse            : any;
   usermanagelist                     : any; 

   color = {
      "Platinum" : "primary",
      "Gold"     : "accent",
      "Silver"   : "warn"
   }

   displayedColumns : string [] = ['select','user','emailAddress','status','accountType','dateCreated','action'];
   dataSource = new MatTableDataSource<any>(this.usermanagelist);
   selection = new SelectionModel<any>(true, []);

   constructor ( private coreService : CoreService,
                 private pageTitleService : PageTitleService ) { }

   ngOnInit () { 
      this.pageTitleService.setTitle("User Management")
      this.dataSource.paginator = this.paginator;

      this.coreService.getUserManagementList().
         subscribe( res => { this.usermanagelist = res },
                    err => console.log(err),
                    ()  => this.getUserList(this.usermanagelist)
                  ); 
   }

   //getUserList method is used to get the user management list data.
   getUserList(res){
      this.usermanagelist = res;
      this.dataSource = new MatTableDataSource<any>(this.usermanagelist);
      setTimeout(()=>{
         this.dataSource.paginator = this.paginator;
      },0)
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
         this.dataSource = new MatTableDataSource<any>(this.usermanagelist);
         this.dataSource.paginator = this.paginator; 
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
