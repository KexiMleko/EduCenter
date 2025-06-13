import { MAT_DATE_FORMATS } from '@angular/material/core';

export const RS_DATE_TIME_FORMATS = {
  parse: {
    dateInput: 'yyyy-MM-ddTHH:mm',
  },
  display: {
    dateInput: 'DD.MM.yyyy HH:mm', // This controls the input display
    monthYearLabel: 'MMM yyyy',
    dateA11yLabel: 'dd.MM.yyyy HH:mm',
    monthYearA11yLabel: 'MMMM yyyy',
  },
};
