import {Component, OnInit, ViewChild, ViewContainerRef} from '@angular/core';
import {FieldType} from '@ngx-formly/material';
import { AutoCompleteComponent } from '../../auto-complete/auto-complete.component';
import { MatInput } from '@angular/material';

@Component({
    selector: 'app-formly-autocomplete',
    templateUrl: './formly-autocomplete.component.html',
    styleUrls: ['./formly-autocomplete.component.scss']
})
export class FormlyAutocompleteComponent extends FieldType implements OnInit {

    constructor() {
        super();
    }

    idField = 'id';
    nameField = 'description';

    @ViewChild('autoCompleteComponent', { static: true }) public autoCompleteComponent: AutoCompleteComponent;
    @ViewChild(MatInput, { static: true }) formFieldControl: MatInput;

    ngOnInit() {
        this.idField = this.to.idField || this.idField;
        this.nameField = this.to.nameField || this.nameField;
        this.to.reloadOptionsAction = this.reloadOptions;
    }

    getByFilter = (id, text) => {
        return this.to.getByFilter(id, text);
    }

    reloadOptions = () => {
        this.autoCompleteComponent.reloadOptions();
    }

}
