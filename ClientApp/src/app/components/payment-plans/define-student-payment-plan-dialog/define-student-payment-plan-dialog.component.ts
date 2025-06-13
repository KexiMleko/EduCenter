import { Component, inject, Input, model, OnInit } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogTitle,
  MatDialogContent,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { PaymentPlanFormComponent } from '../payment-plan-form/payment-plan-form.component';
import { MatButtonModule } from '@angular/material/button';
import { TablerIconsModule } from 'angular-tabler-icons';
import { MatDividerModule } from '@angular/material/divider';
import { PaymentPlan } from 'src/app/models/payment-plan';
import { StudentEnrollment } from 'src/app/models/student-enrollment';
import { StudentBrief } from 'src/app/models/student-brief';

@Component({
  selector: 'app-define-student-payment-plan-dialog',
  imports: [
    MatDividerModule,
    TablerIconsModule,
    MatDialogModule,
    MatDialogTitle,
    MatDialogContent,
    MatButtonModule,
    PaymentPlanFormComponent
  ],
  templateUrl: './define-student-payment-plan-dialog.component.html',
  styleUrl: './define-student-payment-plan-dialog.component.scss'
})
export class DefineStudentPaymentPlanDialogComponent {
  studentPaymentPlan: StudentEnrollment
  readonly dialogRef = inject(MatDialogRef<DefineStudentPaymentPlanDialogComponent>);
  data: { student: StudentBrief, groupId: number } = inject(MAT_DIALOG_DATA);

  studentData = this.data.student
  groupId = this.data.groupId

  onFormSubmited(paymentPlan: PaymentPlan) {
    this.studentPaymentPlan = { studentId: this.studentData.id, groupId: this.groupId, paymentPlan: paymentPlan }
    this.dialogRef.close(this.studentPaymentPlan);
  }
}
