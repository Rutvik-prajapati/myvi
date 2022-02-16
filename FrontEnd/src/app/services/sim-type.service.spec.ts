import { TestBed } from '@angular/core/testing';

import { SimTypeService } from './sim-type.service';

describe('SimTypeService', () => {
  let service: SimTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SimTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
