import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GroupService } from '../group.service';
import { Group } from '../models/group';

@Component({
  selector: 'app-group-detail',
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {

  group!: Group;

  constructor(private groupService: GroupService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getGroup();
  }
  //delete membership, NumGroups of user -1, MemberNumber of Group -1
  leaveGroup() {

  }
  getGroup(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.groupService.getGroupById(id)
      .subscribe(group => this.group = group);
  }
}
