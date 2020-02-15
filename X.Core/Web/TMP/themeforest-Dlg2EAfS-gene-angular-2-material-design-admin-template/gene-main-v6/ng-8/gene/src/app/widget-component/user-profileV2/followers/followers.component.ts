import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ms-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.scss']
})
export class FollowersComponent implements OnInit {

	@Input() followers : any;

	constructor(private translate : TranslateService) { }

	ngOnInit() {
	}

}
