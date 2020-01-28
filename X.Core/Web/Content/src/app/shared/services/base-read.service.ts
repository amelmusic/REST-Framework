import { Injectable } from '@angular/core';
import { BaseService } from './base-service.service';
import { HttpContextService } from './http-context.service';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseReadService extends BaseService {
  constructor(
    protected httpContextService: HttpContextService
  ) {
    super();
  }

  abstract getResourceName();

  abstract getBasePath(): string;

  protected getPageUrl(): string {
    return this.getBasePathAbsolute() + "/" + this.getResourceName();
  }

  protected getByIdUrl(): string {
    return this.getBasePathAbsolute() + "/" + this.getResourceName() + "/{id}";
  }
  
  protected getBasePathAbsolute(): string {
    let code: string = this.getBasePath();
    return environment.pathList[code];
  }

  async getPage(searchObject: any, params: any = null): Promise<any> {
    // IF params.saveSearch = true then we should persist search either by given route and name, or by activated route path
    const pageUrl = this.getPageUrl();
    if (!pageUrl) {
      throw new Error('Url undefined');
    }

    if (params && params.saveSearch) {
      const historyRequest = params;
      historyRequest.searchObject = searchObject;
      // try {
      //   await this.searchHistoryService.insert(historyRequest);
      // } catch (error) {
      //   console.log(error);
      // }
    }
    return await this.httpContextService.get(pageUrl, searchObject)
  }

  async get(id, additionalData = null): Promise<any> {
    let url =  this.getByIdUrl();
      if (!url) {
          throw new Error('Url undefined');
      }
    url = url.replace('{id}', id);

    return await this.httpContextService.get(url, additionalData);
  }

  async checkPermission(request: any) {
    if (!request) {
      throw new Error("Request must be populated!");
    }
    let url = this.getBasePathAbsolute() + "/" + "PermissionCheck/Check";
    return await this.httpContextService.get(url, request);
  }

}