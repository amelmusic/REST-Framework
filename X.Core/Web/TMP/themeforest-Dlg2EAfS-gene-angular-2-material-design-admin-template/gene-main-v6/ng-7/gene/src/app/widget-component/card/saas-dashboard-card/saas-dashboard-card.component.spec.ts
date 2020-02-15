import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaasDashboardCardComponent } from './saas-dashboard-card.component';

describe('SaasDashboardCardComponent', () => {
  let component: SaasDashboardCardComponent;
  let fixture: ComponentFixture<SaasDashboardCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaasDashboardCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaasDashboardCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
