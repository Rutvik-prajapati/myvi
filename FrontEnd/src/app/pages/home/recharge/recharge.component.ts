import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';
declare var $: any;

@Component({
  selector: 'app-recharge',
  templateUrl: './recharge.component.html',
  styleUrls: ['./recharge.component.css']
})
export class RechargeComponent implements OnInit {

  constructor(private auth:AuthenticationService,private route:Router) { }
  mobileNo:string;
  val:boolean;
  type:string;
  rechargeForm:FormGroup;

  ngOnInit(): void {

    this.mobileNo = localStorage.getItem('contactNo');
    
    this.rechargeForm = new FormGroup({
      "rechargeType":new FormControl('prepaid',[Validators.required]),
      "phoneNumber":new FormControl({value: '', disabled: true},[Validators.required,Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")])                                                                                                                                                                    
    });

    if(this.mobileNo != null)
      this.rechargeForm.controls['phoneNumber'].setValue(this.mobileNo);
  
  }

  check(){
    this.val = this.auth.checkLogged();
    this.makeHideShow(this.val);
    return this.val;
  }

  OnSubmit(){
    var type = this.rechargeForm.controls['rechargeType'].value;
    if(type == "prepaid")
      this.route.navigate(['home/prepaid']);
    else
      this.route.navigate(['home/postpaid']);
  }


  // userDetails(userId:number)
  // {
  //   this.user.getUserDetailById(userId)
  //   .subscribe((res:any)=>{
  //     console.log(res);
  //     this.rechargeForm.controls['phoneNumber'].setValue(res.contactNo);;
  //     console.log(res);
  //   })
  // }

  makeHideShow(val:boolean){
      if(val == false)
        $('#recharge').removeClass("rounded-pill border input-group px-1").addClass("px-5"); 
      else
        $('#recharge').removeClass("px-5").addClass("rounded-pill border input-group px-1");
  }
}
