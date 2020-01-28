import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormlyTinymceComponent } from './formly-tinymce.component';

describe('FormlyTinymceComponent', () => {
  let component: FormlyTinymceComponent;
  let fixture: ComponentFixture<FormlyTinymceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormlyTinymceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormlyTinymceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
