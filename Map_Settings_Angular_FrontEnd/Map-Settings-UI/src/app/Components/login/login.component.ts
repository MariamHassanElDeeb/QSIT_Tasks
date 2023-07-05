import { Component,AfterContentInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements AfterContentInit {

  formlogin:FormGroup;
  loginStatus = 'Pending';

  constructor(private fb:FormBuilder,
    private authService: AuthService,
    private router: Router,
    titleService:Title) {

      if (authService.isLoggedIn()){
        router.navigateByUrl('/');
      }

      this.formlogin = this.fb.group({
          username: ['',Validators.required],
          password: ['',Validators.required]
      });

      titleService.setTitle('MapCreation - Login');

    }

  ngAfterContentInit(): void {
    const togglePassword = document.querySelector("#togglePassword");
    const password = document.querySelector("#password-input");

    if (togglePassword && password)
      togglePassword.addEventListener("click", function () {

        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the eye icon
        togglePassword.classList.toggle('fa-eye');
        togglePassword.classList.toggle('fa-eye-slash');
      });
  }

  public get usernameValid() : boolean {
    return this.formlogin.controls["username"].valid;
  }

  public get passwordValid() : boolean {
    return this.formlogin.controls["password"].valid;
  }

  login() {
    const val = this.formlogin.value;

    if (this.formlogin.valid) {

      this.loginStatus = 'Created'

      this.authService.login(val.username, val.password)
        .subscribe({
          next: () => {
            this.router.navigateByUrl('/create');
          },
          error: () => {
            this.loginStatus = 'Failed';
          }
        });
    }
  }
}