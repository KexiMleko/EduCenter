import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleGroupSessionComponent } from './schedule-group-session.component';

describe('ScheduleGroupSessionComponent', () => {
  let component: ScheduleGroupSessionComponent;
  let fixture: ComponentFixture<ScheduleGroupSessionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleGroupSessionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleGroupSessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
