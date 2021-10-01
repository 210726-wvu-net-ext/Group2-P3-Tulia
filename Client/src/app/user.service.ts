import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { User } from './models/user';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Router, Routes } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private userSubject: BehaviorSubject<User> | any;
  public user: Observable<User> | any;
  

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  private usersUrl = 'https://localhost:44326/api/User';

  constructor(
    private http: HttpClient,
    private router: Router,
  ) { 
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  getUser(id: number): Observable<User>
  {
    const url = `${this.usersUrl}/${id}`;
    return this.http.get<User>(url);
  }
  //getUsers(): Observable<User>
  //{
  //  return this.http.get<User[]>(this.usersUrl);
  //}

  addUser(user: User): Observable<User>{
    return this.http.post<User>(`${this.usersUrl}/register`, user, this.httpOptions).pipe
    (catchError(this.handleError1));
  }
  handleError1(error: HttpErrorResponse){
    return throwError(error.message);
  }

  updateUser(id: number, user: User): Observable<any> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.put<User>(url, user, this.httpOptions)
  }

  

  deleteUser(id: number): Observable<User> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.delete<User>(url, this.httpOptions);
  }
  
  login(username: string, password: string) {
    return this.http.post<User>(`${this.usersUrl}/authenticate`, { username, password })
        .pipe(map(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('user', JSON.stringify(user));
            this.userSubject.next(user);
            return user;
        }));
}

logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/account/login']);
}
}