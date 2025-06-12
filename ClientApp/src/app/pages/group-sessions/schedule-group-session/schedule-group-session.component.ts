import { CommonModule } from '@angular/common';
import { Component, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of } from 'rxjs';
import { Classroom } from 'src/app/models/classroom';
import { GroupBrief } from 'src/app/models/group-brief';
import { UserBrief } from 'src/app/models/userBrief';
import { ClassroomService } from 'src/app/services/api/classroom.service';
import { GroupService } from 'src/app/services/api/group.service';
import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-schedule-group-session',
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
  templateUrl: './schedule-group-session.component.html',
  styleUrl: './schedule-group-session.component.scss'
})
export class ScheduleGroupSessionComponent {

  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  sessionForm: FormGroup;
  teachers: UserBrief[] = [];
  groups: GroupBrief[] = []
  classrooms: Classroom[] = []
  private toast = inject(HotToastService);
  constructor(private fb: FormBuilder, private classroomService: ClassroomService, private groupService: GroupService, private userService: UserService) {
    this.sessionForm = this.fb.group({
      title: ['', Validators.required],
      description: [null, Validators.required],
      teacherId: [0, Validators.required],
      timeScheduled: [null, Validators.required],
      groupId: [0, Validators.required],
      classroomId: [0, Validators.required],
      studentPaymentPlans: null
    });
  }
  ngOnInit(): void {
    this.loadTeachers();
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
  loadGroups() {
    this.groupService.getAllBrief().subscribe({
      next: (res: any) => {
        this.groups = res;
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
      const group = this.sessionForm.value;

      console.log('Podaci za slanje:', group);
      this.groupService.addgroup(group).pipe(
        this.toast.observe(
          {
            loading: 'Dodavanje...',
            success: () => 'grupa uspesno dodata',
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
