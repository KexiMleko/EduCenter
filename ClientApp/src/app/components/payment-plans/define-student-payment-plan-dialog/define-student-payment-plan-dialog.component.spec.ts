import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefineStudentPaymentPlanDialogComponent } from './define-student-payment-plan-dialog.component';

describe('DefineStudentPaymentPlanDialogComponent', () => {
  let component: DefineStudentPaymentPlanDialogComponent;
  let fixture: ComponentFixture<DefineStudentPaymentPlanDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DefineStudentPaymentPlanDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DefineStudentPaymentPlanDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
