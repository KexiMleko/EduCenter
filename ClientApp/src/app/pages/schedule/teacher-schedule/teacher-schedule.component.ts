import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { FullCalendarModule } from '@fullcalendar/angular';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { CalendarOptions, EventInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import interactionPlugin from '@fullcalendar/interaction';
import srLocale from '@fullcalendar/core/locales/sr';
import { ScheduleService } from 'src/app/services/api/schedule.service';
import { TeacherSchedule } from 'src/app/models/teacher-schedule';
import { GroupSessionEvent } from 'src/app/models/group-session-event';
import { IndividualSessionEvent } from 'src/app/models/individual-session-event';

@Component({
  selector: 'app-teacher-schedule',
  standalone: true,
  imports: [CommonModule, MatCardModule, FullCalendarModule],
  templateUrl: './teacher-schedule.component.html',
  styleUrl: './teacher-schedule.component.scss',
})
export class TeacherScheduleComponent implements OnInit {
  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;

  constructor(private scheduleService: ScheduleService) {}

  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, timeGridPlugin, listPlugin, interactionPlugin],
    initialView: 'dayGridMonth',
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,timeGridDay,listWeek',
    },
    events: [], // will be filled dynamically
    dateClick: (arg) => alert(`Datum: ${arg.dateStr}`),
    eventClick: (arg) => {
      const event = arg.event;
      const { sessionId, type, originalData } = event.extendedProps;
      alert(`Događaj: ${event.title}\nTip: ${type}\nID: ${sessionId}`);
      console.log('Original data:', originalData);
    },
    locale: 'sr',
    locales: [srLocale],
  };

  ngOnInit(): void {
    this.scheduleService.getCurrentUserSchedule().subscribe({
      next: (res: TeacherSchedule) => {
        console.log(res);
        const allEvents: EventInput[] = [];

        res.groupSessions.forEach((el: GroupSessionEvent) => {
          allEvents.push({
            title: el.sessionTitle,
            start: el.startTime,
            end: this.addMinutes(el.startTime, el.sessionDuration),
            extendedProps: {
              sessionId: el.sessionId,
              type: 'group',
              originalData: el,
            },
          });
        });
        res.individualSessions.forEach((el: IndividualSessionEvent) => {
          allEvents.push({
            title: el.sessionTitle,
            start: el.startTime,
            end: this.addMinutes(el.startTime, el.sessionDuration),
            extendedProps: {
              sessionId: el.sessionId,
              type: 'individual',
              originalData: el,
            },
          });
        });

        this.calendarOptions.events = allEvents;
      },
      error: (err: any) => console.error(err),
    });
  }

  private addMinutes(dateStr: string, minutes: number): string {
    const date = new Date(dateStr);
    date.setMinutes(date.getMinutes() + minutes);
    return date.toISOString();
  }
}
