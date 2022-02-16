import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IOrderSim } from 'src/app/models/orderSim';
import { IPlan } from 'src/app/models/plan';
import { ISimType } from 'src/app/models/simType';
import { IVIPNumber } from 'src/app/models/vipNumber';
import { OrderSIMCardService } from 'src/app/services/order-simcard.service';
import { PlanService } from 'src/app/services/plan.service';
import { PortnumberService } from 'src/app/services/portnumber.service';
import { SimTypeService } from 'src/app/services/sim-type.service';
import { VipnumberService } from 'src/app/services/vipnumber.service';
declare var $: any;

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  constructor(private plan:PlanService,private vip:VipnumberService,private port:PortnumberService,private _route:Router,private route: ActivatedRoute,private router:Router,private simtype:SimTypeService,private orderSimCard:OrderSIMCardService) { }
  userDetailForm:FormGroup;
  planDetail:IPlan;
  userId:number;
  planId:number;
  simTypeId:number;
  orderDetail:IOrderSim;
  message:string;
  type:string;
  simTypeName:string;
  vipNumberList:IVIPNumber[];
  test:string;
  temp:boolean=false;

  ngOnInit(): void {
    this.userId = parseInt(localStorage.getItem('userId'));

    this.planId = parseInt(this.route.snapshot.paramMap.get('id'));
    this.planDetails(this.planId);

    this.simTypeName = this.route.snapshot.paramMap.get('type');

    this.userDetailForm = new FormGroup({
        "alterMobileNumber":new FormControl('',[Validators.required,Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
        "portMobileNumber":new FormControl('',[Validators.required,this.validatePortNumber(),Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
        "selectedVIPNumId":new FormControl('',[Validators.required]),

        "flatNo":new FormControl('',[Validators.required,Validators.minLength(1),Validators.pattern('[0-9]*')]),  
        "city":new FormControl('',[Validators.required,Validators.minLength(3),Validators.pattern('[a-zA-Z]*')]),
        "state":new FormControl('',[Validators.required,Validators.minLength(3),Validators.pattern('[a-zA-Z]*')]),
        "country":new FormControl('',[Validators.required,Validators.minLength(3),Validators.pattern('[a-zA-Z]*')]),
        "pincodeNo":new FormControl('',[Validators.required,Validators.minLength(6),Validators.maxLength(6),Validators.pattern('[0-9]*')])                                                                                                                                           
    });
    this.getAllSimType();
    setTimeout(() => {
      this.setDefaultValue();
    }, 500);
    window.scrollTo(0, 0)
  }

   validatePortNumber():ValidatorFn  {
    return (control: AbstractControl): ValidationErrors | null => {
      if(control.value.length>9)
      {
        this.port.checkPortNumberExist(control.value)
          .subscribe(
            (res:any) => {
              //let res: string = data;
              if (res.portNumber === control.value) {
                this.test = "already for order";
                return {'alreadyExist': true};
              } else {
                this.test = "";
                return null;
              }
            },
            (error) => {
              console.log(error);
            }
        )
     }
     return null;
    }
  }

  setDefaultValue()
  {
    if(this.type==null)
    {
      this.userDetailForm.controls['portMobileNumber'].setValue('1234567890');
      this.userDetailForm.controls['selectedVIPNumId'].setValue('1');
    }
    else if(this.type == 'vip')
    {
      this.vipNumbers();
      this.userDetailForm.controls['portMobileNumber'].setValue('1234567890');
    }
    else
      this.userDetailForm.controls['selectedVIPNumId'].setValue('1');
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

  getSimTypeName():string{
    var url = this.router.url;
    var newarr = url.split("/");
    this.type = newarr.find(x => x === "vip" || x === "port");
    console.log(this.type);
    return newarr.find(x => x === "postpaid" || x === "prepaid");
  }

  getAllSimType(){
    this.simtype.getAllSimType()
    .subscribe((res:any)=>{
      console.log(res);
      var item = res.find(item => item.name.toLowerCase() === this.getSimTypeName());
      this.simTypeId = item.id;
      console.log(this.simTypeId);
    })
  }

  vipNumbers()
  {
    this.vip.getAllVIPNumber()
    .subscribe((res:any)=>{
      console.log(res);
      this.vipNumberList = res.filter(x=>x.simtypeName.toLowerCase() == this.simTypeName);
      console.log(this.vipNumberList);
    })
  }

  OnSubmit()
  {
    if(this.type==null){
      this.OrderPrepaidOrPostpaidSIM();
    }
    else if(this.type == "vip"){
      this.OrderVIPSim()
    }
    else{
      this.OrderPortNumberSim()
    }

  }

  OrderPrepaidOrPostpaidSIM(){
    this.orderDetail = {
      alternateContactNo : this.userDetailForm.value.alterMobileNumber,
      userId : this.userId,
      planId : this.planId,
      simtypeId : this.simTypeId,
      status: 2,
      flatNo: parseInt(this.userDetailForm.value.flatNo),
      city: this.userDetailForm.value.city,
      state: this.userDetailForm.value.state,
      country: this.userDetailForm.value.country,
      pincodeNo: this.userDetailForm.value.pincodeNo
    }
    this.orderSimCard.buyNewSimCard(this.orderDetail)
    .subscribe((res:any)=>{
      if(res.status=="Success")
      {
        console.log(this.userDetailForm);
        this.userDetailForm.reset();
        this.message=res.message;
        console.log(this.message);
        console.log(res.message);
        this.showModal();
      }
      else
      {
        console.log(res.message);
      }
    });
  }

  OrderVIPSim()
  {
    this.orderDetail = {
      alternateContactNo : this.userDetailForm.value.alterMobileNumber,
      userId : this.userId,
      planId : this.planId,
      simtypeId : this.simTypeId,
      status: 2,
      vipNumberId:parseInt(this.userDetailForm.value.selectedVIPNumId),
      flatNo: parseInt(this.userDetailForm.value.flatNo),
      city: this.userDetailForm.value.city,
      state: this.userDetailForm.value.state,
      country: this.userDetailForm.value.country,
      pincodeNo: this.userDetailForm.value.pincodeNo
    }
    this.vip.buyNewVIPSimCard(this.orderDetail)
    .subscribe((res:any)=>{
      if(res.status=="Success")
      {
        console.log(this.userDetailForm);
        this.userDetailForm.reset();
        this.message=res.message;
        console.log(res.message);
        this.showModal();
      }
      else
      {
        console.log(res.message);
      }
    });
  }

  OrderPortNumberSim(){
    this.orderDetail = {
      alternateContactNo : this.userDetailForm.value.alterMobileNumber,
      userId : this.userId,
      planId : this.planId,
      simtypeId : this.simTypeId,
      status: 2,
      portNumber:this.userDetailForm.value.portMobileNumber,
      flatNo: parseInt(this.userDetailForm.value.flatNo),
      city: this.userDetailForm.value.city,
      state: this.userDetailForm.value.state,
      country: this.userDetailForm.value.country,
      pincodeNo: this.userDetailForm.value.pincodeNo
    }
    this.port.buyNewPortSimCard(this.orderDetail)
    .subscribe((res:any)=>{
      if(res.status=="Success")
      {
        console.log(this.userDetailForm);
        this.userDetailForm.reset();
        this.message=res.message;
        console.log(res.message);
        this.showModal();
      }
      else
      {
        console.log(res.message);
      }
    });
  }

  showModal(){
    $(document).ready(function(){
      $(window).load(function(){
        $('#exampleModal').modal('show');
      });
    });
  }

  navigate(){
    this._route.navigate(['home']);
  }
}
