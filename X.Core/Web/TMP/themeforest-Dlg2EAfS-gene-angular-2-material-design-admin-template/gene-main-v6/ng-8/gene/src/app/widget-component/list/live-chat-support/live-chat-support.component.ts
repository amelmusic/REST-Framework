import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'ms-live-chat-support',
	templateUrl: './live-chat-support.component.html',
	styleUrls: ['./live-chat-support.component.scss']
})
export class LiveChatSupportComponent implements OnInit {

	@Input() liveChatContent : any;
	
	constructor() { }

	ngOnInit() {
	}

}
