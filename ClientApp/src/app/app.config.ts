
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import {
  ApplicationConfig,
  provideZoneChangeDetection,
  importProvidersFrom,
} from '@angular/core';
import {
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { routes } from './app.routes';
import {
  provideRouter,
  withComponentInputBinding,
  withInMemoryScrolling,
} from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideClientHydration } from '@angular/platform-browser';

// icons
import { TablerIconsModule } from 'angular-tabler-icons';
import * as TablerIcons from 'angular-tabler-icons/icons';

// perfect scrollbar
import { NgScrollbarModule } from 'ngx-scrollbar';

//Import all material modules
import { MaterialModule } from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { authInterceptor } from './auth/auth.interceptor';
import { provideHotToastConfig } from '@ngxpert/hot-toast';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { getSerbianPaginatorIntl } from './configs/serbianPaginatorIntl';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, provideNativeDateAdapter } from '@angular/material/core';
import { RS_DATE_TIME_FORMATS } from './configs/rs-date-time-format';
import { MomentDateAdapter } from '@angular/material-moment-adapter';

export const appConfig: ApplicationConfig = {
  providers: [{
    provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
    useValue: { appearance: 'outline' },
  },
  provideZoneChangeDetection({ eventCoalescing: true }),
  provideRouter(
    routes,
    withInMemoryScrolling({
      scrollPositionRestoration: 'enabled',
      anchorScrolling: 'enabled',
    }),
    withComponentInputBinding()
  ),
  { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
  { provide: MAT_DATE_FORMATS, useValue: RS_DATE_TIME_FORMATS },
  //provideNativeDateAdapter(),
  provideHttpClient(withInterceptors([authInterceptor])),
  provideClientHydration(),
  provideAnimationsAsync(),
  importProvidersFrom(
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    TablerIconsModule.pick(TablerIcons),
    NgScrollbarModule,
  ),
  {
    provide: MatPaginatorIntl,
    useFactory: getSerbianPaginatorIntl
  },
  provideHotToastConfig(),
  ],
};
