import { PaymentPlan } from "./payment-plan";

export interface StudentEnrollment {

  studentId: number,
  groupId: number,
  paymentPlan: PaymentPlan
}
