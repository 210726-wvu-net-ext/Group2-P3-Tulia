import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  user!: User;

  constructor(
    private router: Router,
    private userService: UserService
  ) {
    this.user = this.userService.userValue;
    }
  ngOnInit(): void {
  }
}




