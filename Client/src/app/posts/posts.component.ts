import { Component, Input, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
import { Post } from '../models/post';
import { User } from '../models/user';
import { PostsService } from '../posts.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  post!: Post;
  user!: User;
  time: string = new Date().toISOString().slice(0, 19).replace('T', ' ');
  @Input() createdPost: Post;
  posts: Post[] = [];
  groups: Group[] = [];
  constructor(
    private postService: PostsService,
    private groupService: GroupService,
    private userService: UserService
  ) { 
    this.user = this.userService.userValue;
    this.createdPost = {body:"", userId: this.user.id, title:"", groupId:1}
  }

  ngOnInit(): void {
    this.postService.getAllPosts(this.post).subscribe(posts => this.posts = posts);
  }

  submitPost(): void {
    this.postService.createPost(this.createdPost);
  }

  getAllGroups(): void {
    this.groupService.getallGroups().subscribe(groups => this.groups = groups);
  }

}
