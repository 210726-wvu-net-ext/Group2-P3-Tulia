import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { User } from './models/user';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  private usersUrl = 'https://localhost:44365/api/users';

  constructor(
    private http: HttpClient
  ) { }

  getUser(id: number): Observable<User>
  {
    const url = `${this.usersUrl}/${id}`;
    return this.http.get<User>(url);
  }

  addUser(user: User): Observable<User>{
    return this.http.post<User>(this.usersUrl, user, this.httpOptions);
  }

  updateUser(id: number, user: User): Observable<any> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.put<User>(url, user, this.httpOptions);
  }

  deleteUser(id: number): Observable<User> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.delete<User>(url, this.httpOptions);
  }
  
}
