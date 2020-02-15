import {AfterViewInit, Component, OnInit, TemplateRef, ViewChild, ViewContainerRef} from '@angular/core';
import {FieldWrapper} from '@ngx-formly/core';

@Component({
  selector: 'app-formly-wrapper-addons',
  templateUrl: './formly-wrapper-addons.component.html',
  styleUrls: ['./formly-wrapper-addons.component.scss']
})
export class FormlyWrapperAddonsComponent  extends FieldWrapper implements AfterViewInit {

    @ViewChild('matPrefix', { static: true }) matPrefix: TemplateRef<any>;
    @ViewChild('matSuffix', { static: true }) matSuffix: TemplateRef<any>;
    @ViewChild('fieldComponent', { read: ViewContainerRef, static: true }) fieldComponent: ViewContainerRef;

    ngAfterViewInit() {
        if (this.matPrefix) {
            Promise.resolve().then(() => this.to.prefix = this.matPrefix);
        }

        if (this.matSuffix) {
            Promise.resolve().then(() => this.to.suffix = this.matSuffix);
        }
    }

    addonRightClick($event: any) {
        if (this.to.addonRight.onClick) {
            this.to.addonRight.onClick(this.to, this, $event);
        }
    }

    addonLeftClick($event: any) {
        if (this.to.addonLeft.onClick) {
            this.to.addonLeft.onClick(this.to, this, $event);
        }
    }

}
