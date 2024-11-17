import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class UserApiService {
    constructor(private http: HttpClient) { }

    createUser(): Observable<User> {
        return this.http.post<User>('/api/users', {});
    }
}

export interface User {
    id: string;
    email: string;
    firstname: string;
    lastname: string;
    phoneNumber: string;
    birthDate: string;
    image: string;
}
