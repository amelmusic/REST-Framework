import { Injectable, ErrorHandler, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class XCoreErrorHandlerService implements ErrorHandler {
  constructor(protected snackBarService: MatSnackBar, protected translateService: TranslateService, private zone: NgZone) { }
  handleError(error: any): void {
    debugger;
    console.error(error);
    this.zone.run(() => {
      if (error && error.rejection && error.rejection.status === 404) {
        alert('Url ' + error.rejection.url + 'is not found or API is unavailable!');
      } else if (error && error.rejection && error.rejection.error && error.rejection.status === 400) {
        let message = this.translateService.instant('Common.Error');
        if (error.rejection.error.error && error.rejection.error.error[0]) {
          message = error.rejection.error.error[0];
        }
        this.snackBarService.open(message, null, {
          duration: 5000
        });
      } else {
        if (environment.production === false) {
          alert(error);
        }
        this.snackBarService.open(this.translateService.instant('Common.Error'), null, {
          duration: 5000
        });
      }
    });


    //throw error;

  }
}
