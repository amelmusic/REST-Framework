import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PageTitleService } from '../../core/page-title/page-title.service';

@Component({
	selector: 'ms-video-player',
	templateUrl: './video-player.component.html',
	styleUrls: ['./video-player.component.scss'],
	encapsulation:ViewEncapsulation.None
})

export class VideoPlayerComponent implements OnInit {

	sources: Array<Object>;

	constructor(private pageTitle : PageTitleService) {
		this.sources = [
			{
				src: "assets/audio/SampleAudio_0.4mb.mp3",
				type: "audio/mp3"
			}
		];
	}

	ngOnInit() {
		this.pageTitle.setTitle("Video Player")
	}

}
