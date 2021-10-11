import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
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
  groups: Group[] = [];
  constructor(
    private postService: PostsService,
    private groupService: GroupService
  ) { }

  ngOnInit(): void {
    this.postService.getAllPosts(this.post).subscribe(posts => this.posts = posts);
    this.getAllGroups();
    // get group names
  }

  getAllGroups(): void {
    this.groupService.getallGroups().subscribe(groups => this.groups = groups);
  }

}
