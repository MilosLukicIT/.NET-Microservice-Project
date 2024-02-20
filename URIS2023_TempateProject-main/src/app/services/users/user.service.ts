import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { KORISNIK_URL } from 'app/constants';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public GetAllKorisnik(): Observable<any> {
    return this.httpClient.get(`${KORISNIK_URL}`);
  }

  public GetKorisnikById(korinsikId: string): Observable<any> {
    return this.httpClient.get(`${KORISNIK_URL}/`+ korinsikId)
  }

  



}
