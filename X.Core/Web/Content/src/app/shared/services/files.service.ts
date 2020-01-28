import { Injectable } from '@angular/core';
import { BaseCRUDService } from './base-crud.service';

@Injectable({
  providedIn: 'root'
})
export class FilesService extends BaseCRUDService {

  getResourceName() {
    return 'File';
  }

  getBasePath() {
    return 'common';
  }


  async getDownloadUrl(id) {
    // const endpoints = await this.endpointService.getRoutes();
    // const pageUrl = endpoints.common.files_DownloadFileAsync + '?id=' + id;
    // return pageUrl;
  }

  async upload(model) {
    // const url = this.getPageUrl() + `/Activate`;
    const url = this.getPageUrl();
    return this.httpContextService.uploadAsPost(url, model);
  }
  
  async downloadAsBlob(id) {
    return this.download(id, false);
  }
  async download(id, downloadDialog = true) {
    const url = this.getPageUrl() + `/${id}/Download`;
    const data:any = await this.httpContextService.downloadFile(url);
    let disposition = data.headers.get("content-disposition");
    var filename = "";
    if (disposition && disposition.indexOf('attachment') !== -1) {
        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        var matches = filenameRegex.exec(disposition);
        if (matches != null && matches[1]) { 
          filename = matches[1].replace(/['"]/g, '');
        }
    }
    if (downloadDialog) {
      const fileDownloadTag = document.createElement('a');
      document.body.appendChild(fileDownloadTag);
      if (navigator.appVersion.toString().indexOf('.NET') > 0) { // for IE browser
        window.navigator.msSaveBlob(data.body, filename);
      } else { // for other browsers
        const fileURL = window.URL.createObjectURL(data.body);
        fileDownloadTag.href = fileURL;
        fileDownloadTag.download = filename;
        fileDownloadTag.click();
      }
    }
    
    return data.body;
  }
}
