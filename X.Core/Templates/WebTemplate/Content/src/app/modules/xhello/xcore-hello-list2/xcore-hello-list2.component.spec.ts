import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { XCoreHelloList2Component } from './xcore-hello-list2.component';

describe('XCoreHelloList2Component', () => {
  let component: XCoreHelloList2Component;
  let fixture: ComponentFixture<XCoreHelloList2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ XCoreHelloList2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(XCoreHelloList2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
