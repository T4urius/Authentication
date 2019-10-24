import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-controll-access',
  templateUrl: './controll-access.component.html',
  styleUrls: ['./controll-access.component.scss']
})
export class ControllAccessComponent implements OnInit {

  isChecked: boolean;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  ngAfterContentInit() {
    let role = localStorage.getItem('role');
    if (role == 'Admin') {
      this.isChecked = true;
    }
    else {
      this.isChecked = false;
    }
  }

  salvar() {
    this.router.navigate(['book']);
    if (this.isChecked == true) {
      localStorage.setItem('role', 'Admin');
    }
    else {
      localStorage.setItem('role', 'User');
    }
  }

  checkValue(event: any) {
    console.log(event);
  }

}
