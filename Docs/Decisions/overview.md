# Project Overview

<!--toc:start-->
- [Project Overview](#project-overview)
  - [Target Users / Use Case](#target-users-use-case)
  - [Priorities](#priorities)
  - [Main Design Goals](#main-design-goals)
  - [Constraints](#constraints)
  - [Summary of Major Decisions](#summary-of-major-decisions)
<!--toc:end-->

## Target Users / Use Case

The primary users are administrative staff and educators at private schools or similar educational institutions.
They will use the platform to manage financial records, track student progress, and coordinate day-to-day activities more efficiently.

## Priorities
1. Speed of product delivery, could be adjusted but as of right now its the top priority.
2. Reliability, persistance of data.
3. Learning potential, experimenting for the sake of learning.
4. Raw performance, this is not one of the top priorities because high traffic is not expected.
5. Codebase simplicity and maintanabillity.

## Main Design Goals

-  Optimized backend for performance and reliability
-  Fast and responsive UI
-  Maintainable and modular codebase
-  Secure authentication

## Constraints

- Solo developer project
- Limited development time (2 months)
- Read heavy application

## Details of Major Decisions

| Area        | Summary                                                                 |
|-------------|-------------------------------------------------------------------------|
| [Architecture](architecture.md) | Monolithic backend + SPA frontend, REST communication |
| [Backend](backend.md)       | ASP.NET Core,EF Core, raw SQL via dapper, Vertical slices architecture     |
| [Frontend](frontend.md)     | Angular 19, modular feature folders, service-based API calls |
| [Deployment](../deployment/README.md) | Self hosted on bare metal proxmox server|
