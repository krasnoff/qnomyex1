import { TestBed } from '@angular/core/testing';

import { WaitListService } from './wait-list.service';

describe('WaitListService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WaitListService = TestBed.get(WaitListService);
    expect(service).toBeTruthy();
  });
});
