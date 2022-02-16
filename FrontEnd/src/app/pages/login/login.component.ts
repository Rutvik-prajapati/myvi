import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILogin } from 'src/app/models/login';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private route:Router,private auth:AuthenticationService) { }

  signInForm:FormGroup;
  loginInfo:ILogin;
  errorMessage;
  ngOnInit(): void {
    this.signInForm = new FormGroup({
      "signInData" : new FormGroup({
        "userName":new FormControl('',[Validators.required,Validators.minLength(3),Validators.pattern('[a-zA-Z]*')]),
        "password":new FormControl('',[Validators.required,Validators.minLength(8),Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$')]), 
      })                                                                                                                                                                      
    });
    window.scrollTo(0, 0);
  }

  OnSubmit()
  {
    this.loginInfo = {
      username : this.signInForm.value.signInData.userName,
      password : this.signInForm.value.signInData.password
    }

    this.auth.loginUser(this.loginInfo)
    .subscribe((res:any)=>{
      console.log(this.loginInfo);
      console.log(this.signInForm);
      this.signInForm.reset();
  
      this.route.navigate(['']);
      console.log(res);
    },
    (err:any)=>{
      this.errorMessage = err.error.message;
    });
  }

  registerUser(){
    this.route.navigate(['register']);
  }
}
