import { Component, OnDestroy, OnInit  } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {
  title = 'MyVi';
  constructor(public router: Router,private auth:AuthenticationService){
  //   window.addEventListener("beforeunload", function (e) {
  //     localStorage.clear();
  //  });
  }

  ngOnInit(): void {
      //this.auth.autoLogout();
  }
  
}
