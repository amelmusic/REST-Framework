import { Injectable } from '@angular/core';

import { BaseCRUDService } from 'app/shared/services/base-crud.service';
import {MatDialog} from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class StaticDataService   extends BaseCRUDService  {
    // NOTE: Change this part to suit your needs
    getResourceName() {
        return 'StaticData';
    }    
    
    getBasePath() {
        return 'common';
    }
}

