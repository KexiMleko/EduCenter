import { CommonModule } from '@angular/common';
import {
  CdkDragDrop,
  CdkDrag,
  CdkDropList,
  CdkDropListGroup,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';
import { TablerIconsModule } from 'angular-tabler-icons';
import { MaterialModule } from 'src/app/material.module';
import { GroupDetails } from 'src/app/models/GroupDetails';
import { GroupService } from 'src/app/services/api/group.service';
import { StudentBrief } from 'src/app/models/studentBrief';
import { StudentService } from 'src/app/services/api/student.service';

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
    CdkDrag
  ],
  templateUrl: './enroll-students.component.html',
  styleUrl: './enroll-students.component.scss'
})
export class EnrollStudentsComponent implements OnInit {
  groupId: number;
  groupData: GroupDetails
  enrolledStudents: StudentBrief[] = [];
  availableStudents: StudentBrief[] = []
  constructor(private studentService: StudentService, private groupService: GroupService, private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('groupId');
    this.groupId = idParam ? parseInt(idParam, 10) : 0;
    this.loadGroupData();
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
  loadAvailableStudents() {
    this.studentService.getStudentByGroup(this.groupId).subscribe(
      {
        next: (data: any) => {
          this.availableStudents = data;
        }
      }
    );
  }
  todo = ['Get to work', 'Pick up groceries', 'Go home', 'Fall asleep'];


  drop(event: CdkDragDrop<StudentBrief[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }
}
