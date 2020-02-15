import { Injectable } from '@angular/core';
import { BaseReadService } from 'app/shared/services/base-read.service';

@Injectable({
  providedIn: 'root'
})
export class TemplateTypeService  extends BaseReadService   {
    // NOTE: Change this part to suit your needs
    getResourceName() {
        return 'TemplateType';
    }    
    
    getBasePath() {
        return 'common';
    }
}

