import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user';

@Component({
  selector: 'app-controll-access',
  templateUrl: './controll-access.component.html',
  styleUrls: ['./controll-access.component.scss']
})
export class ControllAccessComponent implements OnInit {

  displayedColumns: string[] = ['userId', 'fullName', 'role', 'checkRole'];
  idStorage = localStorage.getItem('id');
  idCheckbox: number[] = [];
  isLoadingResults = true;
  data: User[] = [];
  currentUser: any;
  isChecked: any;
  roleData: any;
  isAdmin: any;

  adminId: any;
  userId: any;

  constructor(private router: Router, private userService: UserService) {
  }

  ngOnInit() {

  }

  ngAfterContentInit() {
    this.getUsers();
  }

  getUsers() {
    this.userService.getAll()
      .subscribe(users => {
        this.data = users;
        this.isAdmin = this.data.filter(e => e.role === "Admin");
        this.isLoadingResults = false;
        this.currentUser = this.data.find(d => d.userId.toString() === this.idStorage);
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  alterarRole() {
    debugger;
    let totalPart = this.idCheckbox.toString();
    let p = totalPart.split(',', 3);
    for (let l = 0; l < this.idCheckbox.length; l++) {
      let part = p[l].slice(0 + l, 1 + l);
      let IdUser = part[l].slice(0, 1);
      let Role = part[l].slice(2, 7);
      if (this.isChecked) {
        this.roleData = {
          IdUser: IdUser,
          Role: Role
        };
        this.router.navigate(['book']);
      }
      else {
        this.roleData = {
          IdUser: IdUser,
          Role: Role
        };
        this.router.navigate(['book']);
      }
      this.userService.alterar(this.roleData).subscribe(() => {
        console.log("Usuário alterado");
      });
    }
  }

  checkValue(event: any) {
    console.log(event);
    this.isChecked = event.checked;
    if (event.source._checked) {
      this.idCheckbox.push(event.source._value);
    }
    else {
      this.idCheckbox.pop();
    }
  }
}