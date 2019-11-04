import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormlyWrapperAddonsComponent } from './formly-wrapper-addons.component';

describe('FormlyWrapperAddonsComponent', () => {
  let component: FormlyWrapperAddonsComponent;
  let fixture: ComponentFixture<FormlyWrapperAddonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormlyWrapperAddonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormlyWrapperAddonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
