import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormlyUploadComponent } from './formly-upload.component';

describe('FormlyUploadComponent', () => {
  let component: FormlyUploadComponent;
  let fixture: ComponentFixture<FormlyUploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormlyUploadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormlyUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
