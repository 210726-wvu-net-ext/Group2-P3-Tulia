import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../models/user';
import { UserDetail } from '../models/userdetail';
import { GroupService } from '../group.service';
import { Membership } from '../models/membership';
import { Group } from '../models/group';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @Input() user!: User;
  userdetail!: UserDetail;
  member!: Membership;
  memberships: Membership[] = [];
  groups: Group[] = [];
  group!: Group;

  //user!: User;
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private groupService: GroupService
  ) {
    this.user = this.userService.userValue;
  }

  getUserwithGroup() {
    this.userService.getUserwithGroup(this.user.id)
      .subscribe(
        userdetail => {
          this.userdetail = userdetail;

          this.memberships = this.userdetail?.memberships;
          console.log(this.userdetail?.memberships);
          for (let membership of this.memberships) {
            this.groupService.GetMembership(membership.id).subscribe(
              member => {
                this.member = member,
                  console.log(member.id);
                this.group = this.member?.group,
                  console.log(this.member?.group)
              }

            );

          }
        },
        //memberships => {
        //  memberships = this.userdetail?.memberships;
        //  console.log("bla");
        //  for (let membership of memberships) {
        //    this.groupService.GetMembership(membership.id).subscribe(
        //      member => { this.member = member },
        //      group => { group = this.member?.group }
        //    );
        //  }
        //},
      );
  }
  //getMember(): void {
  //  this.groupService.GetMembership(1).subscribe(
  //    member => { this.member = member },
  //    group => { group = this.member?.group }
  //  );
  //}

  //getUserwithGroup() {
  //  this.userService.getUserwithGroup(this.user.id)
  //    .subscribe(
  //      userdetail => {
  //        this.userdetail = userdetail;
  //      },
  //      //groups => {
  //      //  groups = this.userdetail?.groups
  //      //},
  //    );
  //}
  getUser(): void {

    this.userService.getUser(this.user.id)
      .subscribe(user => this.user = user);
  }


  ngOnInit(): void {
    //this.route.params.subscribe(routeParams => {
    //  this.getUser();
    //});
    console.log(this.groups);
    console.log(this.memberships);
    this.getUserwithGroup();
    //this.getUserwithGroup();
    //this.getMember();
  }

}




