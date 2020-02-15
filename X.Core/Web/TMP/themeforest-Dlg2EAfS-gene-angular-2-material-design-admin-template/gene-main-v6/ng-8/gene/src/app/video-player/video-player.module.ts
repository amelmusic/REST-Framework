import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material';
import { VideoPlayerComponent } from './video-player/video-player.component';
import { VideoPlayerRoutes } from './video-player.routing';

import { VgCoreModule} from 'videogular2/core';
import { VgControlsModule} from 'videogular2/controls';
import { VgOverlayPlayModule} from 'videogular2/overlay-play';
import { VgBufferingModule} from 'videogular2/buffering';
import { VgStreamingModule } from 'videogular2/streaming';

@NgModule({
	declarations: [VideoPlayerComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(VideoPlayerRoutes),
		VgCoreModule,
		VgControlsModule,
		VgOverlayPlayModule,
		VgBufferingModule,
		VgStreamingModule,
		MatCardModule
	]
})
export class VideoPlayerModule { }
