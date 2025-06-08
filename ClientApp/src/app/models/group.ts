export interface Group {
  Id: number;
  Name: string;
  TeacherId: number;
  TeacherFirstName: string;
  TeacherLastName: string;
  SubjectId: number;
  SubjectName: string;
  IsActive: boolean;
  MaxNumberOfClasses: number;
  NumberOfClassesLeft: number;
  CreatedAt: Date;
  UpdatedAt: Date;
  StudentCount: number;
}
