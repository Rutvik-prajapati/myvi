import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppModule } from 'src/app/app.module';
import { IPlan } from 'src/app/models/plan';
import { IRecharge } from 'src/app/models/recharge';
import { IUser } from 'src/app/models/user';
import { PlanService } from 'src/app/services/plan.service';
import { RechargeService } from 'src/app/services/recharge.service';
import { UserService } from 'src/app/services/user.service';
import { WindowRefService } from 'src/app/services/window-ref.service';

@Component({
  selector: 'app-confirm-mobile-no',
  templateUrl: './confirm-mobile-no.component.html',
  styleUrls: ['./confirm-mobile-no.component.css'],
  providers: [WindowRefService]
})
export class ConfirmMobileNoComponent implements OnInit {

  constructor(private plan:PlanService,private user:UserService,private route: ActivatedRoute,private winRef: WindowRefService,private recharge:RechargeService,private _route:Router) { }

  planDetail:IPlan
  planId:number;
  confirmForm:FormGroup;
  userId:number;
  userDetail:IUser
  phoneNumber:string;
  temp:string="";
  rechargeData:IRecharge;
  
  ngOnInit(): void {
    
    this.userId = parseInt(localStorage.getItem('userId'));
    this.userDetails(this.userId);

    this.planId = parseInt(this.route.snapshot.paramMap.get('id'));
    this.planDetails(this.planId);

    this.confirmForm = new FormGroup({
        "phoneNumber":new FormControl(this.temp,[Validators.required,Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")])                                                                                                                                                                    
    });
    setTimeout(() => {
      this.loadPhoneNumber();
    }, 200);
    window.scrollTo(0, 0)
  }

  loadPhoneNumber()
  {
    let phoneNumber = this.userDetail.contactNo;
    console.log(phoneNumber);
    this.confirmForm.controls['phoneNumber'].setValue(phoneNumber);
  }

  planDetails(planId:number)
  {
    this.plan.getPlanDetailById(planId)
    .subscribe((res:any)=>{
      console.log(res);
      this.planDetail = res;
      console.log(this.planDetail);
    })
  }

  userDetails(userId:number)
  {
    this.user.getUserDetailById(userId)
    .subscribe((res:any)=>{
      console.log(res);
      this.userDetail = res;
      this.temp=res.contactNo;
      console.log(this.temp);
      console.log(this.userDetail);
    })
  }

  OnSubmit()
  {
    this.phoneNumber = this.confirmForm.value.confirmData.phoneNumber
  }

  newRecharge(planId:number,price:number)
  { 
    this.recharge.NewRecharge({
      planId : planId,
      userId : this.userDetail.id,
      price : price
    })
    .subscribe((res:any)=>{
      console.log(res);
      this.payWithRazor(res.orderId,price);
      console.log(res);
    })
  }
  payWithRazor(orderId:string,Amount:number) {
    const options: any = {
      key: 'rzp_test_shavDUAXAVCMOq',
      amount: Amount*100, // amount should be in paise format to display Rs 1255 without decimal point
      currency: 'INR',
      name: 'myvi', // company name or product name
      description: '',  // product description
      image: '../../../../assets/img/homespyder/Vi-logo.svg', // company logo or product image
      order_id: orderId, // order_id created by you in backend
      modal: {
        // We should prevent closing of the form when esc key is pressed.
        escape: false,
      },
      notes: {
        // include notes if any
      },
      theme: {
        color: '#0c238a'
      }
    };
    options.handler = ((response: any, error: any) => {
      options.response = response;
      console.log("payment_id : "+response.razorpay_payment_id);
      console.log("order_id : "+response.razorpay_order_id);
      console.log("signature : "+response.razorpay_signature);
      console.log(options);

      this.checkCheckout({
        planId:this.planDetail.id,
        userId:this.userDetail.id,
        price:Amount,
        rzpPaymentId:response.razorpay_payment_id,
        rzpSignature:response.razorpay_signature
      });
     
      // call your backend api to verify payment signature & capture transaction
    });
    options.modal.ondismiss = (() => {
      // handle the case when user closes the form while transaction is in progress
      console.log('Transaction cancelled.');
    });
    const rzp = new this.winRef.nativeWindow.Razorpay(options);
    rzp.open();
  }

  checkCheckout(checkoutData:IRecharge)
  {
    this.recharge.checkout(checkoutData)
    .subscribe((res:any)=>{
      console.log(res);
      if(res.status == "success")
      {
        alert("payment successfull");
        this._route.navigate(['home']).then(() => {
          window.location.reload();
        });;
        
      }
      else
        alert("payment fail");
    })
  }
}
