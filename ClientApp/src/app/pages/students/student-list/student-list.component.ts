import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BaseTableComponent } from 'src/app/base/base-table-component';
import { Student } from 'src/app/models/student';
import { StudentService } from 'src/app/services/api/student.service';

@Component({
  selector: 'app-student-list',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTooltipModule,
    MatDialogModule,
    MatChipsModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSortModule
  ],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.scss'
})
export class StudentListComponent extends BaseTableComponent<Student, any> {
  displayedColumns: string[] = ['fullName', 'levelOfStudy', 'AcademicYear', 'email', 'phoneNumber', 'address', 'note', 'actions'];
  constructor(private studentService: StudentService) {
    super(studentService); // explicitly pass service to base class
  }
  ngOnInit(): void {
    this.loadTableData(this.getStudentFilter())
  }
  getStudentFilter() {
    return {}
  }
  onMenuAction(action: string, student: Student): void { }

}
