import { NgModule } from '@angular/core';

import { MenuToggleAnchorDirective, MenuToggleLinkDirective, MenuToggleDirective } from './menu-toggle';

@NgModule({
  declarations: [
    MenuToggleAnchorDirective,
    MenuToggleLinkDirective,
    MenuToggleDirective,
  ],
  exports: [
    MenuToggleAnchorDirective,
    MenuToggleLinkDirective,
    MenuToggleDirective,
   ],
})
export class MenuToggleModule { }
