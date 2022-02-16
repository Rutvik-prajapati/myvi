import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmMobileNoComponent } from './confirm-mobile-no.component';

describe('ConfirmMobileNoComponent', () => {
  let component: ConfirmMobileNoComponent;
  let fixture: ComponentFixture<ConfirmMobileNoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmMobileNoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmMobileNoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
