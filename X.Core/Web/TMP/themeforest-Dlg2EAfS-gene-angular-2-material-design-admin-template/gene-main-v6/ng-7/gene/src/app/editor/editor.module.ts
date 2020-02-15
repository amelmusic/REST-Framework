import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule} from '@angular/router';
import { CKEditorModule } from 'ng2-ckeditor';
import { QuillModule } from 'ngx-quill';
import { MatCardModule, MatDividerModule, MatInputModule,MatFormFieldModule} from '@angular/material';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { EditorComponent } from './wysiwyg-editor/editor.component';
import { Ckeditor } from './ckeditor/ckeditor.component';
import { editorRouters } from './editor.routing';

@NgModule({
   declarations: [EditorComponent, Ckeditor],
   imports: [
      CommonModule,
      RouterModule.forChild(editorRouters),
      CKEditorModule,
      QuillModule,
      MatCardModule,
      MatDividerModule,
      MatInputModule,
      TranslateModule,
      MatFormFieldModule,
      FormsModule
   ]
})

export class EditorModule { }
