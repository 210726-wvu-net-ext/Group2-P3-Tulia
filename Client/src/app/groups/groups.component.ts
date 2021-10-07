import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
import { User } from '../models/user';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  user!: User;
  group!: Group;
  id: number | any;
  groups: Group[] = [];
  submitted = false;
  loading = false;

  form: FormGroup = new FormGroup({

    userId: new FormControl(''),
    groupId: new FormControl('')
  });
  
  constructor(
    private groupService: GroupService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public userService: UserService
    ) { this.user = this.userService.userValue;}

  ngOnInit(): void {
    this.getallgroups();
    this.form = this.formBuilder.group({
      //userId: ['', Validators.required],
      userId: [this.user.id],
      groupId: ['']
    })
  };

  getallgroups(): void{
    this.groupService.getallGroups()
    .subscribe(groups => this.groups = groups);
  }

  //OnInput(event: any) {
  //  this.groupId = event.target.value;
  //  }

  //updateGroup():void{
    //const id = Number(this.route.snapshot.paramMap.get('id'));
    //if (this.group && this.groupId) {
      //this.groupService.updateGroup(this.form.value.groupId, this.group); 
    //}
  //}

  
  onSubmit() {
    
    this.submitted = true;
    //stop here if form is invalid
    //if (this.form.invalid) {
    //    return;
    //}
    this.loading=true;
    this.groupService.updateGroup(this.form.value.groupId, this.group).subscribe(data=>{alert("yah!")});
    this.groupService.CreateMembership(this.form.value)
        .pipe(first())
        .subscribe(
          data => {
            this.router.navigate(['../groupDetail'], {relativeTo: this.route});
            
            alert("Register successfully!");
            
          },
          error => {
            //ifmember >40
            this.loading = false;
            alert(error);
          }
        );
    
    
  }


}