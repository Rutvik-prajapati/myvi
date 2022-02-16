import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  role=localStorage.getItem('role');
  constructor(private route:Router,private auth:AuthenticationService) { }

  //logged:boolean;

  ngOnInit(): void {
     this.checkLogged();
  }

  checkLogged()
  {
    return this.auth.checkLogged();
  }

  logout()
  {
    this.auth.logout();
  }
}
