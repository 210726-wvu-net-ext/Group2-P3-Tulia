import { Component, OnInit } from '@angular/core';
import { Post } from '../models/post';
import { PostsService } from '../posts.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  post!: Post;
  posts: Post[] = [];
  constructor(
    private postService: PostsService
  ) { }

  ngOnInit(): void {
    this.postService.getAllPosts(this.post).subscribe(posts => this.posts = posts);
  }

}
