import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Principal } from 'app/models/korisnik/principal';
import { AuthenticateService } from 'app/services/authenticate/authenticate.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  signinForm!: FormGroup;
  constructor(public fb: FormBuilder, public router: Router, private authService: AuthenticateService) {
    this.signinForm = this.fb.group({
      email: '',
      password: ''
    });
   }


  ngOnInit(): void {
  }

  get email() {
    return this.signinForm.get('email');
  }

  get password() {
    return this.signinForm.get('password');
  }

  loginUser(){

    this.authService.logIn(this.signinForm.value)
  }

  buildFormControls(){
    this.signinForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    })
  }

}
