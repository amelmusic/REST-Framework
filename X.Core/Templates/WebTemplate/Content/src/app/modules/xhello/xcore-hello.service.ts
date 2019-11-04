import { Injectable } from '@angular/core';
import { BaseReadService } from 'app/shared/services/base-read.service';

@Injectable({
  providedIn: 'root'
})
export class XCoreHelloService  extends BaseReadService   {
    // NOTE: Change this part to suit your needs
    getResourceName() {
        return 'XCoreHello';
    }    
    
    getBasePath() {
        return 'xcore';
    }
}

