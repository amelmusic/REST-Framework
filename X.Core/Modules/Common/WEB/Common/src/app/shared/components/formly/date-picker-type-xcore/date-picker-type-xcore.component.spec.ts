import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatePickerTypeXCoreComponent } from './date-picker-type-xcore.component';

describe('DatePickerTypeXCoreComponent', () => {
  let component: DatePickerTypeXCoreComponent;
  let fixture: ComponentFixture<DatePickerTypeXCoreComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatePickerTypeXCoreComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatePickerTypeXCoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
