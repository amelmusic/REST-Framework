import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CodetablesComponent } from './codetables.component';

describe('CodetablesComponent', () => {
  let component: CodetablesComponent;
  let fixture: ComponentFixture<CodetablesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CodetablesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CodetablesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
