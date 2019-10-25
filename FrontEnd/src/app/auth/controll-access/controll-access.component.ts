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

  data: User[] = [];
  displayedColumns: string[] = ['userId', 'fullName', 'role'];
  isLoadingResults = true;
  isChecked: boolean;
  role = localStorage.getItem('role');
  emailStorage = localStorage.getItem('email');
  currentUser: any;

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
    this.getUsers();
  }

  ngAfterContentChecked() {
    let i = this.data;
    // console.log(i.find(e => e.Email == this.emailStorage));
  }

  getUsers() {
    this.userService.getAll()
      .subscribe(users => {
        this.data = users;
        this.isLoadingResults = false;
        this.currentUser = this.data.find(d => d.email === this.emailStorage);
        if (this.currentUser != null) {

        }

      }, err => {
        this.isLoadingResults = false;
      });
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
