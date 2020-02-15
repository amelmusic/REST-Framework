import { Routes } from '@angular/router';

import { MediaComponent } from './media/media.component';
import { MediaV2Component } from './mediaV2/mediaV2.component';
import { PricingComponent } from './pricing/pricing.component';
import { Pricing1Component } from './pricing1/pricing1.component';
import { BlankComponent } from './blank/blank.component';
import { FaqComponent } from './faq/faq.component';
import { AboutComponent } from './about/about.component';
import { TimelineComponent } from './timeline/timeline.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { ContactComponent } from './contact/contact.component';
import { SearchComponent } from './search/search.component';


export const PagesRoutes: Routes = [
   {
      path: '',
      redirectTo: 'media',
      pathMatch: 'full'
   },
   {
      path: '',
      children: [
         {
            path: 'media',
            component: MediaComponent
         },
         {
            path: 'mediaV2',
            component: MediaV2Component
         },
         {
            path: 'pricing',
            component:  PricingComponent
         },
         {
            path: 'pricing-1',
            component:  Pricing1Component
         },
			{
            path: 'blank',
            component:  BlankComponent
         },
         {
            path: 'timeline',
            component: TimelineComponent
         },
         {
            path: 'faq',
            component:  FaqComponent
         },
         {
            path: 'feedback',
            component:  FeedbackComponent
         },
         {
            path: 'about',
            component:  AboutComponent
         },
         {
            path: 'contact',
            component:  ContactComponent
         },
         {
            path: 'search',
            component:  SearchComponent
         }
      ]
   }
];
