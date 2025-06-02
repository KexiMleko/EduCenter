
# Frontend Decisions

## Introduction  
The frontend is built using Angular 19 with a modular, feature-folder structure. The priority is delivering a fast, responsive UI that is easy to maintain and extend by a solo developer. The app follows SPA principles and communicates with the backend via REST APIs.

## Architectural Approach  
**Style chosen:** Single Page Application (SPA) with modular feature-based folder structure.  

**Why I chose it:**  
- SPA provides smooth navigation without full page reloads, improving UX.  
- Angular CLI's free starter template offers an organized and scalable project structure out of the box.  
- Modular folder approach keeps related components, services, and models grouped, easing maintenance.  

**Personal note:**  
I considered state management libraries like NgRx but opted for simple service-based state handling to reduce complexity and speed up development.

## API Communication  
**Approach:** REST calls via Angular HttpClient, wrapped in services.  

**Why:**  
- Clear separation of concerns; services handle all API calls and logic.  
- Easy to mock and test (future-proof).  
- Compatible with backend’s RESTful ASP.NET Core API.  

## Authentication  
**Mechanism:** JWT stored in HttpOnly cookies with refresh token flow.  

**Why:**  
- HttpOnly cookies improve security by preventing client-side script access.  
- Refresh tokens ensure smooth user experience without frequent logins.  

**Personal note:**  
Authentication logic is handled centrally, with route guards protecting secured pages.

## Styling  
Scoped CSS per component using Angular’s built-in view encapsulation ensures no style leakage and modular, maintainable CSS.

## Tradeoffs  
- No formal unit testing currently due to time constraints.  
- State management is kept simple to prioritize speed over advanced features.  
- UI library/framework usage is minimal to keep bundle size small and maintain control.
