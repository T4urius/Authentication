import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
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
  role = localStorage.getItem('role');
  isLoadingResults = true;
  data: User[] = [];
  currentUser: any;
  idCheckbox: number[] = [];
  isChecked: any;

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

  getUsers() {
    this.userService.getAll()
      .subscribe(users => {
        this.data = users;
        this.isLoadingResults = false;
        this.currentUser = this.data.find(d => d.userId.toString() === this.idStorage);
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  alterarRole() {
    for (let l = 0; l < this.idCheckbox.length; l++) {
      let data = this.idCheckbox;
      console.log(data);
      this.userService.alterar(data).subscribe(() => {
        console.log(data);
      });
    }
  }

  checkValue(event: any) {
    if (event.checked) {
      this.idCheckbox.push(event.source.id);                                                                                                                                                                                                                                                                         
    }
    else {
      this.idCheckbox.pop();
    }
  }
}



// let email = localStorage.getItem('email');
//     if (this.isChecked == true) {
//       localStorage.setItem('role', 'Admin');
//       let role = localStorage.getItem('role');
//       let data = { role, email };
//       this.userService.alterar(data)
//         .subscribe(() => {
//           this.router.navigate(['book']);
//         });
//     }
//     else {
//       localStorage.setItem('role', 'User');
//       let role = localStorage.getItem('role');
//       let data = { role, email };
//       this.userService.alterar(data)
//         .subscribe(() => {
//           this.router.navigate(['book']);
//         });
//     }