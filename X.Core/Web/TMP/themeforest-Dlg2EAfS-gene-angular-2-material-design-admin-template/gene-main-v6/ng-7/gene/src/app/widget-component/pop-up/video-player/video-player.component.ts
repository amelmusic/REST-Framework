import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { EmbedVideoService } from 'ngx-embed-video';

@Component({
  selector: 'ms-video-player',
  templateUrl: './video-player.component.html',
  styleUrls: ['./video-player.component.scss']
})
export class VideoPlayerComponent implements OnInit, OnDestroy {

	yt_iframe_html : any;
	video 			: string ;

	constructor(public dialogRef : MatDialogRef<VideoPlayerComponent>,
					private embedService: EmbedVideoService) { 
		setTimeout(()=> {
			this.yt_iframe_html = this.embedService.embed(this.video);
		},200)	

		var body = document.body;
		body.classList.add("video-popup");	
	}

	ngOnInit() {
	}

	ngOnDestroy(){
		var body = document.body;
		body.classList.remove("video-popup");
	}

}
