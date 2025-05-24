import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private api: ApiService, private router: Router) { }

  refreshTokens() {
    return this.api.get('auth/refresh');
  }
  login(username: string, password: string) {
    return this.api.post('auth/login', { username: username, password: password })
  }
  logout() {
    this.api.get('auth/logout').subscribe({
      next: () => {
        this.router.navigate(['/auth/login']);
      },
      error: (err: any) => {
        console.error(err);
      }
    });
  }
}
