import { AfterViewInit, Component, inject } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
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
  rememberMe = false;
  showPassword = false;
    private router = inject(Router);
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

  handleGoogleLogin(response: any) {

    console.log(response);

    const idToken = response.credential;

    // לשלוח לבקאנד
  }
    goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}