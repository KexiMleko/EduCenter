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
import { UserService } from '../users-service/user.service';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

export interface UserViewModel {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  address: string;
  note?: string;
}

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
    MatSortModule
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent implements OnInit {
  displayedColumns: string[] = ['fullName', 'username', 'email', 'phoneNumber', 'address', 'note', 'actions'];

  private toast = inject(HotToastService);
  users: UserViewModel[] = [];
  constructor(private service: UserService) { }
  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.service.getAllUsers().pipe(
      this.toast.observe(
        {
          loading: 'Pretrazivanje...',
          success: () => 'Uspesna pretraga',
          error: () => 'Doslo je do greske, pokusajte ponovo',
        }
      ),
      catchError((error: any) => of(error))
    ).subscribe({
      next: (value) => {
        this.users = value
      }
    });
  }
  onMenuAction(action: string, user: UserViewModel): void { }
}
