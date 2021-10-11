import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Post } from './models/post';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private postUrl = "https://localhost:44326/api/Post";
  httpOptions = {
    headers: new HttpHeaders({'Content-Type' : 'application/json'})
  };

  constructor(
    private http: HttpClient,
    private router: Router,
  ) { }

  getAllPosts(post: Post){
    return this.http.get<Post[]>(`${this.postUrl}/all`);
  }

  createPost(post: Post){
    this.http.post(`${this.postUrl}/create`, post).subscribe(data => {
      console.log(data);
    })
  }
  handleError1(error: HttpErrorResponse){
    return throwError(error.error);
  }
}
