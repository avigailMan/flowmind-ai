import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: '../login/login.css'
})
export class Register {
  private authService = inject(AuthService);
  private router = inject(Router);

  showPassword = false;

  registerForm = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    preferredCurrency: 'ILS'
  };

  register(): void {
    this.authService.register(this.registerForm).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: err => console.error('Register failed', err)
    });
  }
}