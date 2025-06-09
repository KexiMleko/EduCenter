import { StudentBrief } from "./studentBrief";

export interface GroupDetails {
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
  students: StudentBrief[];
  createdAt: Date;
  updatedAt?: Date | null;
  studentCount: number;
}
