import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { Role } from 'src/app/models/role';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { UserDetails } from 'src/app/models/user-details';
import { UserService } from 'src/app/services/api/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatMenuModule, MatIconModule, MatChipsModule, MatDividerModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
  userProfile: UserDetails | null = null;
  userId: number = 0;
  loading = true;
  constructor(private userService: UserService, private route: ActivatedRoute) { }
  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('groupId');
    this.userId = idParam ? parseInt(idParam, 10) : 0;
    this.loadUserProfile(this.userId);
    console.log(this.userProfile?.updatedAt)
  }

  private loadUserProfile(userId: number) {
    let user$;
    if (userId == 0) {
      user$ = this.userService.getCurrentUserDetails()
    } else {
      user$ = this.userService.getDetailsById(userId)
    }
    user$.subscribe({
      next: (res: any) => this.userProfile = res,
      error: (err: any) => console.error(err)
    })
    this.loading = false;
  }

  getFullName(): string {
    if (!this.userProfile) return '';
    return `${this.userProfile.firstName} ${this.userProfile.lastName}`.trim();
  }

  // Action methods - implement functionality as needed
  onEditProfile() {
    console.log('Edit profile clicked');
    // TODO: Implement edit functionality
  }

  onManageRoles() {
    console.log('Manage roles clicked');
    // TODO: Implement role management
  }

  onChangePassword() {
    console.log('Change password clicked');
    // TODO: Implement password change
  }

  onToggleStatus() {
    console.log('Toggle status clicked');
    // TODO: Implement status toggle
  }

  onDeleteUser() {
    console.log('Delete user clicked');
    // TODO: Implement user deletion with confirmation
  }
}
