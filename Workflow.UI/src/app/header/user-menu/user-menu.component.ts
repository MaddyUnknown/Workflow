import { Component, inject, OnInit } from '@angular/core';
import { AuthUserService } from '../../services/auth-user.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-menu',
  standalone: true,
  imports: [],
  templateUrl: './user-menu.component.html',
  styleUrl: './user-menu.component.css'
})
export class UserMenuComponent implements OnInit {
  private _authUserService: AuthUserService;
  
  constructor(authUserService: AuthUserService) {
    this._authUserService = authUserService;
  }

  ngOnInit(): void {
    
  }

  get IsUserLoggedIn(): boolean {
    return this._authUserService.IsAuthenticated;
  }

  get LoggedInUser(): User {
    return this._authUserService.User;
  }

}
