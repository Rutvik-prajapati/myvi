import { TestBed } from '@angular/core/testing';

import { OrderSIMCardService } from './order-simcard.service';

describe('OrderSIMCardService', () => {
  let service: OrderSIMCardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderSIMCardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
