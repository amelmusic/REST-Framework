// The file for the current environment will overwrite this one during build.
// Different environments can be found in ./environment.{dev|prod}.ts, and
// you can create your own and use it with the --env flag.
// The build system defaults to the dev environment.

export const environment = {
  production: false,
  debugTimeout: 0,
  snackbarDefaultTimeout: 300,
  pathList: {xcorewebapp: "https://localhost:5001/api"},
  auth: {

    // Url of the Identity Provider
    issuer: 'http://localhost/Core360.IdentityManager.Identity/oauth',
  
    // URL of the SPA to redirect the user to after login
    redirectUri: window.location.origin,
  
    // The SPA's id. The SPA is registered with this id at the auth-server
    clientId: 'Core360',
  
    // set the scope for the permissions the client should request
    scope: 'openid profile name email roles',
    oidc: true,
    requireHttps: false
  }
};
