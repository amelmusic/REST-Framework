import {Component, Inject, OnInit} from '@angular/core';
import {Location} from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {fadeInAnimation} from '../../../../core/route-animation/route.animation';
import {FilesService} from '../../../services/files.service';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-file-viewer-dialog',
  templateUrl: './file-viewer-dialog.component.html',
  styleUrls: ['./file-viewer-dialog.component.scss'],
  animations: [fadeInAnimation]
})
export class FileViewerDialogComponent implements OnInit {

  // REGION: Common Variables
  id: any = null;
  model: any = null;
  form: FormGroup;
  actionProgress: any = {isLoading: true}; // default action or any custom action for eg. actionProgress['save']

  defaultSearchObjectAdditionalData = {};

  // REGION: Additional Variables

  // END REGION: Additional Variables

  constructor(protected translateService: TranslateService
      , protected fb: FormBuilder
      , public location: Location, public dialogRef: MatDialogRef<FileViewerDialogComponent>
      , @Inject(MAT_DIALOG_DATA) public data: any
      , protected filesService: FilesService) {
  }

  async ngOnInit() {
    this.id = this.data.id;
  }


  // REGION: Additional methods

  // END REGION: Additional methods
}
