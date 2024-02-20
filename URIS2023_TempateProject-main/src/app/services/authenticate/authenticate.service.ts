import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { KORISNIK_URL } from 'app/constants';
import { Korisnik } from 'app/models/korisnik/korisnik';
import { Principal } from 'app/models/korisnik/principal';
import { TokenClass } from 'app/models/korisnik/token';
import { jwtDecode } from "jwt-decode";


@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  constructor(private httpClient: HttpClient, public router: Router) { }

  logIn(principal: Principal) {
    return this.httpClient.post(`${KORISNIK_URL}/authenticate`, principal)
    .subscribe((res: any) => {
      localStorage.setItem('access-token', res.token);

      this.router.navigate(['user-profile'])
    });
  };

  doLogout(){
    let removeToken = localStorage.removeItem('access-token');
    if (removeToken == null) {
      this.router.navigate(['login']);
    }
  }

  getToken() {
    return localStorage.getItem('access-token');
  }

  decodeToken():TokenClass{
    let token = this.getToken();
    try {
      return jwtDecode(token!)
    } catch(Error) {
      return new TokenClass();
    }
  }

  get isLoggedIn(){
    let authToken = localStorage.getItem('access-token');
    return authToken !== null ? true : false;
  }

}
