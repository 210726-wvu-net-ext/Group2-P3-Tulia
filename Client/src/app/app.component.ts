import { Component } from '@angular/core';
import { UserService } from './user.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Welcome to Tulia';

  constructor(
    private userService: UserService
    ) { }
  ngOnInit() {
    //this.userService.getUser()
  }
}
