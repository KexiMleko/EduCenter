import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

export const PagesRoutes: Routes = [
  {
    path: '',
    component: HomeComponent,
    // data: {
    //   title: 'Starter',
    //   urls: [
    //     { title: 'Dashboard', url: '/home' },
    //     { title: 'Starter' },
    //   ],
    // },
  },
  {
    path: '**',
    component: HomeComponent
  }
];
