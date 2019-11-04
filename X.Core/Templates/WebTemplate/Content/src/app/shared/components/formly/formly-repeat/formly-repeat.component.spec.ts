import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormlyRepeatComponent } from './formly-repeat.component';

describe('FormlyRepeatComponent', () => {
  let component: FormlyRepeatComponent;
  let fixture: ComponentFixture<FormlyRepeatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormlyRepeatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormlyRepeatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
