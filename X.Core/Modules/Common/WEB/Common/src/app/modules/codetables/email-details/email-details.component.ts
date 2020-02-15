import { Component, ViewChild, OnDestroy, AfterViewInit, Input, OnInit } from '@angular/core';

import { Location } from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormGroup} from '@angular/forms';
import {Subject} from 'rxjs';
import { PageTitleService } from 'app/shared/services/page-title.service';
import { EmailService } from 'app/service/email.service';

@Component({
  selector: 'email-details',
  templateUrl: './email-details.component.html',
  styleUrls: ['./email-details.component.scss']
})
export class EmailDetailsComponent implements OnInit, AfterViewInit, OnDestroy {
      // REGION:Common Variables
      loading: boolean = true;
      onDestroy$ = new Subject<void>();
      @Input()
      model:any = {}; // empty form. Override here if necessary
      @Input()
      id: any = 'new';
      additionalData = {includeList: []};
      actions = [
        {name: 'Action.cancel', visible: true, method: () => {this.location.back();}, color: "primary"},
        {name: 'Action.save', visible: true, method: () => {this.save();}, color: "accent"}
      ];
      
      fields: FormlyFieldConfig[] = [
        {
            fieldGroupClassName: 'row',
            fieldGroup: [
            
                   {
                       className: 'col-sm-4',
                       type: 'input',
                       key: 'from',
                       templateOptions: {
                           translate: true,
                           label: 'Email.from',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-4',
                       type: 'input',
                       key: 'to',
                       templateOptions: {
                           translate: true,
                           label: 'Email.to',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-4',
                       type: 'input',
                       key: 'cc',
                       templateOptions: {
                           translate: true,
                           label: 'Email.cc',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-4',
                       type: 'input',
                       key: 'bcc',
                       templateOptions: {
                           translate: true,
                           label: 'Email.bcc',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-3',
                       type: 'checkbox',
                       key: 'sent',
                       templateOptions: {
                           translate: true,
                           label: 'Email.sent',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-3',
                       type: 'checkbox',
                       key: 'failedDelivery',
                       templateOptions: {
                           translate: true,
                           label: 'Email.failedDelivery',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-12',
                       type: 'input',
                       key: 'subject',
                       templateOptions: {
                           translate: true,
                           label: 'Email.subject',
                           
                       },
                   },
            
                   {
                       className: 'col-sm-12',
                       type: 'textarea',
                       key: 'content',
                       templateOptions: {
                           translate: true,
                           label: 'Email.content',
                           rows: 15,
                           
                       },
                   },
            
            ]
      }];
      
      form = new FormGroup({});
      


      constructor(public mainService: EmailService, 
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
                          this.pageTitleService.setTitle('Views.' + 'EmailDetails'); // Change this by loaded model if needed
                       } else {
                         this.model = (await this.mainService.get(this.id, this.additionalData)); // get model if requested
                         this.pageTitleService.setTitle(this.model.subject); // Change this by loaded model if needed
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
