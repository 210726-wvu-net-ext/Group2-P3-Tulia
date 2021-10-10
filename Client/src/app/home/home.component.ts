import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../models/user';
import { UserDetail } from '../models/userdetail';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @Input() user!: User;
  userdetail!: UserDetail;
  //user!: User;
  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) {
    this.user = this.userService.userValue;
  }
  getUserwithGroup() {
    this.userService.getUserwithGroup(this.user.id)
      .subscribe(
        userdetail => {
          this.userdetail = userdetail;
        },
        groups => {
          groups = this.userdetail?.groups
        },
      );
  }
  getUserwithMembership() {
    this.userService.getUserwithGroup(this.user.id)
      .subscribe(
        userdetail => {
          this.userdetail = userdetail;
        },
        memberships => {
          memberships = this.userdetail?.memberships
        },
      );
  }

  getUser(): void {

    this.userService.getUser(this.user.id)
      .subscribe(user => this.user = user);
  }


  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.getUser();
    });
    this.getUserwithGroup();
    this.getUserwithMembership();
  }

}




