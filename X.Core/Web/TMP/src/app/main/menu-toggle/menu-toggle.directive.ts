import { Directive } from '@angular/core';
import { Router } from '@angular/router';

import { MenuToggleLinkDirective } from './menu-toggle-link.directive';

@Directive({
  selector: '[menuToggleDirective]',
})
export class MenuToggleDirective {

  protected navlinks: Array<MenuToggleLinkDirective> = [];

  public closeOtherLinks(openLink: MenuToggleLinkDirective): void {
    this.navlinks.forEach((link: MenuToggleLinkDirective) => {
      if (link !== openLink) {
        link.open = false;
      }
    });
  }

  public addLink(link: MenuToggleLinkDirective): void {
    this.navlinks.push(link);
  }

  public removeGroup(link: MenuToggleLinkDirective): void {
    const index = this.navlinks.indexOf(link);
    if (index !== -1) {
      this.navlinks.splice(index, 1);
    }
  }

  public getUrl() {
    return this.router.url;
  }

  constructor( private router: Router) {}
}
