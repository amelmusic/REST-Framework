import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { XCoreHelloList3Component } from './xcore-hello-list3.component';

describe('XCoreHelloList3Component', () => {
  let component: XCoreHelloList3Component;
  let fixture: ComponentFixture<XCoreHelloList3Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ XCoreHelloList3Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(XCoreHelloList3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
