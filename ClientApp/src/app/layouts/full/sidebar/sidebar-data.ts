import { NavItem } from './nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Home',
  },
  {
    displayName: 'Početna',
    iconName: 'home',
    route: '/home',
  },
  {
    divider: true,
    navCap: 'Apps',
  },
  {
    displayName: 'Djaci',
    iconName: 'solar:atom-line-duotone',
    route: '/students',
  },
  {
    displayName: 'Nastavnici',
    iconName: 'school',
    route: '/teachers',
  },
  {
    divider: true,
    navCap: 'Statistika',
  },
  {
    displayName: 'Popularnost predmeta',
    iconName: 'graph',
    route: '/teachers',
  },
  {
    displayName: 'Finansije',
    iconName: 'report-money',
    route: '/teachers',
  },
];
