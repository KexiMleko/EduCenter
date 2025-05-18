# Backend

## 1. Introduction

Brief summary of what powers the backend, the key priorities (performance, simplicity, long-term structure), and general overview of chosen tech.

> The backend is built in ASP.NET Core and structured around vertical slices with CQRS.
   Given the app’s read-heavy nature and solo/team-of-one development, I’ve chosen a mix of high-performance tools and pragmatic patterns that avoid over-architecting.

---

## 2. Architectural Approach

### Style Chosen: Vertical Slice Architecture (CQRS-based)

Why I chose it:
- Clear separation between features
- Faster navigation
- Less ceremony than Clean Architecture, more focus on functionality
- Keeps things modular without over-complication

Personal note:
> I originally considered Clean Architecture, but after a few iterations I found it too abstract for this type of app.
   I wanted something lighter and faster to navigate.
   Based on my past experience with Clean Architecture, I found that it often introduced unnecessary boilerplate for relatively simple functionality.

Tradeoff:
- It breaks traditional layering, so junior devs or newcomers might take longer to understand the structure (There were probably never going to be any newcomers).

---

## 3. API Design

### Style Chosen: Controller-based API (not Minimal APIs)

Reasoning:
- Easier to scale and organize
- Better tooling support (e.g., filters, attributes, model binding)
- Keeping them anemic would make it easy to navigate
- Better support for cross-cutting concerns like filters, middleware, and action pipelines.


Personal note:
> I thought to myself, if controllers were never a thing, what would i do with minimal apis, how would i structure them. 
   Did not take long to realize that i would eventually just create less readable and less organized controllers with minimal APIs.

Tradeoff:
- Slower (There would be no traffic that would make the difference)

---

## 4. Read/Write Separation

### Chosen Pattern: CQRS

- **Reads** use Dapper for speed and projection control
- **Writes** use EF Core with Repository + UoW for safer updates and consistency

Why:
> The app is read-heavy — lots of dashboards, lists, filters — so performance matters more for reads.
   Dapper gives me that. EF Core stays for writes because change tracking and UoW reduce my bug surface.

Tradeoff:
- Slight increase in complexity having two data access paths, but each is optimized for its job.

---

## 5. Domain + Controller Style

### Chosen: Anemic Models and Controllers

Why:
- No rich domain logic — just orchestrating persistence and validation
- Keeps models simple and more flexible when reshaping DTOs
- Easier navigation for controllers and more organization

---

## 6. Database Choice

### Chosen: PostgreSQL

Alternatives considered: SQL Server

Reasoning:
- Faster for complex queries
- Free, open-source, and better Linux compatibility (since I'm self-hosting)
- JSON support gives flexibility for unstructured fields if needed later

Quote:
> SQL Server was familiar, but PostgreSQL felt more aligned with the self-hosting story. It’s less bloated, no licensing, and better performance in my tests.

---

## 7. Final Thoughts


> The balance between performance and structure feels right.
   If the app grows, I’d consider fully adopting containers and CI/CD — but for now, this setup is clean, fast, and under control.
