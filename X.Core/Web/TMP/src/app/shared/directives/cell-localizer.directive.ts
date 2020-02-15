import {
    AfterContentInit,
    ContentChildren,
    Directive,
    ElementRef, HostBinding, Input,
    QueryList,
    ViewChild,
    ViewChildren
} from '@angular/core';
import {MatCell, MatHeaderCell} from '@angular/material';
import { TranslateService } from '@ngx-translate/core';

@Directive({
  selector: '[appCellLocalizer]'
})
export class CellLocalizerDirective implements  AfterContentInit {

    @Input()appCellLocalizer;

    @ContentChildren(MatHeaderCell, {descendants: true}) headers !: QueryList<MatHeaderCell>;
    constructor(public el: ElementRef, protected translateService: TranslateService) {}

    @HostBinding('attr.data-label')
    label = '';

    ngAfterContentInit(): void {
        if (this.appCellLocalizer) {
            this.label = this.translateService.instant(this.appCellLocalizer);
        }
    }
}
