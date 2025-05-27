import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { CommonModule } from '@angular/common';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-side-login',
  imports: [RouterModule, MaterialModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './side-login.component.html',
})
export class AppSideLoginComponent {
  constructor(private router: Router, private auth: AuthService) { }
  toast = inject(HotToastService)
  hide = true;
  form = new FormGroup({
    uname: new FormControl('', [Validators.required, Validators.minLength(4)]),
    password: new FormControl('', [Validators.required]),
  });

  get f() {
    return this.form.controls;
  }

  submit() {
    if (this.form.valid) {
      this.auth.login(this.f.uname.value!, this.f.password.value!).pipe(
        this.toast.observe(
          {
            loading: 'Ucitavanje...',
            success: () => 'Uspesna prijava',
            error: () => 'Doslo je do greske, pokusajte ponovo',
          }
        ),
        catchError((error: any) => of(error))

      ).subscribe({
        next: () => {
          this.router.navigate([''])
          console.log("successfull login");
        },
        error: (err: any) => {
          console.error('Login failed', err);
        }
      });
    }
  }
}
