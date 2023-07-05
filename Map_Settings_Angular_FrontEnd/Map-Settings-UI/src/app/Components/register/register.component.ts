import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiPaths } from 'src/app/Enums/api-paths';
import { RegisterService } from 'src/app/Services/register.service';
import { environment } from 'src/enviroments/enviroments';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  constructor(private service: RegisterService, private router: Router, private route: ActivatedRoute, private http: HttpClient) { }
  user: any;
  newReg: any;

  Register(username : any , email : any, password :any ,department:any) {
    if (username && email &&  password &&  department) { // Check if all required fields have data
      this.newReg = {username  , email ,  password , department};
      //console.log('Registration...');
      const registrationDto = {
        userName: this.newReg.username.replace(/\s+/g, ''),
        password: this.newReg.password,
        email: this.newReg.email,
        department :this.newReg.department
      };
      //console.log(registrationDto);
      this.http.post(environment.baseUrl + ApiPaths.user+ApiPaths.userRegister, registrationDto).subscribe(
        (response) => {
          //console.log('Registration successful!');
         // console.log(response);
          // Navigate to login component after adding the new Register
          this.router.navigate(['/login']);
        },
        (error) => {
          //console.log('Error occurred during registration.');
          console.error(error);
          //console.log(error.status);
          //console.log(error.statusText);
          //console.log(error.error);
        }
      );

    }
  }

  ngOnInit(): void {
    // ...
  }
}

