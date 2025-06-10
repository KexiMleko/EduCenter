import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatOptionModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TablerIconsModule } from 'angular-tabler-icons';
import { BaseTableComponent } from 'src/app/base/base-table-component';
import { MaterialModule } from 'src/app/material.module';
import { StudentFilter } from 'src/app/models/filters/studentFilter';
import { LevelOfStudy } from 'src/app/models/level-of-study';
import { Student } from 'src/app/models/student';
import { StudentService } from 'src/app/services/api/student.service';
import { LevelsOfStudyService } from 'src/app/services/levels-of-study.service';

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
    MatSortModule,
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
    MatInputModule,
    ReactiveFormsModule,
    TablerIconsModule,
    MaterialModule
  ],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.scss'
})
export class StudentListComponent extends BaseTableComponent<Student, any> {
  displayedColumns: string[] = ['fullName', 'levelOfStudy', 'AcademicYear', 'email', 'phoneNumber', 'address', 'note', 'actions'];
  filterForm: FormGroup;
  levels: LevelOfStudy[] = [];
  constructor(private levelService: LevelsOfStudyService, private studentService: StudentService, private fb: FormBuilder) {
    super(studentService);
    this.filterForm = this.fb.group({
      academicYear: [null],
      levelId: [null],
      firstName: [''],
      lastName: [''],
    });
  }

  ngOnInit(): void {
    this.loadLevels()
    this.loadTableData(this.createFilters())
  }
  async loadLevels() {
    this.levelService.getAll().subscribe({
      next: (levels: any) => this.levels = levels
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
  onMenuAction(action: string, student: Student): void { }
  search() {
    this.loadTableData(this.createFilters())
  }
}
