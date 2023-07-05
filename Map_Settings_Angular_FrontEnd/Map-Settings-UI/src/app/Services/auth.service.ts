import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, shareReplay } from 'rxjs';
import { environment } from 'src/enviroments/enviroments';
import { ApiPaths } from 'src/app/Enums/api-paths';
import * as moment from "moment";
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router:Router) {
  }

  login(username:string, password:string ) {

    return this.http.post(environment.baseUrl+ApiPaths.user+ApiPaths.userLogin,
      {username, password})
      .pipe(
          shareReplay(),
          map(res => {
            this.setSession(res)
        })
      )
  }

  private setSession(authResult: any) {
    const expiresAt = moment(authResult.expiry);
    localStorage.setItem('id_token', authResult.token);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()) );
  }

  public logout() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("expires_at");

    this.router.navigateByUrl('/login');
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at") || '-1';
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }
}
