import { Component } from '@angular/core';
import { UserMenuComponent } from "./user-menu/user-menu.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [UserMenuComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}
