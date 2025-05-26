
# EduCentar Feature List

This document outlines the basic and advanced features that can be implemented based on the provided database schema.

---

## Basic Features

### User & Role Management
- User registration and login
- Role-based access control (Admin, Teacher, Student)
- Profile editing (name, email, phone)
- Password management (reset/update)
- Session authentication with refresh tokens

### Student Management
- Full CRUD for student records
- View student progress via attendance and performance
- Enroll students into groups
- Assign payment plans to students

### Teacher & Subject Management
- CRUD operations for teachers
- Assign subjects and levels to teachers
- View assigned students and groups

### Subject & Level Management
- CRUD for subjects
- CRUD for levels of study

### Group & Session Management
- Create groups per subject and teacher
- Manage group sessions
- Track attendance and performance for each session
- Track remaining number of classes per group

### Individual Sessions
- Book individual sessions with teachers
- Link sessions to teachers, students, subjects, and payment plans

### Payments
- Create and manage payment plans
- Track payments linked to plans
- Identify unpaid or overdue payments

---

## Advanced Features

### Analytics & Reporting
- Generate student performance reports
- Analyze attendance statistics
- Teacher effectiveness reports
- Session utilization tracking

### Scheduling & Notifications
- Weekly timetable view for students and teachers
- Conflict detection for overlapping sessions
- Email/SMS reminders for upcoming sessions

### Payment & Finance Management
- Auto-generate invoices for students
- Send payment reminders
- Generate revenue reports by teacher, group, or date range

### CRM-style Features
- Communication logs with parents
- Internal notes and flags on students
- Track student engagement (login, attendance, performance trends)

### Admin Dashboard
- Summary widgets (active students, upcoming sessions, unpaid plans)
- Searchable, paginated list views for sessions, users, payments

### Public/Student Portal
- View personal schedule
- Download payment history
- Submit feedback for sessions
- Request enrollment into groups

