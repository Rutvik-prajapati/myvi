import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPlan } from 'src/app/models/plan';
import { PlanService } from 'src/app/services/plan.service';
declare var $:any;

@Component({
  selector: 'app-plan',
  templateUrl: './plan.component.html',
  styleUrls: ['./plan.component.css']
})
export class PlanComponent implements OnInit {

  constructor(private plan:PlanService) { }

  plans:IPlan[];
  planDetail:IPlan;

  ngOnInit(): void {
    this.getAllPlans();    
  }

  getAllPlans()
  {
    // this.planType = "unlimited"
    this.plan.getAllPlans()
    .subscribe((res:any)=>{
      console.log(res);
      this.plans = res.sort();
      // this.datatable(res);
    },
    (err:HttpErrorResponse)=>{
      console.log(err.message);
    })
  }

  planDetails(planId:number)
  {
    this.plan.getPlanDetailById(planId)
    .subscribe((res:any)=>{
      console.log(res);
      this.planDetail = res;
      this.showModal();
      console.log(this.planDetail);
    })
  }

  showModal(){
      $(window).load(function(){
        $('#pack-detail').modal('show');
      });
  }

  datatable(plans){
    $(document).ready(function () {
      // $('#dtBasicExample').DataTable();
      // // $('.dataTables_length').addClass('bs-select');
      // });

      $('#dtBasicExample').DataTable({
        // bDestroy: true,
        data: plans,
        columns: [
            { "data": "id" },
            { "data": "price" },
            { "data": "call" },
            { "data": "data" },
            // { "data": "VehicleInsuranceNo" },
            // {
            //     "data": "Model"
            // },
            {
                "data": "Id",
                //"orderable": false,
                //"searchable": false,
                "render": function (Id) { // render event defines the markup of the cell text
                    //debugger
                    var a = '<a href="#" onclick=EditVehicle(' + Id + ')><i class="btn btn-success bi bi-eye"></i> </a>'; // row object contains the row data
                    return a;
                    
                }
            },
            {
                "data": "Id",
                //"orderable": false,
                //"searchable": false,
                "render": function (Id) { // render event defines the markup of the cell text
                    //debugger
                    var a = '<a href="#" onclick=DetailVehicle(' + Id + ')><i class="btn btn-primary bi bi-pen"></i> </a>'; // row object contains the row data
                    return a;
                }
            },
            {
                "data": "Id",
                //"orderable": false,
                //"searchable": false,
                "render": function (Id) { // render event defines the markup of the cell text
                    //debugger
                    var a = '<a href="#" onclick=DeleteVehicle(' + Id + ')><i class="btn btn-danger bi bi-trash"></i> </a>'; // row object contains the row data
                    return a;
                }
            }
        ]
    });
  });
}
}
