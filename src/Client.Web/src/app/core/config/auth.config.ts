import { AuthConfig } from "angular-oauth2-oidc";

export const authCodeFlowConfig: AuthConfig = {
    issuer: 'http://localhost:18080/realms/magjob-realm',
    tokenEndpoint: 'http://localhost:18080/realms/magjob-realm/protocol/openid-connect/token',
    redirectUri: window.location.origin,
    clientId: 'web-client',
    responseType: 'code',
    scope: 'openid profile',
}

