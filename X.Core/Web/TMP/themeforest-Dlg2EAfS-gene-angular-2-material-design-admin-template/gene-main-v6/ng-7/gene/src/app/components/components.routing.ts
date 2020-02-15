import { Routes } from '@angular/router';

import { ButtonsComponent } from './buttons/buttons.component';
import { CheckboxComponent } from './checkbox/checkbox.component';
import { CardsComponent } from './cards/cards.component';
import { ColorpickerComponent } from './colorpicker/colorpicker.component';
import { DatepickerComponent } from './datepicker/datepicker.component';
import { DialogOverviewComponent } from './dialog/dialog.component';
import { GridListComponent } from './grid-list/gridlist.component';
import { InputComponent } from './input/input.component';
import { ListOverviewComponent } from './list/list.component';
import { MenuOverviewComponent } from './menu/menu.component';
import { ProgressComponent } from './progress/progress.component';
import { RadioComponent } from './radio/radio.component';
import { SelectComponent } from './select/select.component';
import { SliderOverviewComponent } from './slider/slider.component';
import { SnackbarOverviewComponent } from './snackbar/snackbar.component';
import { TabsComponent } from './tabs/tabs.component';
import { TooltipOverviewComponent } from './tooltip/tooltip.component';
import { ToolbarComponent } from './toolbar/toolbar.component';


export const ComponentsRoutes: Routes = [
   {
      path: '',
      redirectTo: 'buttons',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'buttons',
            component: ButtonsComponent
         },
         {
            path: 'checkbox',
            component: CheckboxComponent 
         },
         {
            path: 'cards',
            component: CardsComponent
         },
         {
            path: 'colorpicker',
            component: ColorpickerComponent 
         },
          {
            path: 'datepicker',
            component: DatepickerComponent
         },
         {
            path: 'dialog',
            component: DialogOverviewComponent 
         },
         {
            path: 'grid',
            component: GridListComponent
         },
         {
            path: 'input',
            component: InputComponent 
         },
           {
            path: 'list',
            component: ListOverviewComponent
         },
         {
            path: 'menu',
            component: MenuOverviewComponent 
         },
         {
            path: 'progress',
            component: ProgressComponent
         },
         {
            path: 'radio',
            component: RadioComponent 
         },
         {
            path: 'select',
            component: SelectComponent
         },
         {
            path: 'slider',
            component: SliderOverviewComponent 
         },
         {
            path: 'snackbar',
            component: SnackbarOverviewComponent
         },
         {
            path: 'tabs',
            component: TabsComponent 
         },
         {
            path: 'toolbar',
            component: ToolbarComponent
         },
         {
            path: 'tooltip',
            component: TooltipOverviewComponent
         }
      ]
   }
];
