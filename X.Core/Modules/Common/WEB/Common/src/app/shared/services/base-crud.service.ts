import { Injectable, EventEmitter } from '@angular/core';
import { BaseReadService } from './base-read.service';
import { HttpContextService } from './http-context.service';
import { TranslateService } from '@ngx-translate/core';
import { MatSnackBar } from '@angular/material';
import { environment } from 'environments/environment';
import { TranslateExtService } from './translate-ext.service';
export class DataChangedEvent {
  url: string;
  name: string;
  request: any;
  response: any;

  constructor(url = null, name = null, request = null, response = null) {
    this.url = url;
    this.name = name;
    this.request = request;
    this.response = response;
  }
}

@Injectable({
  providedIn: 'root'
})
export abstract class BaseCRUDService extends BaseReadService  {
  public dataChanged: EventEmitter<any>;
  constructor(
      protected httpContextService: HttpContextService,
      protected translateService: TranslateService,
      protected translateServiceExt: TranslateExtService,
      protected snackBarService: MatSnackBar
  ) {
    super(httpContextService);
    this.dataChanged = new EventEmitter();
  }

  async isConfirmed() {
    const areYouSure = await this.translateServiceExt.translate('Common.AreYouSure');
    const requireConfirmation = await this.translateServiceExt.translate('Common.ActionRequireConfirmation');
    const yes = await this.translateServiceExt.translate('Common.Yes');
    const no = await this.translateServiceExt.translate('Common.No');
    let observable = null;


    // const result: any = await promise;
    // observable.unsubscribe();
    // return result.resolved;
    return true;
  }

  async insert(insertRequest: any, skipDefaultSnackbar = false, askConfirmation = false) {
    const pageUrl = this.getPageUrl();
    let confirmed = true;
    if (askConfirmation) {
      confirmed = await this.isConfirmed();
    }
    if (confirmed) {
      const response = await this.httpContextService.post(pageUrl, insertRequest);
      this.dataChanged.emit(new DataChangedEvent(pageUrl, 'SYS_INSERT', insertRequest, response ));
      if (!skipDefaultSnackbar) {
        this.snackBarService.open(this.translateService.instant('Common.RequestCompleted'), null, {
          duration: environment.snackbarDefaultTimeout
        });
      }
      return response;
    }
    return null;
  }

  async update(id: any, updateRequest, skipDefaultSnackbar = false, askConfirmation = false) {
    const url = this.getByIdUrl().replace('{id}', id);
    let confirmed = true;
    if (askConfirmation) {
      confirmed = await this.isConfirmed();
    }
    if (confirmed) {
      const response = await this.httpContextService.put(url, updateRequest);
      this.dataChanged.emit(new DataChangedEvent(url, 'SYS_UPDATE', updateRequest, response ));
      if (!skipDefaultSnackbar) {
        this.snackBarService.open(this.translateService.instant('Common.RequestCompleted'), null, {
          duration: environment.snackbarDefaultTimeout
        });
      }
      return response;
    }
    return null;
  }

  async updateAsPatch(id: any, updateRequest, skipDefaultSnackbar = false, askConfirmation = false) {
    const url = this.getByIdUrl().replace('{id}', id);
    let confirmed = true;
    if (askConfirmation) {
      confirmed = await this.isConfirmed();
    }
    if (confirmed) {
      const response = await this.httpContextService.patch(url, updateRequest);
      this.dataChanged.emit(new DataChangedEvent(url, 'SYS_UPDATE', updateRequest, response ));
      if (!skipDefaultSnackbar) {
        this.snackBarService.open(this.translateService.instant('Common.RequestCompleted'), null, {
          duration: environment.snackbarDefaultTimeout
        });
      }
      return response;
    }
    return null;
  }

  async delete(id: any, skipDefaultSnackbar = false, askConfirmation = true) {
    const url = this.getByIdUrl().replace('{id}', id);
    let confirmed = true;
    if (askConfirmation) {
      confirmed = await this.isConfirmed();
    }
    if (confirmed) {
      const response = await this.httpContextService.delete(url);
      this.dataChanged.emit(new DataChangedEvent(url, 'SYS_DELETE', id, response ));
      if (!skipDefaultSnackbar) {
        this.snackBarService.open(this.translateService.instant('Common.RequestCompleted'), null, {
          duration: environment.snackbarDefaultTimeout
        });
      }
      return response;
    }
    return null;
  }
}