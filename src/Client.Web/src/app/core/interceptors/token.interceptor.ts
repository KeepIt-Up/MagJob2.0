import { HttpRequest, HttpHandlerFn, HttpEvent, HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject, from } from 'rxjs';
import { catchError, filter, take, switchMap } from 'rxjs/operators';
import { inject } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

let isRefreshing = false;
const refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

export const tokenInterceptor: HttpInterceptorFn = (request: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
    const authService = inject(OAuthService);

    if (authService.hasValidAccessToken()) {
        request = addToken(request, authService.getAccessToken()!);
    }

    return next(request);
};

function addToken(request: HttpRequest<any>, token: string) {
    const tokenType = 'Bearer';
    return request.clone({
        setHeaders: {
            'Authorization': `${tokenType} ${token}`
        }
    });
}

function handle401Error(request: HttpRequest<any>, next: HttpHandlerFn, authService: OAuthService) {
    if (!isRefreshing) {
        isRefreshing = true;
        refreshTokenSubject.next(null);

        return from(authService.refreshToken()).pipe(
            switchMap((token: any) => {
                isRefreshing = false;
                refreshTokenSubject.next(token);
                return next(addToken(request, token));
            }),
            catchError((err) => {
                isRefreshing = false;
                authService.logOut();
                return throwError(() => err);
            })
        );
    } else {
        return refreshTokenSubject.pipe(
            filter(token => token != null),
            take(1),
            switchMap(jwt => {
                return next(addToken(request, jwt));
            })
        );
    }
}
