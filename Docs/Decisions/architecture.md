# Architecture

## 1. Overview

This project follows a monolithic architecture with a single backend service exposing REST APIs consumed by a Single Page Application (SPA) frontend.
The backend handles business logic, data access, and authentication.

Microservices would have been a decent learning experience, but would provide an unnecessary overhead when it comes to development of this app.
Though they could be beneficial if the app gets multiple clients that have differences in some parts of business logic.

## 2. Architectural Style

A modular monolithic architecture was chosen to simplify development and deployment given the project's scope and team size (1 person :D).
This approach keeps all application logic within a single deployable unit while maintaining a clear internal structure through feature-based modularization.

## 3. System Components

- **Frontend:** SPA client built with Angular responsible for user interface and interactions.
- **Backend:** API service managing business logic, data persistence, and security.
- **Database:** Relational database for structured data storage.
- **Deployment:** Will be self-hosted on a Proxmox VE server using a bare-metal virtualized environment.
   The application will run on a dedicated VM with a static IP, reverse proxy, and HTTPS configured. Containerization and CI/CD are planned for future iterations.

## 4. Communication and Integration

Frontend and backend communicate via RESTful HTTP APIs over HTTPS. As an industry standard this is the chosen type of integration.

## 5. Non-functional Considerations

- Maintainability through modular design.
- Performance sufficient for expected usage levels.
- Security ensured by token-based authentication.
- Scalability can be addressed by future architectural changes if necessary.

## 6. Alternatives Considered

Microservices architecture was evaluated but deemed too complex for the current project scope and resources.
