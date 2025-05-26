import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../users-service/user.service';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent {
  userForm: FormGroup;

  private toast = inject(HotToastService);
  constructor(private fb: FormBuilder, private service: UserService) {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: [''],
      address: [''],
      note: [''],
    });
  }
  showToast() {
    this.toast.show('Hello World!')
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
          this.userForm.reset();
        }
      });
    }
  }
}
