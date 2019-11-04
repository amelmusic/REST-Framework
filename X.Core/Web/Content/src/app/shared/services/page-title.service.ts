import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class PageTitleService {
    public title: BehaviorSubject<string> = new BehaviorSubject<string>(null);

    constructor(
        private translateService: TranslateService) {
    }

    setTitle(value: string) {
        this.title.next(this.translateService.instant(value));
    }
}