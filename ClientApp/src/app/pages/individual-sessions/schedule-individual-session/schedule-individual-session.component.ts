import { CommonModule } from '@angular/common';
import { Component, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { NgxMatDatepickerActions, NgxMatDatepickerApply, NgxMatDatepickerCancel, NgxMatDatepickerInput, NgxMatDatepickerToggle, NgxMatDatetimepicker } from '@ngxmc/datetime-picker';
import { HotToastService } from '@ngxpert/hot-toast';
import { Classroom } from 'src/app/models/classroom';
import { GroupBrief } from 'src/app/models/group-brief';
import { Subject } from 'src/app/models/subjects';
import { UserBrief } from 'src/app/models/userBrief';
import { ClassroomService } from 'src/app/services/api/classroom.service';
import { IndividualSessionService } from 'src/app/services/api/individual-session.service';
import { SubjectService } from 'src/app/services/api/subject.service';
import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-schedule-individual-session',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,

    // NGX-MAT datetime picker modules
    NgxMatDatetimepicker,
    NgxMatDatepickerToggle,
    NgxMatDatepickerInput,
    NgxMatDatepickerActions,
    //  NgxMatDatepickerClear,
    NgxMatDatepickerCancel,
    NgxMatDatepickerApply
  ],
  templateUrl: './schedule-individual-session.component.html',
  styleUrl: './schedule-individual-session.component.scss'
})
export class ScheduleIndividualSessionComponent {
  subjects: Subject[] = []
  currentDate = new Date()
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  sessionForm: FormGroup;
  teachers: UserBrief[] = [];
  groups: GroupBrief[] = []
  classrooms: Classroom[] = []
  private toast = inject(HotToastService);
  constructor(private fb: FormBuilder, private individualSessionService: IndividualSessionService, private subjectService: SubjectService,
    private classroomService: ClassroomService,
    private userService: UserService) {
    this.sessionForm = this.fb.group({
      title: ['', Validators.required],
      description: [null, Validators.required],
      amount: [1000, Validators.required],
      studentId: [0, Validators.required],
      teacherId: [0, Validators.required],
      timeScheduled: [null, Validators.required],
      subjectId: [0, Validators.required],
      classroomId: [0, Validators.required],
      sessionDuration: [60, Validators.required]
    });
  }
  ngOnInit(): void {
    this.loadTeachers();
    this.loadSubjects()
    this.loadClassrooms()
  }
  loadTeachers() {
    this.userService.getByRole(3).subscribe({
      next: (res: any) => {
        this.teachers = res;
      },
      error: (err: any) => {
        console.error(err)
      }
    })
  }
  loadClassrooms() {
    this.classroomService.getAll().subscribe({
      next: (res: any) => {
        this.classrooms = res;
      },
      error: (err: any) => {
        console.error(err)
      }
    })
  }
  onSubmit() {
    if (this.sessionForm.valid) {
      const session = this.sessionForm.value;

      console.log('Podaci za slanje:', session);
      this.individualSessionService.scheduleSession(session).pipe(
        this.toast.observe(
          {
            loading: 'Zakazivanje...',
            success: () => 'Nastava je uspesno zakazana',
            error: () => 'Doslo je do greske, pokusajte ponovo',
          }
        ),
      ).subscribe({
        next: () => {
          this.formDirective.resetForm();
        },
        error: (err: any) => console.error(err)
      });
    }
  }

  loadSubjects() {
    this.subjectService.getAll().subscribe({
      next: (res: any) => {
        this.subjects = res;
      },
      error: (err: any) => {
        console.error(err)
      }
    })
  }
}
