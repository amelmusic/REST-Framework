import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {OAuthService} from 'angular-oauth2-oidc';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class HttpContextService {

  constructor(protected http: HttpClient, protected oauthService: OAuthService, protected snackBarService: MatSnackBar, protected translateService: TranslateService) { }

  public createHeaders() {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    const accessToken = this.oauthService.getAccessToken();
    headers = headers.append('Accept-Language', localStorage.getItem('core.language'));

    if (accessToken) {
      headers = headers.append('Authorization', 'Bearer ' + accessToken);
    }
    return headers;
  }

  private sleep (time) {
    return new Promise((resolve) => setTimeout(resolve, time));
  }

  public async get(url, model) {
    let queryParams = '';
    if (model) {
      if (url.indexOf('?')  !== -1) {
        queryParams = '&' + this.param(model, false);
      } else {
        queryParams = '?' + this.param(model, false);
      }
    }
    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }
    url += queryParams;
    const headers = this.createHeaders();

    const options = {
      headers: headers
    }

    return this.http.get(url, options)
        .toPromise()
        .catch(this.handleError.bind(this));
  }

  public async post(url, model) {
    const modelString = JSON.stringify(model);

    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }

    const headers = this.createHeaders();
    const options = {
      headers: headers
    }

    return this.http.post(url, modelString, options)
        .toPromise()
        .catch(this.handleError.bind(this));
  }

  public async put(url, model) {
    const modelString = JSON.stringify(model);

    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }

    const headers = this.createHeaders();
    const options = {
      headers: headers
    }

    return this.http.put(url, modelString, options)
        .toPromise()
        .catch(this.handleError.bind(this));
  }

  public async patch(url, model) {
    const modelString = JSON.stringify(model);

    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }

    const headers = this.createHeaders();
    const options = {
      headers: headers
    }

    return this.http.patch(url, modelString, options)
        .toPromise()
        .catch(this.handleError.bind(this));
  }

  public async delete(url) {
    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }
    const headers = this.createHeaders();
    const options = {
      headers: headers
    }
    return this.http.delete(url, options)
        .toPromise().catch(this.handleError.bind(this));
    // .map((response:Response)=>response.json());
  }

  protected getFilenameFromHeader(res: Response) {
    const contentDispositionHeader = res.headers.get('Content-Disposition');
    const result = contentDispositionHeader.split(';')[1].trim().split('=')[1];
    return result.replace(/"/g, '');
  }
  protected extractBlobData = (res: Response) => {
    const contentDispositionHeader = res.headers.get('Content-Disposition');
    const result = contentDispositionHeader.split(';')[1].trim().split('=')[1];
    const filename = result.replace(/"/g, '');

    const body = res.blob();
    return {body: body, filename: filename};
  };

  protected handleError(error: any) {
    if (error.status === 401 || error.status === 403) {
      this.oauthService.initImplicitFlow();
    }

    debugger;
    if (error === null) {
      error = 'Bad request!';
    }
    if (!error.message) {
      error.message = 'Unknown error (download)';
    }
    console.error('ERROR:', error); // log to console instead
    return Promise.reject(error);
  }

  public async uploadAsPut(url, file) {
    const headers = new HttpHeaders();
    const accessToken = this.oauthService.getAccessToken();

    if (accessToken) {
      headers.append('Authorization', 'Bearer ' + accessToken);
    }

    // headers.append('Content-Type', 'multipart/form-data');
    const options = {
      headers: headers
    }

    const formData: FormData = new FormData();
    formData.append('uploadFile', file, file.name);

    return this.http.put(url, formData, options)
        .toPromise()
        .catch(this.handleError.bind(this));
  }

  public async downloadFile(url, model, fileName = null) {
    const ref = this.snackBarService.open(this.translateService.instant('Common.RunningReport'), null, {
      duration: 0
    });
    let queryParams = '';
    if (model) {
      if (url.indexOf('?')  !== -1) {
        queryParams = '&' + this.param(model, false);
      } else {
        queryParams = '?' + this.param(model, false);
      }
    }
    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }
    url += queryParams;

    const headers = new HttpHeaders();
    headers.append('Accept-Language', localStorage.getItem('core.language'));
    headers.append('responseType', 'blob');
    const accessToken = this.oauthService.getAccessToken();

    if (accessToken) {
      headers.append('Authorization', 'Bearer ' + accessToken);
    }

    const options:any = {
      headers: headers
    }
    //TODO: Check response type / download on HttpClient
    // options.responseType = ResponseContentType.Blob;

    const data = await this.http.get(url, options)
        .toPromise()
        .catch(this.handleError.bind(this));

    const fileDownloadTag = document.createElement('a');
    document.body.appendChild(fileDownloadTag);

    //TODO THIS AS WELL
    // if (navigator.appVersion.toString().indexOf('.NET') > 0) { // for IE browser
    //   window.navigator.msSaveBlob(data.body, fileName == null ? data.filename : fileName);
    // } else { // for other browsers
    //   const fileURL = window.URL.createObjectURL(data.body);
    //   fileDownloadTag.href = fileURL;
    //   fileDownloadTag.download = fileName == null ? data.filename : fileName;
    //   fileDownloadTag.click();
    // }

    ref.dismiss();
  }

  public async downloadFilePost(url, model, fileName = null) {
    const ref = this.snackBarService.open(this.translateService.instant('Common.RunningReport'), null, {
      duration: 0
    });
    let queryParams = '';
    // if (model) {
    //   if (url.indexOf('?')  !== -1) {
    //     queryParams = '&' + this.param(model, false);
    //   } else {
    //     queryParams = '?' + this.param(model, false);
    //   }
    // }
    if (environment.debugTimeout > 0) {
      await this.sleep(environment.debugTimeout);
    }
    // url += queryParams;

    const headers = new Headers();
    headers.append('Accept-Language', localStorage.getItem('core.language'));
    headers.append('responseType', 'blob');
    const accessToken = this.oauthService.getAccessToken();

    if (accessToken) {
      headers.append('Authorization', 'Bearer ' + accessToken);
    }

    const options:any = {
      headers: headers
    }
    // options.responseType = ResponseContentType.Blob;

    const data = await this.http.post(url,model, options)
        .toPromise()
        .catch(this.handleError.bind(this));

    const fileDownloadTag = document.createElement('a');
    document.body.appendChild(fileDownloadTag);

    // if (navigator.appVersion.toString().indexOf('.NET') > 0) { // for IE browser
    //   window.navigator.msSaveBlob(data.body, fileName == null ? data.filename : fileName);
    // } else { // for other browsers
    //   const fileURL = window.URL.createObjectURL(data.body);
    //   fileDownloadTag.href = fileURL;
    //   fileDownloadTag.download = fileName == null ? data.filename : fileName;
    //   fileDownloadTag.click();
    // }

    ref.dismiss();
  }



  rbracket = /\[\]$/;

  param(a, traditional) {
    let prefix,
        s = [],
        add = function (key, valueOrFunction) {
          let value = valueOrFunction;
          if (value instanceof moment) {
            value = (<any>value).toISOString();
          }
          s[s.length] = encodeURIComponent(key) + '=' +
              encodeURIComponent(value == null ? '' : value);
        };
    if (a instanceof Array) {
      for (const x of a) {
        add(x.name, x.value);
      }

    } else {
      for (prefix in a) {
        this.buildParams(prefix, a[prefix], traditional, add);
      }
    }

    return s.join('&');
  }

  buildParams(prefix, obj, traditional, add) {
    let name;

    if (obj instanceof Array) {
      let ind = 0;
      for (const s of obj) {
        if (traditional || this.rbracket.test(prefix)) {
          add(prefix, s);

        } else {
          this.buildParams(
              prefix + '' + (s instanceof Object && s != null ? ind : '') + '',
              s,
              traditional,
              add
          );
        }
        ind++;
      }

    } else if (!traditional && obj instanceof Object && !(obj instanceof moment)) {
      for (name in obj) {
        this.buildParams(prefix + '.' + name, obj[name], traditional, add);
      }

    } else {
      add(prefix, obj);
    }
  }

}