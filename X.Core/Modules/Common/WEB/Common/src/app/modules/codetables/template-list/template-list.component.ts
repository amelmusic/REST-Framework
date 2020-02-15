import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatPaginator, MatSort} from '@angular/material';
import { Location } from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormGroup} from '@angular/forms';
import { PageTitleService } from 'app/shared/services/page-title.service';
import { TemplateService } from 'app/service/template.service';
import { TemplateTypeService } from 'app/service/template-type.service';





@Component({
  selector: 'template-list',
  templateUrl: './template-list.component.html',
  styleUrls: ['./template-list.component.scss']
})
export class TemplateListComponent implements OnInit, AfterViewInit {
        // REGION:Common Variables
        
        isLoading = false;
        isFilterDescriptionShown = true;
        searchObject: any = {
          page: 0,
          pageSize: 10,
          includeCount: false, //change this if it's really necessary!
          reload: false,
          // add filters here
          additionalData: { includeList: ["TemplateType"] },
          
        };
        searchObjectClone = {}; //holds initial search object so that we can clear search
        form = new FormGroup({});
        
        fields: FormlyFieldConfig[] = [{
            fieldGroupClassName: 'row',
            fieldGroup: [
            
                   {
                       className: 'col-sm-6',
                       type: 'input',
                       key: 'descriptionGTE',
                       templateOptions: {
                           translate: true,
                           label: 'Template.descriptionGTE',
                           
                           
                       },
                   },
                   {
                       className: 'col-sm-4',
                       type: 'autocomplete',
                       key: 'templateTypeId',
                       templateOptions: {
                           translate: true,
                           label: 'Template.templateTypeId',
                           
                           getByFilter: async (id, text) => {
                            const result = await this.templateTypeService.getPage({
                              id: id,
                              descriptionGTE: text
                            });
                            console.log(result);
                            return result.resultList;
                          },
                       },
                   },
            ]
        }];
        

      searchResult: any = {count: 100, resultList: []}; // leave room for hasMore flag
      displayedColumns: string[] = [
      'description', 'code', 'templateType.description', 
      
      ]
      @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
      @ViewChild(MatSort, {static: false}) sort: MatSort;

      constructor(public mainService: TemplateService, public templateTypeService: TemplateTypeService,
                     private location: Location,
                     private router: Router,
                     protected activatedRoute: ActivatedRoute,
                     private pageTitleService: PageTitleService,
                     
                     ) {
      }

      async ngOnInit() {
          this.pageTitleService.setTitle('Views.' + 'TemplateList');
              await this.init();
      }

      private async init() {
          
          
          Object.assign(this.searchObjectClone, this.searchObject);
           
          this.activatedRoute.queryParams.subscribe(async x => {
              if (x.__query) {
                  const search = JSON.parse(atob(x.__query));
                  Object.assign(this.searchObject, search);
                  this.searchObject = {...this.searchObject};
                  this.searchObject.reload = true;
                  await this.searchInternal();
              } else {
                  this.searchObject = {};
                  Object.assign(this.searchObject, this.searchObjectClone);
                  
              }
          });
          
      }
      

      async search(reload = false) {
              this.searchObject.reload = reload; // reverse logic
              if (reload === true) {
                  this.searchObject.page = 0;
              }
              await this.searchInternal();
      }

        async searchMore() {
            this.searchObject.page += 1;
            await this.searchInternal(true);
        }

      private async searchInternal(append = false) {
          this.isLoading = true;
          const search: any = {};
          Object.assign(search, this.searchObject);
          if (this.searchObject.page === 0) {
              search.includeCount = this.searchObject.includeCount;
          } else {
              search.includeCount = false;
          }
          const result = (await this.mainService.getPage(search));
          if (this.searchObject.reload) {
              this.searchResult.count = result.count || this.searchResult.count;
          }
           this.writeToQueryString(); 
          if (append) {
            this.searchResult.resultList = [...this.searchResult.resultList, ...result.resultList];
            } else {
                this.searchResult.resultList = result.resultList;
            }
          this.searchResult.hasMore = result.hasMore;
          this.isLoading = false;
          this.isFilterDescriptionShown = false;
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
              this.searchObject.orderBy = event.active + " " + event.direction.toUpperCase();
              this.search(true);
          });
      }

      async clear() {
          this.searchObject = {};
          Object.assign(this.searchObject, this.searchObjectClone);
          await this.search(true);
      }

      details(row) {
          this.router.navigate(['template', row.id], { relativeTo: this.activatedRoute.parent });
          
      }

      
}
