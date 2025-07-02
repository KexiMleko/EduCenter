import { NavItem } from './nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Dashboard',
  },
  {
    displayName: 'Početna',
    iconName: 'home',
    route: '/home',
  },

  {
    divider: true,
    navCap: 'Korisnici',
  },
  {
    displayName: 'Korisnici',
    iconName: 'users',
    children: [
      {
        displayName: 'Lista korisnika',
        route: '/users',
        iconName: 'list',
      },
      {
        displayName: 'Dodaj korisnika',
        route: '/users/add',
        iconName: 'user-plus',
      },
      {
        displayName: 'Role i prava',
        route: '/users/roles',
        iconName: 'shield-lock',
      },
    ],
    subItemIcon: true,
  },
  {
    navCap: 'Učenici',
  },
  {
    displayName: 'Učenici',
    iconName: 'id',
    children: [
      {
        displayName: 'Lista učenika',
        route: '/students',
        iconName: 'list',
      },
      {
        displayName: 'Dodaj učenika',
        route: '/students/add',
        iconName: 'user-plus',
      },
      {
        displayName: 'Upis u grupe',
        route: '/students/enroll',
        iconName: 'user-check',
      },
      {
        displayName: 'Uplate',
        route: '/students/payments',
        iconName: 'cash',
      },
      {
        displayName: 'Napredak',
        route: '/students/progress',
        iconName: 'chart-line',
      },
    ],
    subItemIcon: true,
  },

  {
    divider: true,
    navCap: 'Grupe i časovi',
  },
  {
    displayName: 'Grupe',
    iconName: 'layout-grid',
    route: '/groups',
  },

  {
    displayName: 'Kreiraj grupu',
    iconName: 'layout-grid-add',
    route: '/group/add',
  },
  {
    displayName: 'Časovi',
    iconName: 'calendar',
    route: '/groupSessions/schedule',
  },
  {
    displayName: 'Individualni časovi',
    iconName: 'user-circle',
    route: '/individualSessions/schedule',
  },

  {
    divider: true,
    navCap: 'Predmeti i nivoi',
  },
  {
    displayName: 'Predmeti',
    iconName: 'book',
    route: '/subjects',
  },
  {
    displayName: 'Nivoi',
    iconName: 'stairs',
    route: '/levelsOfStudy/add',
  },

  {
    divider: true,
    navCap: 'Finansije',
  },
  {
    displayName: 'Uplate i planovi',
    iconName: 'cash',
    route: '/payments',
  },
  {
    displayName: 'Fakture i izveštaji',
    iconName: 'report-money',
    route: '/payments/reports',
  },

  {
    divider: true,
    navCap: 'Statistika',
  },
  {
    displayName: 'Analitika',
    iconName: 'chart-bar',
    children: [
      {
        displayName: 'Prisustvo',
        route: '/analytics/attendance',
        iconName: 'calendar-check',
      },
      {
        displayName: 'Efikasnost nastavnika',
        route: '/analytics/teachers',
        iconName: 'award',
      },
      {
        displayName: 'Iskorišćenost časova',
        route: '/analytics/sessions',
        iconName: 'clock-hour-10',
      },
    ],
    subItemIcon: true,
  },
];
