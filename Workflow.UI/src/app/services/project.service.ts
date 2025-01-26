import { inject, Injectable } from "@angular/core";
import { Project } from "../models/project.model";
import { AuthUserService } from "./auth-user.service";

@Injectable({providedIn: 'root'})
export class ProjectService {
    private _authUserService = inject(AuthUserService);

    get projectList(): Project[] {
        return [
            {id: 1, name: 'Web design'},
            {id: 2, name: 'Project design'},
            {id: 3, name: 'Angular front-end'},
            {id: 4, name: '.NET back-end'}
        ];
    }
}