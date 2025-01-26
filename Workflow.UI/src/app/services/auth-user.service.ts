import { Injectable } from "@angular/core";
import { EMPTY_USER, User } from "../models/user.model";

@Injectable({ providedIn: 'root' })
export class AuthUserService {
    private _isAuthenticated: boolean = false;
    private _loggedInUser?: User;

    constructor() {
        this._isAuthenticated = true;
        this._loggedInUser = {id: 1, name: 'Mehdi Hossain', username: 'mehdih'};
    }

    get isAuthenticated(): boolean {
        return this._isAuthenticated;
    }

    get user(): User {
        return this._isAuthenticated ? Object.freeze(this._loggedInUser!) : EMPTY_USER;
    }
}