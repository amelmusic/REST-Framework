import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FormlyFieldConfig } from '@ngx-formly/core';

export class TranslateFormlyExtensionService {
  constructor(private translate: TranslateService) {}
  prePopulate(field: FormlyFieldConfig) {
      const to = field.templateOptions || {};
      if (!to.translate || to._translated) {
          return;
      }

      to._translated = true;
      field.expressionProperties = {
          ...(field.expressionProperties || {}),
          'templateOptions.label': this.translate.stream(to.label),
      };
  }
}

export function registerTranslateExtension(translate: TranslateService) {
  return {
      extensions: [{
          name: 'translate',
          extension: new TranslateFormlyExtensionService(translate),
      }],
  };
}

