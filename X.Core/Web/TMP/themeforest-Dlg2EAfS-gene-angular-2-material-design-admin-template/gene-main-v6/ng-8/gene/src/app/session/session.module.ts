import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatInputModule,
			MatFormFieldModule,
			MatCardModule,
			MatButtonModule,
			MatIconModule,
			MatCheckboxModule,
			MatDividerModule,
			MatToolbarModule} from '@angular/material';

import { EcommerceService } from '../service/ecommerce/ecommerce.service';
import { ToastrModule } from 'ngx-toastr';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LockScreenComponent } from './lockscreen/lockscreen.component';;
import { LockScreenV2Component } from './lockscreenV2/lockscreenV2.component';
import { ForgotPasswordV2Component } from './forgot-passwordV2/forgot-passwordV2.component';
import { RegisterV2Component } from './registerV2/registerV2.component';
import { LoginV2Component } from './loginV2/loginV2.component';


import { SessionRoutes } from './session.routing';


@NgModule({
	declarations: [
		LoginComponent,
		RegisterComponent,
		ForgotPasswordComponent,
		LockScreenComponent,
		LoginV2Component,
		RegisterV2Component,
		LockScreenV2Component,
		ForgotPasswordV2Component
	],
	imports: [
		CommonModule,
		MatInputModule,
		MatFormFieldModule,
		FlexLayoutModule,
		MatCardModule,
		MatButtonModule,
		MatIconModule,
		MatToolbarModule,
		MatCheckboxModule,
		MatDividerModule,
		FormsModule,
		TranslateModule,
		ReactiveFormsModule,
		RouterModule.forChild(SessionRoutes),
		ToastrModule.forRoot(),
		SlickCarouselModule
	],
	providers: [
		EcommerceService
	]
})
export class SessionModule { }
