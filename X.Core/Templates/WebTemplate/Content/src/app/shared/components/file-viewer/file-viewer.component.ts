import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Error} from 'tslint/lib/error';
import { FilesService } from 'app/shared/services/files.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-file-viewer',
  templateUrl: './file-viewer.component.html',
  styleUrls: ['./file-viewer.component.scss']
})
export class FileViewerComponent implements OnInit, OnChanges {
  @Input()
  fileId: any = null;

  @Input()
  fileUrl: any = null;

  @Input()
  fileInfo: any = null;

  @Input()
  displayCard = true;

  loading = true;

  data = null;

  constructor(protected filesService: FilesService, private sanitizer: DomSanitizer) {
  }

  async ngOnInit() {
    if (this.fileId !== null) {
      this.fileInfo = await this.filesService.get(this.fileId);
      //TODO: check if it's image, show otherwise offer download
      const mimeType = this.fileInfo.mimeType;
      if (mimeType === "image/jpg" || mimeType === "image/jpeg" || mimeType === "image/png") {
        const tmpData = await this.filesService.downloadAsBlob(this.fileId);
        const fileURL = window.URL.createObjectURL(tmpData);
        this.data = this.sanitizer.bypassSecurityTrustUrl(fileURL);
      } else {
        this.data = null;
      }
      
      this.loading = false;
    }
  }

  async ngOnChanges(changes: SimpleChanges) {
    if (this.fileId) {
      this.fileUrl = await this.filesService.getDownloadUrl(this.fileId);
      this.fileInfo = await this.filesService.get(this.fileId);
    }
  }

  async download() {
    await this.filesService.download(this.fileId);
  }

}
