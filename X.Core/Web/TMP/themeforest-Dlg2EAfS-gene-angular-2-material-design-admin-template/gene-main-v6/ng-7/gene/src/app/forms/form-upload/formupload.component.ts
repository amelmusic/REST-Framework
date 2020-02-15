import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { PageTitleService } from '../../core/page-title/page-title.service';
import {fadeInAnimation} from "../../core/route-animation/route.animation";
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'ms-formupload',
    templateUrl:'./formupload-component.html',
    styleUrls: ['./formupload-component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        "[@fadeInAnimation]": 'true'
    },
    animations: [ fadeInAnimation ]
})
export class FormUploadComponent implements OnInit {

    uploader: FileUploader = new FileUploader({url: 'https://evening-anchorage-3159.herokuapp.com/api/'});
    hasBaseDropZoneOver = false;
    hasAnotherDropZoneOver = false;

    constructor( private pageTitleService: PageTitleService,
                 private translate : TranslateService) {}

    ngOnInit() {
        this.pageTitleService.setTitle("Upload");
    }
    
    /**
      *fileOverBase fires during 'over' and 'out' events for Drop Area.
      */
    fileOverBase(e: any): void {
        this.hasBaseDropZoneOver = e;
    }

    /**
      *fileOverAnother fires after a file has been dropped on a Drop Area.
      */
    fileOverAnother(e: any): void {
        this.hasAnotherDropZoneOver = e;
    }
}



