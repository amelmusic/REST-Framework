import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { XCoreHelloListComponent } from './xcore-hello-list.component';

describe('XCoreHelloListComponent', () => {
  let component: XCoreHelloListComponent;
  let fixture: ComponentFixture<XCoreHelloListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ XCoreHelloListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(XCoreHelloListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
