import { Component } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { ProjectListComponent } from "./project-list/project-list.component";
import { ProjectDetailComponent } from "./project-detail/project-detail.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, ProjectListComponent, ProjectDetailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Workflow.UI';
}
