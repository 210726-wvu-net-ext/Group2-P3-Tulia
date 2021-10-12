import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
import { UserService } from '../user.service';
import { User } from '../models/user';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-group-detail',
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {
  user!: User;
  group!: Group;
  submitted = false;
  groups: Group[] = [];
  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService) { this.user = this.userService.userValue; }

  ngOnInit(): void {
    this.getGroup();
  }
  //delete membership, NumGroups of user -1, MemberNumber of Group -1
  leaveGroup(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.groupService.deleteMembership(this.user.id, id);
    this.groupService.updateGroupWhenLeave(id, this.group);
    this.userService.updateUserWhenLeave(id, this.user)
  }

  getGroup(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.groupService.getGroupIncludingPosts(id)
      .subscribe(
        group => {
          this.group = group;
        },
        posts =>
          posts = this.group.posts
      );
  }


  onSubmit() {
    this.submitted = true;
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.groupService.updateGroupWhenLeave(id, this.group).subscribe(data => { console.log("membernumber -1") });
    this.userService.updateUserWhenLeave(this.user.id, this.user).subscribe(data => { console.log("groupnumber -1") });
    this.groupService.deleteMembership(this.user.id, id).subscribe(
      confirm => {
        this.router.navigate(['../../'], { relativeTo: this.route });
        alert("you left this group!");
      }
    )
  }

}
