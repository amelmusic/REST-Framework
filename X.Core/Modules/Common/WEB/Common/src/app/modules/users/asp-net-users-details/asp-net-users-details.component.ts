import { Component, ViewChild, OnDestroy, AfterViewInit, Input, OnInit } from '@angular/core';

import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { PageTitleService } from 'app/shared/services/page-title.service';
import { AspNetUsersService } from 'app/service/asp-net-users.service';
import { RoleService } from 'app/service/role.service';

@Component({
  selector: 'asp-net-users-details',
  templateUrl: './asp-net-users-details.component.html',
  styleUrls: ['./asp-net-users-details.component.scss']
})
export class AspNetUsersDetailsComponent implements OnInit, AfterViewInit, OnDestroy {
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

  fields: FormlyFieldConfig[] = [
    {
      fieldGroupClassName: 'row',
      fieldGroup: [

        {
          className: 'col-sm-3',
          type: 'input',
          key: 'firstName',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.firstName',
            required: true
          },
        },

        {
          className: 'col-sm-3',
          type: 'input',
          key: 'lastName',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.lastName',
            required: true
          },
        },

        {
          className: 'col-sm-3',
          type: 'input',
          key: 'userName',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.userName',
            required: true
          },
        },
        {
          className: 'col-sm-3',
          type: 'input',
          key: 'password',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.password',
          },
        },
      ]
    },
    {
      fieldGroupClassName: 'row',
      fieldGroup: [
        {
          className: 'col-sm-3',
          type: 'input',
          key: 'email',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.email',
            required: true
          },
        },

        {
          className: 'col-sm-3',
          type: 'checkbox',
          key: 'emailConfirmed',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.emailConfirmed',
          },
        },

        {
          className: 'col-sm-3',
          type: 'input',
          key: 'phoneNumber',
          templateOptions: {
            translate: true,
            label: 'AspNetUsers.phoneNumber',
          },
        },

      ]
    },
    {
      key: 'aspNetUserRoles',
      type: 'repeat',
      templateOptions: {
        label: 'Role.header',
        allowRemove: true,
        allowAdd: true
      }, fieldArray: {
        fieldGroup: [
          {
            fieldGroupClassName: 'row',
            fieldGroup: [
              {
                className: 'col-sm-10 col-xs-12',
                type: 'autocomplete',
                key: 'roleId',
                templateOptions: {
                  translate: true,
                  label: 'Role.name',
                  required: true,
                  getByFilter: async (id, text) => {
                    const result = await this.roleService.getPage({
                      name: id,
                      fts: text
                    });
                    return result.resultList;
                  },
                  idField: 'name'
                },
              }]
          }]
      }
    }
  ];

  form = new FormGroup({});



  constructor(public mainService: AspNetUsersService, public roleService: RoleService,
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
        this.pageTitleService.setTitle('Views.' + 'AspNetUsersDetails'); // Change this by loaded model if needed
      } else {
        this.model = (await this.mainService.get(this.id, this.additionalData)); // get model if requested
        this.pageTitleService.setTitle(this.model.firstName); // Change this by loaded model if needed
      }
      this.loading = false;
    });

  }


  ngAfterViewInit(): void {

  }

  async save() {
    const request: any = this.model;
    request.roles = this.model.aspNetUserRoles.map(x => { return {id: x.roleId}});
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
