import { APP_INITIALIZER, ApplicationConfig } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { tokenInterceptor } from './app/core/interceptors/token.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { OAuthService, provideOAuthClient } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from './app/core/config/auth.config';
import { circularReferenceInterceptor } from './app/core/interceptors/circular-reference.interceptor';

export const appConfig: ApplicationConfig = {
    providers: [
        provideHttpClient(
            withInterceptors([tokenInterceptor, circularReferenceInterceptor])
        ),
        provideRouter(routes, withComponentInputBinding()),
        provideOAuthClient(),
        {
            provide: APP_INITIALIZER,
            useFactory: (oauthService: OAuthService) => {
                return () => {
                    initializeOAuth(oauthService);
                };
            },
            multi: true,
            deps: [OAuthService],
        }, provideAnimationsAsync('noop'),
    ],
};

function initializeOAuth(oauthService: OAuthService): Promise<void> {
    return new Promise((resolve) => {
        oauthService.configure(authCodeFlowConfig);
        oauthService.setupAutomaticSilentRefresh();
        oauthService.loadDiscoveryDocumentAndLogin().then(() => {
            resolve();
        });
    });
}