import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { MatTableDataSource ,
         MatPaginator} from '@angular/material';

@Component({
    selector: 'ms-userlist',
    templateUrl:'./userlist-component.html',
    styleUrls: ['./userlist-component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})

export class UserListComponent implements OnInit {

   constructor( private pageTitleService : PageTitleService ) {}

   ngOnInit() {
      this.pageTitleService.setTitle("User List");
   }
	
   userGridList : any [] = [
      {
         name: 'Adam',
         city: 'California',
         country: 'USA',
         post: 'Senior Developer, Company Inc.',
         image: 'assets/img/user-1.jpg'
      },
      {
         name: 'Thomas',
         city: 'Punjab',
         country: 'India',
         post:'Software Engineer, Company Inc.',
         image:'assets/img/user-2.jpg'
      },
      {
         name: 'Gilcharist',
         city: 'California',
         country: 'USA',
         post: 'Senior Developer, Company Inc.',
         image: 'assets/img/user-3.jpg'
      },
      {
         name: 'John',
         city: 'Punjab',
         country: 'India',
         post: 'Software Engineer, Company Inc.',
         image: 'assets/img/user-4.jpg'
      },
      {
         name: 'Smith',
         city: 'California',
         country: 'USA',
         post: 'Senior Developer, Company Inc.',
         image: 'assets/img/user-1.jpg'
      },
      {
         name: 'Peter',
         city: 'Punjab',
         country: 'India',
         post: 'Software Engineer, Company Inc.',
         image: 'assets/img/user-2.jpg'
      },
      {
         name: 'Kley',
         city: 'California',
         country: 'USA',
         post:'Senior Developer, Company Inc.',
         image:'assets/img/user-3.jpg'
      },
      {
         name: 'Adam',
         city: 'Punjab',
         country: 'India',
         post: 'Software Engineer, Company Inc.',
         image: 'assets/img/user-4.jpg'
      },
      {
         name: 'Orton',
         city: 'California',
         country: 'USA',
         post: 'Senior Developer, Company Inc.',
         image: 'assets/img/user-1.jpg'
      }
   ];
	
}



