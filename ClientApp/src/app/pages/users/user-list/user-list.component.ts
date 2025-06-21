import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { BaseTableComponent } from 'src/app/base/base-table-component';
import { UserService } from 'src/app/services/api/user.service';
import { User } from 'src/app/models/user';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-user-list',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTooltipModule,
    MatDialogModule,
    MatChipsModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSortModule,
    RouterModule,
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss',
})
export class UserListComponent
  extends BaseTableComponent<User, any>
  implements OnInit {
  displayedColumns: string[] = [
    'fullName',
    'username',
    'email',
    'phoneNumber',
    'address',
    'note',
    'actions',
  ];
  constructor(private userService: UserService) {
    super(userService); // explicitly pass service to base class
  }
  users: User[] = [];
  ngOnInit(): void {
    this.loadTableData(this.getUserFilter());
  }
  getUserFilter() {
    return {};
  }
  onMenuAction(action: string, user: User): void { }
}
