import { Routes } from '@angular/router';

import { FormWizardComponent}  from './form-wizard/formwizard.component';
import { FormValidationComponent}  from './form-validation/formvalidation.component';
import { FormUploadComponent}  from './form-upload/formupload.component';
import { FormTreeComponent}  from './form-tree/formtree.component';

export const FormRoutes: Routes = [
   {
      path: '',
      redirectTo: 'form-wizard',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'form-wizard',
            component: FormWizardComponent
         },
         {
            path: 'form-validation',
            component:  FormValidationComponent
         },
         {
            path: 'form-upload',
            component: FormUploadComponent
         },
         {
            path: 'form-tree',
            component:  FormTreeComponent
         }
      ]
   }
];
