import { CommonModule } from '@angular/common';
import {
  CdkDragDrop,
  CdkDrag,
  CdkDropList,
  CdkDropListGroup,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';
import { TablerIconsModule } from 'angular-tabler-icons';
import { MaterialModule } from 'src/app/material.module';
import { GroupService } from 'src/app/services/api/group.service';
import { StudentService } from 'src/app/services/api/student.service';
import { LevelOfStudy } from 'src/app/models/level-of-study';
import { LevelsOfStudyService } from 'src/app/services/levels-of-study.service';
import { PageEvent } from '@angular/material/paginator';
import { PagedRequest } from 'src/app/models/wrappers/PagedRequest';
import { PagedResult } from 'src/app/models/wrappers/PagedResult';
import { StudentFilter } from 'src/app/models/filters/studentFilter';
import { MatDialog } from '@angular/material/dialog';
import { DefineStudentPaymentPlanDialogComponent } from 'src/app/components/payment-plans/define-student-payment-plan-dialog/define-student-payment-plan-dialog.component';
import { PaymentPlan } from 'src/app/models/payment-plan';
import { StudentEnrollment } from 'src/app/models/student-enrollment';
import { EnrollmentService } from 'src/app/services/api/enrollment.service';
import { HotToastService } from '@ngxpert/hot-toast';
import { catchError } from 'rxjs';
import { GroupDetails } from 'src/app/models/group-details';
import { StudentBrief } from 'src/app/models/student-brief';

@Component({
  selector: 'app-enroll-students',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    TablerIconsModule,
    MaterialModule,
    CdkDropListGroup,
    CdkDropList,
    CdkDrag,
  ],
  templateUrl: './enroll-students.component.html',
  styleUrl: './enroll-students.component.scss'
})
export class EnrollStudentsComponent implements OnInit {
  groupId: number;
  toast = inject(HotToastService);
  groupData: GroupDetails
  levels: LevelOfStudy[] = []
  enrolledStudents: StudentBrief[] = [];
  availableStudents: StudentBrief[] = []
  filterForm: FormGroup
  newEnrollments: StudentEnrollment[] = []

  totalCount: number = 0;
  startIndex: number = 0
  limit: number = 5
  dialog = inject(MatDialog);
  constructor(private fb: FormBuilder, private enrollmentService: EnrollmentService,
    private levelOfStudyService: LevelsOfStudyService, private studentService: StudentService, private groupService: GroupService, private route: ActivatedRoute) {
    this.filterForm = this.fb.group({
      academicYear: [null],
      levelId: [null],
      firstName: [''],
      lastName: [''],
    });
  }
  ngOnInit(): void {
    this.loadLevels();
    const idParam = this.route.snapshot.paramMap.get('groupId');
    this.groupId = idParam ? parseInt(idParam, 10) : 0;
    this.loadGroupData();
    this.loadAvailableStudents(this.createFilters())
  }

  openDialog(student: StudentBrief) {
    let data = {
      student: student,
      groupId: this.groupId
    }
    const dialogRef = this.dialog.open(DefineStudentPaymentPlanDialogComponent, {
      data: data
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.newEnrollments.push(result)
      }
    });
  }
  createFilters(): StudentFilter {
    const filterData = this.filterForm.getRawValue();
    return {
      levelOfStudyId: filterData.levelId,
      academicYear: filterData.academicYear,
      firstName: filterData.firstName || null,
      lastName: filterData.lastName || null,
    };
  }
  async loadLevels() {
    this.levelOfStudyService.getAll().subscribe({
      next: (levels: any) => this.levels = levels
    });
  }
  loadGroupData() {
    this.groupService.getGroupDetails(this.groupId).subscribe(
      {
        next: (data: any) => {
          this.groupData = data
          this.enrolledStudents = this.groupData.students;
          console.log(this.groupData)
        },
        error: (err: any) => console.error(err)
      }
    )
  }
  createPagedRequest(filter: StudentFilter): PagedRequest<any> {
    return {
      startIndex: this.startIndex,
      limit: this.limit,
      filters: filter
    };
  }
  submit() {
    console.log(this.newEnrollments)
    this.enrollmentService.enrollMultiple(this.newEnrollments).pipe(
      this.toast.observe({
        loading: 'Upisivanje studenata...',
        success: () => 'Uspesan upis',
        error: () => 'Greska prilikom upisa'
      }),
    ).subscribe({
      error: (err: any) => console.error(err)
    })
  }
  loadAvailableStudents(filter: StudentFilter) {
    this.studentService.getBriefPagedData(this.createPagedRequest(filter)).subscribe(
      {
        next: (data: PagedResult<StudentBrief>) => {
          this.totalCount = data.totalCount
          this.availableStudents = data.items.filter(s => !(this.enrolledStudents.some(x => x.id == s.id)));
        }
      }
    );
  }

  drop(event: CdkDragDrop<StudentBrief[]>) {
    let curr = event.container.id;
    let prev = event.previousContainer.id;
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      let student = event.item.data;
      if (curr == "cdk-drop-list-1") {
        this.openDialog(student)
      }
      else {
        this.newEnrollments = this.newEnrollments.filter(e => e.studentId != student.id)
      }
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }
  quickEnroll(student: StudentBrief) {

  }
  search() {
    this.loadAvailableStudents(this.createFilters())
  }
  onPageChange(event: PageEvent, filter: StudentFilter) {
    this.limit = event.pageSize;
    this.startIndex = event.pageIndex;

    this.loadAvailableStudents(filter);
  }
}
