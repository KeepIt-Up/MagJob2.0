import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Permission } from '../types/role/role';


@Injectable({
    providedIn: 'root'
})
export class PermissionApiService {
    private http = inject(HttpClient);

    getAllPermissions(): Observable<Permission[]> {
        return this.http.get<Permission[]>(`/api/permission`);
    }
}