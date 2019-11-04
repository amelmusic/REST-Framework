import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  openInStandaloneWindow(url) {
    const strWindowFeatures = 'menubar=no,location=no,resizable=no,scrollbars=yes,status=no,width=1020,height=600';
    window.open(url, 'SYS_WINDOW_STANDALONE', strWindowFeatures);
  }

  dynamicSort(property) {
    let sortOrder = 1;
    if (property[0] === '-') {
      sortOrder = -1;
      property = property.substr(1);
    }
    return function (a, b) {
      const result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
      return result * sortOrder;
    };
  }
}
