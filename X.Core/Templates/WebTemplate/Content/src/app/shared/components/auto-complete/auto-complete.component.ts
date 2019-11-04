import {
    AfterViewInit,
    Component,
    ElementRef,
    EventEmitter,
    HostBinding,
    HostListener,
    Input,
    OnDestroy,
    OnInit,
    Optional,
    Output,
    Renderer2,
    Self,
    TemplateRef,
    ViewChild
} from '@angular/core';
import {FormBuilder, FormControl, NgControl} from '@angular/forms';
import {MatAutocomplete, MatDialog, MatFormFieldControl} from '@angular/material';
import 'rxjs/add/operator/debounceTime';
import {Subject} from 'rxjs/Subject';
import {FocusMonitor} from '@angular/cdk/a11y';
import {coerceBooleanProperty} from '@angular/cdk/coercion';


@Component({
    selector: 'app-auto-complete',
    templateUrl: './auto-complete.component.html',
    styleUrls: ['./auto-complete.component.scss'],
    providers: [{provide: MatFormFieldControl, useExisting: AutoCompleteComponent}]
})
export class AutoCompleteComponent implements MatFormFieldControl<any>, OnInit, OnDestroy, AfterViewInit {
    static nextId = 0;
    searchBox: FormControl = new FormControl();
    filteredOptions: any;
    searchValue = '';
    isLoading = true;
    @Output()
    optionSelected: EventEmitter<any> = new EventEmitter<any>();
    @Input() optionalCssClass: any = null;
    @Input('getByFilter') public getByFilter: ((id: any, text: any, controlName: any) => string) | null = null;
    @Input('id') _id = 'id';
    @Input('name') _name = 'name';
    @Input('controlName') _controlName = null;
    @Input('readonly') _readonly = false;
    @Input('columns') _columns = [];
    @Input('localizations') _localizations = [];
    @Input('displayWith') public _displayWith: ((model: any) => string) | null = null;
    @Input() formControlName: string;
    @ViewChild('matAutocomplete', {static: true}) matAutocomplete: MatAutocomplete;
    @ViewChild('searchBoxElement', {static: true}) searchBoxElement: ElementRef;
    @Input()
    viewTemplate: TemplateRef<any>;
    stateChanges = new Subject<void>();
    controlType = 'app-auto-complete';
    context = {
        item: this.selectedItem,
        other: 'otherItem'
    };
    @HostBinding() id = `${this.controlType}-${AutoCompleteComponent.nextId++}`;
    @HostBinding('attr.aria-describedby') describedBy = '';
    // ngControl = null;
    focused = false;

    constructor(@Optional() @Self() public ngControl: NgControl, protected renderer: Renderer2, fb: FormBuilder, private fm: FocusMonitor, private elRef: ElementRef, public dialog: MatDialog) {
        // Just add this in your constructor
        if (this.ngControl) {
            this.ngControl.valueAccessor = this;
        }
        fm.monitor(elRef.nativeElement, true).subscribe((origin) => {
            this.focused = !!origin;
            this.stateChanges.next();
        });
    }

    _selectedItem: any = null;

    get selectedItem() {
        return this._selectedItem;
    }

    set selectedItem(item) {
        this._selectedItem = item;
        this.context.item = this._selectedItem;
    }

    @Input('value') _value = null;

    get value() {
        return this._value;
    }

    set value(val) {
        this._value = val;
        this.onChange(val);
        this.onTouched();
        this.displayItem(val);
        this.stateChanges.next();
    }

    @Input()
    get errorState() {
        return this.ngControl.errors !== null && this.ngControl.touched;
    }

    @HostBinding('class.floating')
    get shouldPlaceholderFloat() {
        return this.focused || !this.empty;
    }

    private _placeholder = '';

    @Input()
    get placeholder() {
        return this._placeholder;
    }

    set placeholder(plh) {
        this._placeholder = plh;
        this.stateChanges.next();
    }

    get autofilled() {
        return false;
    }

    get shouldLabelFloat() {
        return this.focused || !this.empty;
    }

    private _required = false;

    @Input()
    get required() {
        return this._required;
    }

    set required(req) {
        this._required = coerceBooleanProperty(req);
        this.stateChanges.next();
    }

    private _disabled = false;

    @Input()
    get disabled() {
        return this._disabled;
    }

    set disabled(dis) {
        this._disabled = coerceBooleanProperty(dis);
        this.stateChanges.next();
    }

    get empty() {
        const n = this.value;
        return !n;
    }

    optionalCssClassProp() {
        const options = {};
        options[this.optionalCssClass] = this.optionalCssClass;
        return options;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
        this.renderer.setProperty(this.searchBoxElement.nativeElement, 'disabled', this.disabled);
    }

    setDescribedByIds(ids: string[]) {
        this.describedBy = ids.join(' ');
    }

    @HostListener('focusout')
    blur() {
        this.focused = false;
        this.onTouched();
        if (this.searchBox.value !== '' && this.value == null) {
            this.searchBox.setValue('');
        }
    }

    onContainerClick(event: MouseEvent) {
        if ((event.target as Element).tagName.toLowerCase() !== 'input') {
            this.elRef.nativeElement.querySelector('input').focus();
        }
    }

    onChange: any = () => {
    };

    onTouched: any = () => {
    };

    ngOnDestroy() {
        this.stateChanges.complete();
        this.fm.stopMonitoring(this.elRef.nativeElement);
    }

    async ngAfterViewInit() {
        if (this.value === undefined || this.value === null) {
            this.filteredOptions = await this.getByFilter(null, null, this._controlName);
            this.isLoading = false;
        } else if (this.value !== undefined && this.value !== null) {
            await this.displayItem(this.value);
        }
    }

    async ngOnInit() {
        this.searchBox.valueChanges.debounceTime(300).subscribe(async (value) => {
            this.isLoading = true;
            if (value !== this.searchValue && typeof value === 'string' && !this.disabled) {
                this.filteredOptions = await this.getByFilter(null, value, this._controlName);
                this.searchValue = value;
                this.value = null;
            } else if (value === this.searchValue && value === '') {
                this.filteredOptions = await this.getByFilter(null, value, this._controlName);
                this.searchValue = value;
                this.value = null;
            }
            this.isLoading = false;
        });
    }

    async reloadOptions() {
        this.filteredOptions = await this.getByFilter(null, null, this._controlName);
    }

    displayWith = (item) => {
        if (!this._name) {
            throw new Error('Name property is undefined');
        }
        if (this._displayWith && item) {
            return this._displayWith(item);
        } else if (item) {
            return item[this._name];
        } else {
            return '';
        }
    };

    displayItem = async (val) => {
        if (val) {
            if (this.getByFilter != null) {
                let item = null;
                if (this.selectedItem && this.selectedItem[this._id] == val) {
                    item = this.selectedItem;
                } else {
                    this.selectedItem = null;
                    this.isLoading = true;
                    item = await this.getByFilter(val, null, this._controlName);
                    this.isLoading = false;
                }
                if (Array.isArray(item)) {
                    this.selectedItem = item[0];
                } else {
                    this.selectedItem = item;
                }
                if (!this.selectedItem) {
                    throw new Error('Selcted item not found for: ' + val);
                }
                // if (!this.searchBox.value || this.searchBox.value === '') {
                //   if (this.selectedItem[this._name]) {
                //     this.searchBoxElement.nativeElement.value = this.selectedItem[this._name];
                //   }
                // }
                if (this.selectedItem) {
                    if (this.selectedItem[this._name]) {
                        this.searchBoxElement.nativeElement.value = this.selectedItem[this._name];
                    }
                }
            }
        } else {
            this.selectedItem = null;
            if (!this.focused) {
                this.searchBoxElement.nativeElement.value = '';
            }
        }
    };

    onSelectionChange = (event) => {
        if (event.isUserInput) {
            this.selectedItem = event.source.value;
            if (!this._id) {
                throw new Error('Id property isnt defined');
            }
            this.value = event.source.value[this._id];
            setTimeout(() => {
                this.optionSelected.emit(event);
            }, 100);
        }
    };


    registerOnChange(fn) {
        this.onChange = fn;
    }

    registerOnTouched(fn) {
        this.onTouched = fn;
    }

    writeValue(value) {
        this.value = value;
        if (this.value === null) {
            this.filteredOptions = null;
        }
    }

    openDialog() {
        // const dialogRef = this.dialog.open(AutoCompleteDialogComponent, {
        //     data: {
        //         id: this._id,
        //         readonly: this._readonly,
        //         getByFilter: this.getByFilter,
        //         name: this._name,
        //         displayWith: this._displayWith,
        //         controlName: this._controlName,
        //         placeholder: this._placeholder,
        //         value: this._value,
        //         columns: this._columns,
        //         localizations: this._localizations
        //     }, height: '80%'
        // });
        // const dialogSubscriber = dialogRef.afterClosed().subscribe(async item => {
        //     if (item) {
        //         this.selectedItem = item;
        //         if (!this._id) {
        //             throw new Error('Id property isnt defined');
        //         }
        //         this.value = item[this._id];
        //         setTimeout(() => {
        //             this.optionSelected.emit(item);
        //         }, 100);
        //     }
        //     dialogSubscriber.unsubscribe();
        // });
    }


}
