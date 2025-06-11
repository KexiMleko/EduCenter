export interface PaymentPlan {
  totalAmount: number,
  numberOfPayments: number
  note: string,
  status: PaymentStatus
}

enum PaymentStatus {
  Pending = 0,
  Completed = 1,
  Refunded = 3,
  Cancelled = 4,
}
