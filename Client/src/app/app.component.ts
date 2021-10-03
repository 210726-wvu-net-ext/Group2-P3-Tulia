import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Welcome to Tulia';
  user!: User;
  isUser: boolean = false;

  constructor(
    public userService: UserService
    ) { }

  
  ngOnInit() {
    //this.userService.getUser()
    this.userService.user.subscribe((x: User) => this.user = x);
  }

  logout() {
    this.userService.logout();
    alert("You have logged out!");
}
}
