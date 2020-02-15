import { Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LockScreenComponent } from './lockscreen/lockscreen.component';
import { LockScreenV2Component } from './lockscreenV2/lockscreenV2.component';
import { ForgotPasswordV2Component } from './forgot-passwordV2/forgot-passwordV2.component';
import { RegisterV2Component } from './registerV2/registerV2.component';
import { LoginV2Component } from './loginV2/loginV2.component';

export const SessionRoutes: Routes = [
   {
      path: '',
      redirectTo: 'login',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'login',
            component: LoginComponent
         },
         {
            path: 'register',
            component:  RegisterComponent
         },
         {
            path: 'forgot-password',
            component: ForgotPasswordComponent
         },
         {
            path: 'lockscreen',
            component:  LockScreenComponent
         },
          {
            path: 'loginV2',
            component: LoginV2Component
         },
         {
            path: 'registerV2',
            component:  RegisterV2Component
         },
         {
            path: 'forgot-passwordV2',
            component: ForgotPasswordV2Component
         },
         {
            path: 'lockscreenV2',
            component:  LockScreenV2Component
         }
      ]
   }
];
