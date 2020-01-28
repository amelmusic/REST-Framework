import {
  AfterViewInit, Component, ElementRef, forwardRef, Input, OnInit, ViewChild,
  ViewEncapsulation,
  Host,
  Self, OnDestroy, Optional, Renderer2, HostBinding, HostListener, TemplateRef
} from '@angular/core';
import {ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, NgControl} from '@angular/forms';
import {MatDialog, MatFormFieldControl} from '@angular/material';
import {FocusMonitor} from '@angular/cdk/a11y';
import {coerceBooleanProperty} from '@angular/cdk/coercion';
import { FilesService } from 'app/shared/services/files.service';
import { FileViewerDialogComponent } from '../file-viewer/file-viewer-dialog/file-viewer-dialog.component';
import { Subject } from 'rxjs/internal/Subject';
// import {FileViewerDialogComponent} from '../file-viewer/file-viewer-dialog/file-viewer-dialog.component';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss'],
  providers: [{provide: MatFormFieldControl, useExisting: UploadComponent}],
})
export class UploadComponent implements MatFormFieldControl<any>, OnInit, OnDestroy, AfterViewInit {

  // region MATERIAL COMPONENT CODE
  controlType = 'app-upload';
  stateChanges = new Subject<void>();
  @HostBinding() id = `${this.controlType}-${UploadComponent.nextId++}`;

  @Input()
  accept = '*/*';

  @Input()
  get placeholder() {
    return this._placeholder;
  }

  set placeholder(plh) {
    this._placeholder = plh;
    this.stateChanges.next();
  }

  private _placeholder = '';

  focused = false;

  get empty() {
    const n = this.value;
    return !n;
  }

  shouldLabelFloat: boolean;

  @HostBinding('class.floating')
  get shouldPlaceholderFloat() {
    return this.focused || !this.empty;
  }

  @Input()
  get required() {
    return this._required;
  }

  set required(req) {
    this._required = coerceBooleanProperty(req);
    this.stateChanges.next();
  }

  private _required = false;

  @Input()
  get disabled() {
    return this._disabled;
  }

  set disabled(dis) {
    this._disabled = coerceBooleanProperty(dis);
    this.stateChanges.next();
  }

  private _disabled = false;

  @Input()
  get errorState() {
    return this.ngControl.errors !== null && this.ngControl.touched;
  }


  @HostBinding('attr.aria-describedby') describedBy = '';

  setDescribedByIds(ids: string[]) {
    this.describedBy = ids.join(' ');
  }

  onContainerClick(event: MouseEvent) {
    if ((event.target as Element).tagName.toLowerCase() == 'input') {
      this.uploadCmp.nativeElement.click();
    }
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.stateChanges.complete();
    this.fm.stopMonitoring(this.elRef.nativeElement);
  }


  ngAfterViewInit(): void {

  }

  // Region COMPONENT SPECIFIC CODE

  uploadBox: FormControl = new FormControl();
  @ViewChild('uploadCmp', {static: false}) uploadCmp: ElementRef;

  static nextId = 0;

  // tslint:disable-next-line:no-input-rename
  @Input('value') _value = null;
  fileUpload: any = null;
  fileUrl: any = null;
  isUploading = false;
  file: any = null;

  onChange: any = () => {
  };
  onTouched: any = () => {
  };

  get value() {
    return this._value;
  }

  set value(val) {
    this._value = val;
    this.onChange(val);
    this.onTouched();
    this.showThumbail();
  }

  async showThumbail() {
    if (this.value) {
      this.fileUrl = await this.filesService.getDownloadUrl(this.value);
      this.file = await this.filesService.get(this.value);
      this.uploadBox.patchValue(this.file.title);
    } else {
      this.fileUrl = null;
      this.file = null;
      this.uploadBox.patchValue('');

      if (this.uploadCmp && this.uploadCmp.nativeElement) {
        this.uploadCmp.nativeElement.value = '';
      }

    }
  }

  constructor(@Optional() @Self() public ngControl: NgControl, public dialog: MatDialog, protected renderer: Renderer2, private fm: FocusMonitor, private elRef: ElementRef, protected filesService: FilesService) {
    if (this.ngControl) {
      this.ngControl.valueAccessor = this;
    }
    fm.monitor(elRef.nativeElement, true).subscribe((origin) => {
      this.focused = !!origin;
      this.stateChanges.next();
    });
  }

  registerOnChange(fn) {
    this.onChange = fn;
  }

  registerOnTouched(fn) {
    this.onTouched = fn;
  }

  writeValue(value) {
    if (value) {
      this.value = value;
    }
  }

  @HostListener('dragover', ['$event'])
  onDragOver(event) {
    this.placeholder = "Spustite fajl ovdje..";
    console.log('DRAGGING');
  };

  @HostListener('drop', ['$event'])
  async onDrop(event) {
    event.preventDefault();
    event.stopPropagation();

    this.isUploading = true;
    const fileList: FileList = event.dataTransfer.files;
    if (fileList.length > 0) {
      const file: File = fileList[0];
      this.fileUpload = file;
    } else {
      this.fileUpload = null;
    }
    await this.uploadFile();
  };

  async uploadFile() {
    if (this.fileUpload) {
      const fileData:any = await this.filesService.upload({File: this.fileUpload, StorageType: 'DB'});
      this.value = fileData.id;
      this.fileUpload = null;
      this.file = fileData;
    }
    this.isUploading = false;
  }

  async upload(event) {
    this.isUploading = true;
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      const file: File = fileList[0];
      this.fileUpload = file;
    } else {
      this.fileUpload = null;
    }
    await this.uploadFile();
  }

  public async download() {
    if (this.value) {
      this.filesService.download(this.value);
    }
  }

  openInDialog() {
    const dialogRef = this.dialog.open(FileViewerDialogComponent, {
      data: {id: this.value}
    });
    const dialogSubscriber = dialogRef.afterClosed().subscribe(async result => {
      // NOTE: do something here
      // if (result) {

      // }
      dialogSubscriber.unsubscribe();
    });
  }

  public clear() {
    this.value = null;

  }

}
