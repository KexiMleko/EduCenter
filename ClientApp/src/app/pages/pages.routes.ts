import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { AddUserComponent } from './users/add-user/add-user.component';

export const PagesRoutes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: 'dashboard',
    component: HomeComponent,
  },
  {
    path: 'users',
    component: UserListComponent
  },
  {
    path: 'users/add',
    component: AddUserComponent
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
