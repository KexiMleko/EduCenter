import { CommonModule } from '@angular/common';
import { Component, inject, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormGroupDirective,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';
import { SubjectService } from 'src/app/services/api/subject.service';

@Component({
  selector: 'app-add-subject',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    FormsModule,
  ],
  templateUrl: './add-subject.component.html',
  styleUrl: './add-subject.component.scss',
})
export class AddSubjectComponent {
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  levelForm: FormGroup;
  private toast = inject(HotToastService);
  constructor(
    private fb: FormBuilder,
    private subjectService: SubjectService,
  ) {
    this.levelForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }
  onSubmit() {
    if (this.levelForm.valid) {
      const subject = this.levelForm.value;

      console.log('Podaci za slanje:', subject);
      this.subjectService
        .addSubject(subject)
        .pipe(
          this.toast.observe({
            loading: 'Dodavanje...',
            success: () => 'Nivo studija uspesno dodat',
            error: () => 'Doslo je do greske, pokusajte ponovo',
          }),
          catchError((error: any) => of(error)),
        )
        .subscribe({
          next: () => {
            this.formDirective.resetForm();
          },
        });
    }
  }
}
