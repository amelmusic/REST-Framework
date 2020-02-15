import { TestBed } from '@angular/core/testing';

import { XCoreErrorHandlerService } from './xcore-error-handler.service';

describe('XCoreErrorHandlerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: XCoreErrorHandlerService = TestBed.get(XCoreErrorHandlerService);
    expect(service).toBeTruthy();
  });
});
