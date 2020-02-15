import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';
import { fadeInAnimation} from "../../core/route-animation/route.animation";
import { CoreService } from '../../service/core/core.service';

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

   userGridList : any;

   constructor( private pageTitleService : PageTitleService,
                public service : CoreService ) {}

   ngOnInit() {
      this.pageTitleService.setTitle("User List");
      this.service.getUserList().
            subscribe( res => { this.userGridList = res },
                       err => console.log(err),
                       ()  => this.userGridList
                     );
   }
}



