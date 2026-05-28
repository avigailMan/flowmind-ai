import { AfterViewInit, Component, inject, ElementRef, ViewChild } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

declare const google: any;

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    CardModule,
    ButtonModule,
    DividerModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    CheckboxModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements AfterViewInit {
    @ViewChild('googleBtn') googleBtn!: ElementRef<HTMLDivElement>;
  private authService = inject(AuthService);
  rememberMe = false;
  showPassword = false;
    private router = inject(Router);

  loginForm = {
    email: '',
    password: ''
  };

  registerForm = {
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    preferredCurrency: 'ILS'
  };
  ngAfterViewInit(): void {
    google.accounts.id.initialize({
      client_id: '943512359144-k2o632t6jnqqv8hhcklaqptlsdkcchea.apps.googleusercontent.com',
      callback: (response: any) => this.handleGoogleLogin(response)
    });

    google.accounts.id.renderButton(
      document.getElementById('googleBtn'),
      {
        theme: 'outline',
        size: 'large',
        width: 420
      }
    );
  }

    goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
   login(): void {
    this.authService.login(this.loginForm).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: err => console.error('Login failed', err)
    });
  }

  register(): void {
    this.authService.register(this.registerForm).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: err => console.error('Register failed', err)
    });
  }

  private loadGoogleButton(): void {
    if (!this.googleBtn?.nativeElement || typeof google === 'undefined') {
      setTimeout(() => this.loadGoogleButton(), 300);
      return;
    }

    this.googleBtn.nativeElement.innerHTML = '';

    google.accounts.id.initialize({
      client_id: 'YOUR_GOOGLE_CLIENT_ID',
      callback: (response: any) => this.handleGoogleLogin(response)
    });

    google.accounts.id.renderButton(this.googleBtn.nativeElement, {
      theme: 'outline',
      size: 'large',
      width: 420,
      text: 'signin_with',
      shape: 'rectangular',
      logo_alignment: 'left'
    });
  }

  private handleGoogleLogin(response: any): void {
    const idToken = response.credential;

    this.authService.loginWithGoogle(idToken).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: err => console.error('Google login failed', err)
    });
  }

}