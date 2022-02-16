import { TestBed } from '@angular/core/testing';

import { PortnumberService } from './portnumber.service';

describe('PortnumberService', () => {
  let service: PortnumberService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PortnumberService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
