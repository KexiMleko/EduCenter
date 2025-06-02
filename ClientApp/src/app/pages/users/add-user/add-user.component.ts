import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';
import { UserService } from 'src/app/services/api/user.service';
import { MatCardModule } from '@angular/material/card';
import { Role } from 'src/app/models/role';
import { RoleService } from 'src/app/services/api/role.service';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
  ],
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  userForm: FormGroup;

  roles: Role[] = [];
  private toast = inject(HotToastService);
  constructor(private fb: FormBuilder, private service: UserService, private roleService: RoleService) {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      roleIds: ['', Validators.required],
      phoneNumber: [''],
      address: [''],
      note: [''],
    });
  }
  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles() {
    this.roleService.getAll().subscribe({
      next: (res: any) => {
        console.log(res)
        this.roles = res;
      },
      error: (err) => {
        console.error(err)
      }
    })
  }
  onSubmit() {
    if (this.userForm.valid) {
      const user = this.userForm.value;

      console.log('Podaci za slanje:', user);
      this.service.addUser(user).pipe(
        this.toast.observe(
          {
            loading: 'Dodavanje...',
            success: () => 'Korisnik uspesno dodat',
            error: () => 'Doslo je do greske, pokusajte ponovo',
          }
        ),
        catchError((error: any) => of(error))
      ).subscribe({
        next: () => {
          this.formDirective.resetForm();
        }
      });
    }
  }
}
