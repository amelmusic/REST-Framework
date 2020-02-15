import { Component, ViewChild, OnDestroy, AfterViewInit, Input, OnInit } from '@angular/core';

import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { PageTitleService } from 'app/shared/services/page-title.service';
import { StaticDataService } from 'app/service/static-data.service';

@Component({
  selector: 'static-data-details',
  templateUrl: './static-data-details.component.html',
  styleUrls: ['./static-data-details.component.scss']
})
export class StaticDataDetailsComponent implements OnInit, AfterViewInit, OnDestroy {
  // REGION:Common Variables
  loading: boolean = true;
  onDestroy$ = new Subject<void>();
  @Input()
  model: any = {}; // empty form. Override here if necessary
  @Input()
  id: any = 'new';
  additionalData = { includeList: [] };
  actions = [
    { name: 'Action.cancel', visible: true, method: () => { this.location.back(); }, color: "primary" },
    { name: 'Action.save', visible: true, method: async () => { await this.save(); }, color: "accent" }
  ];
  editorOptions = {theme: 'vs', language: 'json'};
  fields: FormlyFieldConfig[] = [
    {
      fieldGroupClassName: 'row',
      fieldGroup: [

        {
          className: 'col-sm-6',
          type: 'input',
          key: 'name',
          templateOptions: {
            translate: true,
            label: 'StaticData.name',

          },
        },

        {
          className: 'col-sm-2',
          type: 'input',
          key: 'code',
          templateOptions: {
            translate: true,
            label: 'StaticData.code',

          },
        },

        {
          className: 'col-sm-2',
          type: 'input',
          key: 'parentCode',
          templateOptions: {
            translate: true,
            label: 'StaticData.parentCode',

          },
        },

        {
          className: 'col-sm-2',
          type: 'input',
          key: 'language',
          templateOptions: {
            translate: true,
            label: 'StaticData.language',

          },
        },

      ]
    }];

  form = new FormGroup({});


  constructor(public mainService: StaticDataService,
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
        this.pageTitleService.setTitle('Views.' + 'StaticDataDetails'); // Change this by loaded model if needed
      } else {
        this.model = (await this.mainService.get(this.id, this.additionalData)); // get model if requested
        this.pageTitleService.setTitle(this.model.name); // Change this by loaded model if needed
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
      const result: any = (await this.mainService.insert(request));
      this.id = result.id;


      let url = this.router.url;
      url = url.replace('new', this.id);
      this.location.replaceState(url);

    } else {
      const result: any = (await this.mainService.update(this.id, request));

    }
  }

  async submit(e) {
    if (this.form.valid || e.action == 'Action.cancel') {
      this.loading = true;
      try {
        await this.actions.find(x => x.name == e.action).method();
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
