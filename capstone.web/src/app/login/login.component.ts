import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private userService: UserService, private router: Router) {}

  login(): void {
    this.userService.login(this.username, this.password).subscribe(
      response => {
        this.userService.setToken(response.token);
        this.router.navigate(['/']); // Navigate to the home page or dashboard
      },
      error => {
        this.errorMessage = 'Invalid username or password.';
      }
    );
  }
}