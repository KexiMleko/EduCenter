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
import { LevelsOfStudyService } from 'src/app/services/levels-of-study.service';

@Component({
  selector: 'app-add-level',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    FormsModule,
  ],
  templateUrl: './add-level.component.html',
  styleUrl: './add-level.component.scss',
})
export class AddLevelComponent {
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  levelForm: FormGroup;
  private toast = inject(HotToastService);
  constructor(
    private fb: FormBuilder,
    private levelsOfStudyService: LevelsOfStudyService,
  ) {
    this.levelForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
    });
  }
  onSubmit() {
    if (this.levelForm.valid) {
      const level = this.levelForm.value;

      console.log('Podaci za slanje:', level);
      this.levelsOfStudyService
        .addLevel(level)
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
