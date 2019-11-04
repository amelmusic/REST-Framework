import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketActivityListComponent } from './market-activity-list.component';

describe('MarketActivityListComponent', () => {
  let component: MarketActivityListComponent;
  let fixture: ComponentFixture<MarketActivityListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketActivityListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketActivityListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
