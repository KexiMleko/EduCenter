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
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';
import {
  NgxMatDatepickerActions,
  NgxMatDatepickerApply,
  NgxMatDatepickerCancel,
  NgxMatDatepickerInput,
  NgxMatDatepickerToggle,
  NgxMatDatetimepicker,
} from '@ngxmc/datetime-picker';
import { HotToastService } from '@ngxpert/hot-toast';
import { Classroom } from 'src/app/models/classroom';
import { GroupBrief } from 'src/app/models/group-brief';
import { StudentBrief } from 'src/app/models/student-brief';
import { Subject } from 'src/app/models/subjects';
import { UserBrief } from 'src/app/models/userBrief';
import { ClassroomService } from 'src/app/services/api/classroom.service';
import { IndividualSessionService } from 'src/app/services/api/individual-session.service';
import { StudentService } from 'src/app/services/api/student.service';
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
    NgxMatDatepickerApply,
  ],
  templateUrl: './schedule-individual-session.component.html',
  styleUrl: './schedule-individual-session.component.scss',
})
export class ScheduleIndividualSessionComponent {
  subjects: Subject[] = [];
  currentDate = new Date();
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  sessionForm: FormGroup;
  teachers: UserBrief[] = [];
  groups: GroupBrief[] = [];
  classrooms: Classroom[] = [];
  private studentId = 0;
  student: StudentBrief;
  private toast = inject(HotToastService);

  constructor(
    private fb: FormBuilder,
    private individualSessionService: IndividualSessionService,
    private studentService: StudentService,
    private subjectService: SubjectService,
    private route: ActivatedRoute,
    private classroomService: ClassroomService,
    private userService: UserService,
  ) {
    this.sessionForm = this.fb.group({
      title: ['', Validators.required],
      description: [null, Validators.required],
      amount: [1000, Validators.required],
      studentId: [null, Validators.required],
      teacherId: [null, Validators.required],
      timeScheduled: [null, Validators.required],
      subjectId: [null, Validators.required],
      classroomId: [null, Validators.required],
      sessionDuration: [60, Validators.required],
    });
  }
  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('studentId');
    console.log('id param:' + idParam);
    this.studentId = idParam ? parseInt(idParam, 10) : 0;
    this.loadStudentProfile(this.studentId);
    this.loadTeachers();
    this.loadSubjects();
    this.loadClassrooms();
  }
  loadStudentProfile(studentId: number) {
    if (studentId != 0)
      this.studentService.getStudentBriefById(studentId).subscribe({
        next: (res: StudentBrief) => {
          this.student = res;
          this.sessionForm.get('studentId')?.setValue(this.student.id);
        },
      });
  }
  loadTeachers() {
    this.userService.getByRole(3).subscribe({
      next: (res: any) => {
        this.teachers = res;
      },
      error: (err: any) => {
        console.error(err);
      },
    });
  }
  loadClassrooms() {
    this.classroomService.getAll().subscribe({
      next: (res: any) => {
        this.classrooms = res;
      },
      error: (err: any) => {
        console.error(err);
      },
    });
  }
  onSubmit() {
    if (this.sessionForm.valid) {
      const session = { ...this.sessionForm.value };

      if (session.timeScheduled && session.timeScheduled.toISOString) {
        session.timeScheduled = session.timeScheduled.toISOString();
      }
      console.log('Podaci za slanje:', session);
      this.individualSessionService
        .scheduleSession(session)
        .pipe(
          this.toast.observe({
            loading: 'Zakazivanje...',
            success: () => 'Nastava je uspesno zakazana',
            error: () => 'Doslo je do greske, pokusajte ponovo',
          }),
        )
        .subscribe({
          next: () => {
            this.formDirective.resetForm();
          },
          error: (err: any) => console.error(err),
        });
    }
  }

  loadSubjects() {
    this.subjectService.getAll().subscribe({
      next: (res: any) => {
        this.subjects = res;
      },
      error: (err: any) => {
        console.error(err);
      },
    });
  }
}
