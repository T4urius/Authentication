import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm, FormGroupDirective, FormControl } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  email = '';
  password = '';
  matcher = new MyErrorStateMatcher();
  isLoadingResults = false;
  error: string;

  constructor(private formBuilder: FormBuilder, private router: Router, private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      'email': [null, Validators.required],
      'password': [null, Validators.required]
    });
  }

  onFormSubmit(form: NgForm) {
    this.authService.login(form)
      .subscribe(resp => {
        console.log(resp);
        //armazenando role
        this.userService.getUser(resp.userId)
          .subscribe(res => {
            if (resp.email == null || undefined) {
              this.error = 'E-mail ou senha incorretos, favor digitar novamente!';
            }
            else {
              console.log(res);
              localStorage.setItem('role', res.role);
              localStorage.setItem('id', res.idUser);

              if (resp.token) {
                localStorage.setItem('token', resp.token);
                this.router.navigate(['book']);
              }
            }
          });
      }, (err) => {
        console.log(err);
      });
  }
  register() {
    this.router.navigate(['register']);
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
