import { Injectable } from '@angular/core';

import { BaseCRUDService } from 'app/shared/services/base-crud.service';
import {MatDialog} from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class MarketActivityService   extends BaseCRUDService  {
    // NOTE: Change this part to suit your needs
    getResourceName() {
        return 'MarketActivity';
    }    
    
    getBasePath() {
        return 'xcore';
    }
}

