import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { User } from '../user';

@Component({
  selector: 'app-controll-access',
  templateUrl: './controll-access.component.html',
  styleUrls: ['./controll-access.component.scss']
})
export class ControllAccessComponent implements OnInit {

  isChecked: boolean;
  role = localStorage.getItem('role');

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit() {
  }

  ngAfterContentInit() {
    if (this.role == 'Admin') {
      this.isChecked = true;
    }
    else {
      this.isChecked = false;
    }
  }

  alterarRole() {
    let email = localStorage.getItem('email');
    if (this.isChecked == true) {
      localStorage.setItem('role', 'Admin');
      let role = localStorage.getItem('role');

      let data = { role, email };
      this.userService.alterar(data)
        .subscribe(() => {
          this.router.navigate(['book']);
        });
    }
    else {
      localStorage.setItem('role', 'User');
      let role = localStorage.getItem('role');

      let data = { role, email };
      this.userService.alterar(data)
        .subscribe(() => {
          this.router.navigate(['book']);
        });
    }
  }

  checkValue(event: any) {
    console.log(event);
  }

}
