import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError, of, Subject } from 'rxjs';
import { ClassSubject } from 'src/app/models/subject';
import { UserBrief } from 'src/app/models/userBrief';
import { GroupService } from 'src/app/services/api/group.service';
import { SubjectService } from 'src/app/services/api/subject.service';
import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-add-group',
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
  templateUrl: './add-group.component.html',
  styleUrl: './add-group.component.scss'
})
export class AddGroupComponent implements OnInit {

  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  groupForm: FormGroup;
  teachers: UserBrief[] = [];
  subjects: ClassSubject[] = [];
  private toast = inject(HotToastService);
  constructor(private fb: FormBuilder, private groupService: GroupService, private subjectService: SubjectService, private userService: UserService) {
    this.groupForm = this.fb.group({
      name: ['', Validators.required],
      teacherId: ['', Validators.required],
      subjectId: ['', Validators.required],
      maxNumberOfClasses: ['', Validators.required],
      studentPaymentPlans: null
    });
  }
  ngOnInit(): void {
    this.loadTeachers();
    this.loadSubjects();
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
  onSubmit() {
    if (this.groupForm.valid) {
      const group = this.groupForm.value;

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
