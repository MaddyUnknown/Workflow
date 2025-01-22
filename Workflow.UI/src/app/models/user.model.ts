export interface User {
    id: number;
    name: string;
    username: string;
}

export const EMPTY_USER: User = Object.freeze({id: 0, name: '', username: ''});