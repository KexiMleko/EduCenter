import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleIndividualSessionComponent } from './schedule-individual-session.component';

describe('ScheduleIndividualSessionComponent', () => {
  let component: ScheduleIndividualSessionComponent;
  let fixture: ComponentFixture<ScheduleIndividualSessionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleIndividualSessionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleIndividualSessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
