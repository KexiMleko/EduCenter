import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { PaymentPlan } from 'src/app/models/payment-plan';

@Component({
  selector: 'app-payment-plan-form',
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
  templateUrl: './payment-plan-form.component.html',
  styleUrl: './payment-plan-form.component.scss'
})
export class PaymentPlanFormComponent {
  planForm: FormGroup;
  @Output() paymentPlanData = new EventEmitter<PaymentPlan>();
  constructor(private fb: FormBuilder) {
    this.planForm = this.fb.group({
      totalAmount: [null, Validators.required],
      numberOfPayments: [1, Validators.required],
      note: [null],
      status: [0, Validators.required],
    });
  }
  onSubmit() {
    let planData = this.planForm.getRawValue();
    this.paymentPlanData.emit(planData);
  }
}

