import { Component, inject, OnInit } from '@angular/core';
import { AuthUserService } from '../../services/auth-user.service';
import { User } from '../../models/user.model';
import { CdkMenuModule } from '@angular/cdk/menu';

@Component({
  selector: 'app-user-menu',
  standalone: true,
  imports: [CdkMenuModule],
  templateUrl: './user-menu.component.html',
  styleUrl: './user-menu.component.css'
})
export class UserMenuComponent implements OnInit {
  private _authUserService = inject(AuthUserService);

  ngOnInit(): void {
    
  }

  protected get isUserLoggedIn(): boolean {
    return this._authUserService.isAuthenticated;
  }

  protected get loggedInUser(): User {
    return this._authUserService.user;
  }

}
