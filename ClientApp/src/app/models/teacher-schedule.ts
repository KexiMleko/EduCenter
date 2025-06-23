import { GroupSessionEvent } from './group-session-event';
import { IndividualSessionEvent } from './individual-session-event';

export interface TeacherSchedule {
  groupSessions: GroupSessionEvent[];
  individualSessions: IndividualSessionEvent[];
}
