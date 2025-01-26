import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { ionSearch } from '@ng-icons/ionicons';
import { Project } from '../models/project.model';
import { ProjectService } from '../services/project.service';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [NgIcon],
  providers: [provideIcons({
    ionSearch
  })],
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.css'
})
export class ProjectListComponent implements OnInit {
  @Output('projectChange') projectChange = new EventEmitter<number>();

  private _projectService = inject(ProjectService);

  protected projectList: Project[] = [];
  private _selectedProjectId?: number; 

  ngOnInit(): void {
    this.projectList = this._projectService.projectList;
  }

  get selectedProjectId() {
    return this._selectedProjectId;
  }

  protected onProjectChange(projectId: number) {
    this._selectedProjectId = projectId;
    this.projectChange.emit(projectId);
  }
  
}