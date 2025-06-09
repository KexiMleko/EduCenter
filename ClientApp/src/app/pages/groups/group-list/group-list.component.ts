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
import { Router } from '@angular/router';
import { BaseTableComponent } from 'src/app/base/base-table-component';
import { Group } from 'src/app/models/group';
import { GroupService } from 'src/app/services/api/group.service';

@Component({
  selector: 'app-group-list',
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
  templateUrl: './group-list.component.html',
  styleUrl: './group-list.component.scss'
})
export class GroupListComponent extends BaseTableComponent {

  displayedColumns: string[] = ['name', 'teacherFullName', 'subjectName', 'MaxNumberOfClasses', 'MaxNumberOfClassesLeft', 'groupCount', 'createdAt', 'actions'];
  constructor(private router: Router, private groupService: GroupService) {
    super(groupService);
  }
  ngOnInit(): void {
    this.loadTableData(this.getGroupFilter())
  }
  getGroupFilter() {
    return {}
  }
  onMenuAction(action: string, group: Group): void {
    console.log(group.id);
    if (action == "enroll") {
      this.router.navigate(['/groups', group.id, 'enrollments'])
    }
  }
}
