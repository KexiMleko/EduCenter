export interface IndividualSessionEvent {
  sessionId: number;
  studentId: number;
  sessionDuration: number;
  classroomName: string;
  subjectName: string;
  sessionTitle: string;
  startTime: string; // Use ISO 8601 format
}
