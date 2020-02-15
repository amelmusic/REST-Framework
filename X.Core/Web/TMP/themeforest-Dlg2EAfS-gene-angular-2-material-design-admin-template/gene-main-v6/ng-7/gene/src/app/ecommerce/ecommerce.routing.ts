import { Routes } from '@angular/router';

import { ShopComponent } from './shop/shop.component';
import { CardComponent } from './cards/cards.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { ProductsComponent } from './products/products.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductsComponent } from './edit-products/edit-products.component';

export const EcommerceRoutes: Routes = [
   {
      path: '',
      redirectTo: 'shop',
      pathMatch: 'full',
   },
   {
      path: '',
      children: [
         {
            path: 'shop',
            component: ShopComponent
         },
         {
            path: 'cart',
            component: CartComponent
         },
         {
            path: 'checkout',
            component: CheckoutComponent
         },
         {
            path: 'cards',
            component: CardComponent
         },
         {
            path: 'invoice',
            component: InvoiceComponent
         },
         {
            path: 'products',
            component: ProductsComponent
         },
         {
            path: 'product-add',
            component: AddProductComponent
         },
         {
            path: 'edit-product',
            component: EditProductComponent
         },
         {
            path: 'edit-product/:type/:id',
            component: EditProductComponent
         },
         {
            path: 'edit-products',
            component: EditProductsComponent
         }
      ]
   }
];
