import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FileViewerDialogComponent } from './file-viewer-dialog.component';

describe('FileViewerDialogComponent', () => {
  let component: FileViewerDialogComponent;
  let fixture: ComponentFixture<FileViewerDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileViewerDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileViewerDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
