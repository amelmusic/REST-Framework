import { Component, ViewChild, OnDestroy, AfterViewInit, OnInit } from '@angular/core';

import { Location } from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormGroup} from '@angular/forms';
import {Subject} from 'rxjs';
import { CountryService } from '../country.service';

@Component({
  selector: 'country-details2',
  templateUrl: './country-details2.component.html',
  styleUrls: ['./country-details2.component.scss']
})
export class CountryDetails2Component implements OnInit, AfterViewInit, OnDestroy {
      // REGION:Common Variables
      loading: boolean = true;
      onDestroy$ = new Subject<void>();
      form = new FormGroup({});
      model = {}; // empty form. Override here if necessary
      id: any = 'new';
      fields: FormlyFieldConfig[] = [
        {
            fieldGroupClassName: 'row',
            fieldGroup: [
            
                   {
                       className: 'col-sm-4',
                       type: 'input',
                       key: 'name',
                       templateOptions: {
                           translate: true,
                           label: 'Country.name',
                       },
                   },
            
                   {
                       className: 'col-sm-3',
                       type: 'input',
                       key: 'code',
                       templateOptions: {
                           translate: true,
                           label: 'Country.code',
                       },
                   },
            
            ]
      }];

      actions = [
        {name: 'save', visible: true, method: () => {this.save();}, color: "primary"}
      ];

      constructor(public mainService: CountryService,
                  private router: Router,
                  private route: ActivatedRoute,
                  ) {
      }

      async ngOnInit() {
              await this.init();
      }

      private async init() {
              
              
                this.route.params.subscribe(async params => {
                       this.id = params['id'];
                       if (this.id !== 'new') {
                            this.model = (await this.mainService.get(this.id, {})); // get model if requested
                       }
                    });
      }


      ngAfterViewInit(): void {

      }

      async save() {
              const request = this.model;
              //override request here
              if (this.id === 'new') {
                const result:any = (await this.mainService.insert(request));
                this.id = result.id;
                
              } else {
                const result = (await this.mainService.update(this.id, request));
                
              }
      }

      async submit(e) {
        try {
          if (this.form.valid) {
            this.loading = true;
            this.actions.find(x=>x.name == e.action).method();
            await this.manageActionVisibility();
          }
        } catch (e) {
          throw e; //there is logger on root level
        } finally {
          this.loading = false;
        }
      }

      async manageActionVisibility() {
        if (this.id != 'new') {
          // example
          const action = this.actions.find(x => x.name == "delete");
          action.visible = true; //todo permissions.
        }
      }

      ngOnDestroy(): void {
            this.onDestroy$.next();
            this.onDestroy$.complete();
      }
}
