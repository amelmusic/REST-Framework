import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AspNetUsersListComponent } from './asp-net-users-list.component';

describe('AspNetUsersListComponent', () => {
  let component: AspNetUsersListComponent;
  let fixture: ComponentFixture<AspNetUsersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AspNetUsersListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AspNetUsersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
