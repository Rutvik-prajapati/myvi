import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IRegister } from '../../models/register';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private route:Router,private auth:AuthenticationService) { }

  registerForm:FormGroup;
  registerInfo:IRegister;
  errorMessage;
  ngOnInit(): void {
    this.registerForm = new FormGroup({
      "registerData" : new FormGroup({
        "userName":new FormControl('',[Validators.required,Validators.minLength(3),Validators.pattern('[a-zA-Z]*')]),
        "password":new FormControl('',[Validators.required,Validators.minLength(8),Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$')]), 
        "email":new FormControl('',[Validators.required,Validators.email]),
        "phoneNumber":new FormControl('',[Validators.required,Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")])
      })                                                                                                                                                                      
    });
    window.scrollTo(0, 0);
  }

  OnSubmit()
  {
    this.registerInfo = {
      username : this.registerForm.value.registerData.userName,
      password : this.registerForm.value.registerData.password,
      email : this.registerForm.value.registerData.email,
      phoneNumber : this.registerForm.value.registerData.phoneNumber
    }
    this.auth.registerUser(this.registerInfo)
    .subscribe((res:any)=>{
      if(res.status=="Success")
      {
        console.log(this.registerInfo);
        console.log(this.registerForm);
        this.registerForm.reset();
    
        this.route.navigate(['login']);

        console.log(res.message);
      }
    },
    (err:any)=>{
      this.errorMessage = err.error.message;
      // alert(err.error.message);
    });
  }

}
