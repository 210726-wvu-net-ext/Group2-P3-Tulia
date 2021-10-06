import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Router, Routes } from '@angular/router';
import { Group } from './models/group';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private groupsUrl = 'https://localhost:44326/api/Group';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private router: Router,
  ) { }
  createGroup(group: Group){
    return this.http.post<Group>(`${this.groupsUrl}/create`, group, this.httpOptions).pipe
    (catchError(this.handleError1));
  }
  handleError1(error: HttpErrorResponse){
    return throwError(error.error);
  }

  getallGroups(): Observable<Group[]>{
    return this.http.get<Group[]>(`${this.groupsUrl}/all`)
  }
}
