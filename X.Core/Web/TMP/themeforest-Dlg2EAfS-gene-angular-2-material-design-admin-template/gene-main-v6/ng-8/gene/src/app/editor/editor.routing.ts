import { Routes } from '@angular/router';

import { EditorComponent } from './wysiwyg-editor/editor.component';
import { Ckeditor } from './ckeditor/ckeditor.component';

export const editorRouters : Routes = [
	{
		path : '',
		redirectTo : 'wysiwyg',
		pathMatch : 'full'
	},
	{
		path : '',
		children : [
			{
				path: "wysiwyg",
				component : EditorComponent
			},
			{
				path: "ckeditor",
				component : Ckeditor
			}
		]
	}	
]