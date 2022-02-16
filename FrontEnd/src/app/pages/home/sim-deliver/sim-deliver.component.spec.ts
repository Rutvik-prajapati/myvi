import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimDeliverComponent } from './sim-deliver.component';

describe('SimDeliverComponent', () => {
  let component: SimDeliverComponent;
  let fixture: ComponentFixture<SimDeliverComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimDeliverComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimDeliverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
