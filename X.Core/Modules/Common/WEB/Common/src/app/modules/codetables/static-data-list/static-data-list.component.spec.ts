import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticDataListComponent } from './static-data-list.component';

describe('StaticDataListComponent', () => {
  let component: StaticDataListComponent;
  let fixture: ComponentFixture<StaticDataListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StaticDataListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StaticDataListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
