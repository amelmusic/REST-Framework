import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatPaginator, MatSort} from '@angular/material';
import { Location } from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormGroup} from '@angular/forms';
import { XCoreHelloService } from '../xcore-hello.service';

@Component({
  selector: 'xcore-hello-list2',
  templateUrl: './xcore-hello-list2.component.html',
  styleUrls: ['./xcore-hello-list2.component.scss']
})
export class XCoreHelloList2Component implements OnInit, AfterViewInit {
        // REGION:Common Variables
        isLoading = false;
        searchObject: any = {
          page: 0,
          pageSize: 10,
          countEnabled: false,
          // add filters here
          nameGTE: null
        };
        searchObjectClone = {}; //holds initial search object so that we can clear search
        form = new FormGroup({});
        fields: FormlyFieldConfig[] = [{
            fieldGroupClassName: 'row', //on root wee don't need it
            fieldGroup: [
                {
                    className: 'col col-lg-4',
                    type: 'input',
                    key: 'nameGTE',
                    templateOptions: {
                        translate: true,
                        label: 'GENERAL.SEARCH'
                    },
                }
            ],
        }];

      searchResult: any = {count: 100, list: []}; // leave room for hasMore flag
      displayedColumns: string[] = [
      'id', 'name', 
      
      ]
      @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
      @ViewChild(MatSort, {static: false}) sort: MatSort;

      constructor(public mainService: XCoreHelloService,
                     private location: Location,
                     private router: Router,
                     protected activatedRoute: ActivatedRoute,
                     ) {
      }

      async ngOnInit() {
              await this.init();
      }

      private async init() {
          Object.assign(this.searchObjectClone, this.searchObject);
           
          this.activatedRoute.queryParams.subscribe(async x => {
              if (x.__query) {
                  const search = JSON.parse(atob(x.__query));
                  Object.assign(this.searchObject, search);
                  this.searchObject.countEnabled = true;
                  await this.searchInternal();
              } else {
                  this.searchObject = {};
                  Object.assign(this.searchObject, this.searchObjectClone);
                  await this.search(true);
              }
          });
          
      }

      async search(countEnabled = false) {
              this.searchObject.countEnabled = countEnabled; // reverse logic
              if (countEnabled === true) {
                  this.searchObject.page = 0;
              }
              await this.searchInternal();
      }

      private async searchInternal() {
          this.isLoading = true;
          const result = (await this.mainService.getPage(this.searchObject));
          if (this.searchObject.countEnabled) {
              this.searchResult.count = result.count || this.searchResult.count;
          }
           this.writeToQueryString(); 
          this.searchResult.list = result.list;
          this.isLoading = false;
          return result;
      }
      
      private writeToQueryString() {
          const urlParts = this.router.url.split('?');
          let url = urlParts[0];
          const urlParams = new URLSearchParams(urlParts[urlParts.length - 1]);
          urlParams.delete('__query');

          url = url.split('?')[0] + '?';
          urlParams.append('__query', btoa(JSON.stringify(this.searchObject)));
          let params = '';
          urlParams.forEach((value, key) => {
              if (value && value !== '') {
                  params += key + '=' + value + '&';
              }
          });
          url += params;
          this.location.replaceState(url);
      }
      
      ngAfterViewInit(): void {
          this.paginator.page.subscribe( async(page) => {
              const reload = this.searchObject.pageSize !== page.pageSize;
              this.searchObject.page = page.pageIndex;
              this.searchObject.pageSize = page.pageSize;
              await this.search(reload);
          });
          this.sort.sortChange.subscribe((event) =>  {
              this.searchObject.page = 0;
              this.searchObject.sortField = event.active;
              this.searchObject.sortDirection = event.direction.toUpperCase();
              this.search(true);
          });
      }

      async clear() {
          this.searchObject = {};
          Object.assign(this.searchObject, this.searchObjectClone);
          await this.search(true);
      }

      details(row) {
          this.router.navigate(['all', { id: row.id }]);
      }

      
}
