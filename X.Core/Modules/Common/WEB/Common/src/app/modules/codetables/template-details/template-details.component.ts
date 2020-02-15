import { Component, ViewChild, OnDestroy, AfterViewInit, Input, OnInit, ViewEncapsulation } from '@angular/core';

import { Location } from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormGroup} from '@angular/forms';
import {Subject} from 'rxjs';
import { PageTitleService } from 'app/shared/services/page-title.service';
import { TemplateService } from 'app/service/template.service';
import { TemplateTypeService } from 'app/service/template-type.service';

@Component({
  selector: 'template-details',
  templateUrl: './template-details.component.html',
  styleUrls: ['./template-details.component.scss'],
})
export class TemplateDetailsComponent implements OnInit, AfterViewInit, OnDestroy {
      // REGION:Common Variables
      loading: boolean = true;
      onDestroy$ = new Subject<void>();
      @Input()
      model:any = {fileId: 1}; // empty form. Override here if necessary
      @Input()
      id: any = 'new';
      additionalData = {includeList: []};
      actions = [
        {name: 'Action.cancel', visible: true, method: () => {this.location.back();}, color: "primary"},
        {name: 'Action.save', visible: true, method: async () => {await this.save();}, color: "accent"}
      ];
      
      fields: FormlyFieldConfig[] = [
        {
            fieldGroupClassName: 'row',
            fieldGroup: [
            
                   {
                       className: 'col-sm-3',
                       type: 'input',
                       key: 'code',
                       templateOptions: {
                           translate: true,
                           label: 'Template.code',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-6',
                       type: 'input',
                       key: 'description',
                       templateOptions: {
                           translate: true,
                           label: 'Template.description',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-12',
                       type: 'autocomplete',
                       key: 'templateTypeId',
                       templateOptions: {
                           translate: true,
                           label: 'Template.templateTypeId',
                           required: true,
                           getByFilter: async (id, text) => {
                            const result = await this.templateTypeService.getPage({
                              id: id,
                              descriptionGTE: text
                            });
                            return result.resultList;
                          },
                          
                       },
                   },
                  //  {
                  //   className: 'col-sm-12',
                  //   type: 'upload',
                  //   key: 'fileId',
                  //   templateOptions: {
                  //       translate: true,
                  //       label: 'Template.fileId',
                  //       rows: 15,
                        
                  //   },
                  //   },
                   {
                       className: 'col-sm-12',
                       type: 'tinymce',
                       key: 'content',
                       templateOptions: {
                           translate: true,
                           label: 'Template.content',
                           rows: 15,
                           
                       },
                   },
            
            ]
      }];
      
      form = new FormGroup({});
      


      constructor(public mainService: TemplateService, public templateTypeService: TemplateTypeService,
                  private router: Router,
                  private route: ActivatedRoute,
                  private location: Location,
                  private pageTitleService: PageTitleService,
                  ) {
      }

      async ngOnInit() {
              await this.init();
      }

      private async init() {
              
              
              
                this.route.params.subscribe(async params => {
                       this.id = params['id'];
                       if (this.id === 'new') {
                          this.pageTitleService.setTitle('Views.' + 'TemplateDetails'); // Change this by loaded model if needed
                       } else {
                         this.model = (await this.mainService.get(this.id, this.additionalData)); // get model if requested
                         this.pageTitleService.setTitle(this.model.description); // Change this by loaded model if needed
                       }
                       this.loading = false;
                    });

      }


      ngAfterViewInit(): void {

      }

      async save() {
              const request: any = this.model;
              
              //override request here
              if (this.id === 'new') {
                const result:any = (await this.mainService.insert(request));
                this.id = result.id;
                
                
                let url = this.router.url;
                url = url.replace('new', this.id);
                this.location.replaceState(url);
                
              } else {
                const result:any = (await this.mainService.update(this.id, request));
                
              }
      }

      async submit(e) {
        if (this.form.valid || e.action == 'Action.cancel') {
            this.loading = true;
            try {
              await this.actions.find(x=>x.name == e.action).method();
              await this.manageActionVisibility();
            } catch (e) {
              throw e;
            } finally {
              this.loading = false;
            }
          }
      }

      async manageActionVisibility() {
        if (this.id != 'new') {
          // example
          //const action = this.actions.find(x => x.name == "delete");
          //action.visible = false; //todo permissions.
        }
      }

      ngOnDestroy(): void {
            this.onDestroy$.next();
            this.onDestroy$.complete();
      }
}
