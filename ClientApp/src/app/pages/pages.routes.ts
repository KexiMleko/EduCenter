import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { AddUserComponent } from './users/add-user/add-user.component';
import { AddStudentComponent } from './students/add-student/add-student.component';
import { StudentListComponent } from './students/student-list/student-list.component';
import { AddGroupComponent } from './groups/add-group/add-group.component';
import { GroupListComponent } from './groups/group-list/group-list.component';
import { EnrollStudentsComponent } from './enrollments/enroll-students/enroll-students.component';

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
    path: 'students',
    component: StudentListComponent
  },
  {
    path: 'students/add',
    component: AddStudentComponent
  },

  {
    path: 'group/add',
    component: AddGroupComponent
  },
  {
    path: 'groups',
    component: GroupListComponent
  },
  {
    path: 'groups/:groupId/enrollments',
    component: EnrollStudentsComponent
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
