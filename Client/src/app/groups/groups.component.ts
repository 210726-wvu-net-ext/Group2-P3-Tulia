import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { Group } from '../models/group';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  groups: Group[] = [];

  constructor(private groupService: GroupService) { }

  ngOnInit(): void {
    this.getallgroups();
  }
  getallgroups(): void{
    this.groupService.getallGroups()
    .subscribe(groups => this.groups = groups);
  }

}
