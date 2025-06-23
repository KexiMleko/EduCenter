export interface GroupSessionEvent {
  sessionId: number;
  groupId: number;
  sessionDuration: number;
  groupName: string;
  classroomName: string;
  sessionTitle: string;
  startTime: string; // Use ISO 8601 format (e.g. "2025-06-23T12:00:00")
}
