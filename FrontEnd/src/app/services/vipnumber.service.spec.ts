import { TestBed } from '@angular/core/testing';

import { VipnumberService } from './vipnumber.service';

describe('VipnumberService', () => {
  let service: VipnumberService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VipnumberService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
