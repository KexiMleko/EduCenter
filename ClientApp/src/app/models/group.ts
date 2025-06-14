export interface Group {
  id: number;
  name: string;
  teacherId: number;
  teacherFirstName: string;
  teacherLastName: string;
  subjectId: number;
  subjectName: string;
  isActive: boolean;
  maxNumberOfClasses: number;
  numberOfClassesLeft: number;
  createdAt: Date;
  updatedAt: Date;
  studentCount: number;
}
