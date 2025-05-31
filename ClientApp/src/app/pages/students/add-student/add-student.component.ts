import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';
import { LevelOfStudy } from 'src/app/models/level-of-study';
import { StudentService } from 'src/app/services/api/student.service';
import { LevelsOfStudyService } from 'src/app/services/levels-of-study.service';

@Component({
  selector: 'app-add-student',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    FormsModule,
  ],
  templateUrl: './add-student.component.html',
  styleUrl: './add-student.component.scss'
})
export class AddStudentComponent implements OnInit {
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  studentForm: FormGroup;
  levels: LevelOfStudy[] = [];
  private toast = inject(HotToastService);
  constructor(private fb: FormBuilder, private service: StudentService, private levelsOfStudyService: LevelsOfStudyService) {
    this.studentForm = this.fb.group({
      academicYear: ['', Validators.required],
      email: ['', [Validators.email]],
      levelId: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: [''],
      address: [''],
      note: [''],
    });
  }
  ngOnInit(): void {
    this.loadLevelsOfStudy();
  }
  loadLevelsOfStudy() {
    this.levelsOfStudyService.getAll().subscribe({
      next: (res: any) => {
        this.levels = res;
      },
      error: (err) => {
        console.error(err)
      }
    })
  }
  onSubmit() {
    if (this.studentForm.valid) {
      const student = this.studentForm.value;

      console.log('Podaci za slanje:', student);
      this.service.addStudent(student).pipe(
        this.toast.observe(
          {
            loading: 'Dodavanje...',
            success: () => 'Student uspesno dodat',
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
