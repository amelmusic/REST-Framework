using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Common.Services.Database
{
    public partial class CommonContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityResources>()
               .HasData(new IdentityResources()
               {
                   Id = 1,
                   Enabled = true,
                   Name = "profile",
                   DisplayName = "Profile",
                   Created = DateTime.Now,
                   NonEditable = false,
                   Required = true,
               },new IdentityResources()
               {
                   Id = 2,
                   Enabled = true,
                   Name = "openid",
                   DisplayName = "OpenId",
                   Created = DateTime.Now,
                   NonEditable = false,
                   Required = true,
               },new IdentityResources()
               {
                   Id = 3,
                   Enabled = true,
                   Name = "name",
                   DisplayName = "Name",
                   Created = DateTime.Now,
                   NonEditable = false,
                   Required = true,
               },new IdentityResources()
               {
                   Id = 4,
                   Enabled = true,
                   Name = "email",
                   DisplayName = "E-mail",
                   Created = DateTime.Now,
                   NonEditable = false,
                   Required = true,
               });

            modelBuilder.Entity<IdentityClaims>()
               .HasData(new IdentityClaims()
               {
                   Id = 1,
                   Type = "given_name",
                   IdentityResourceId = 1
               },new IdentityClaims()
               {
                   Id = 2,
                   Type = "family_name",
                   IdentityResourceId = 1
               },new IdentityClaims()
               {
                   Id = 3,
                   Type = "name",
                   IdentityResourceId = 1
               },new IdentityClaims()
               {
                   Id = 4,
                   Type = "sub",
                   IdentityResourceId = 2
               });

            modelBuilder.Entity<ApiResources>()
               .HasData(new ApiResources()
               {
                   Id = 1,
                   Enabled = true,
                   Name = "roles",
                   DisplayName = "roles",
                   Created = DateTime.Now,
                   NonEditable = false
               });

            modelBuilder.Entity<ApiScopes>()
               .HasData(new ApiScopes()
               {
                   Id = 1,
                   Name = "roles",
                   DisplayName = "Roles",
                   Description = "Roles for the user",
                   Required = true,
                   Emphasize = true,
                   ShowInDiscoveryDocument = true,
                   ApiResourceId = 1
               });

            modelBuilder.Entity<ApiScopeClaims>()
               .HasData(new ApiScopeClaims()
               {
                   Id = 1,
                   Type = "roles",
                   ApiScopeId = 1
               });

            modelBuilder.Entity<Clients>()
                .HasData(
                new Clients
                {
                    Id = 1,
                    Enabled = true,
                    ClientId = "XCore",
                    ProtocolType = "oidc",
                    RequireClientSecret = false,
                    ClientName = "XCore App",
                    Description = "XCore based application",
                    ClientUri = null, //TODO Check this,
                    LogoUri = null,
                    RequireConsent = false,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = false,
                    AllowPlainTextPkce = true,
                    AllowAccessTokensViaBrowser = true,
                    FrontChannelLogoutUri = null,
                    FrontChannelLogoutSessionRequired = false,
                    BackChannelLogoutUri = null,
                    BackChannelLogoutSessionRequired = false,
                    AllowOfflineAccess = true,
                    IdentityTokenLifetime = 600000,
                    AccessTokenLifetime = 600000,
                    AuthorizationCodeLifetime = 600000,
                    ConsentLifetime = 600000,
                    AbsoluteRefreshTokenLifetime = 600000,
                    SlidingRefreshTokenLifetime = 600000,
                    RefreshTokenUsage = 600000,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenExpiration = 600000,
                    AccessTokenType = 0, //jwt
                    EnableLocalLogin = true,
                    IncludeJwtId = true,
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = null,
                    Updated = DateTime.Now,
                    DeviceCodeLifetime = 600000,
                    NonEditable = false
                });

            modelBuilder.Entity<ClientScopes>()
                .HasData(new ClientScopes()
                {
                    Id = 1,
                    ClientId = 1,
                    Scope = "name"
                },new ClientScopes
                {
                    Id = 2,
                    ClientId = 1,
                    Scope = "email"
                },new ClientScopes
                {
                    Id = 3,
                    ClientId = 1,
                    Scope = "roles"
                },new ClientScopes
                {
                    Id = 4,
                    ClientId = 1,
                    Scope = "openid"
                },new ClientScopes
                {
                    Id = 5,
                    ClientId = 1,
                    Scope = "profile"
                });

            modelBuilder.Entity<ClientRedirectUris>()
                .HasData(new ClientRedirectUris()
                {
                    Id = 1,
                    ClientId = 1,
                    RedirectUri = "http://localhost:4200"
                },new ClientRedirectUris()
                {
                    Id = 2,
                    ClientId = 1,
                    RedirectUri = "https://localhost:5001/swagger/oauth2-redirect.html"
                });

            modelBuilder.Entity<ClientGrantTypes>()
                .HasData(new ClientGrantTypes()
                {
                    Id = 1,
                    ClientId = 1,
                    GrantType = "implicit"
                });

            modelBuilder.Entity<AspNetUsers>()
               .HasData(new AspNetUsers()
               {
                   Id = "69555f57-ba5f-412c-a6ce-445039a1257b",
                   UserName = "sa",
                   NormalizedUserName = "SA",
                   Email = "amel@xcore.io",
                   EmailConfirmed = true,
                   NormalizedEmail = "AMEL@XCORE.IO",
                   PasswordHash = "AQAAAAEAACcQAAAAEHvT2ICFTNOaGZXTOzhbXEGRPd98mdlscwfeOc96w7vS8GEOxWuKxO1O8bdc/rTQMA==", //QWEasd123!
                   SecurityStamp = "LLK2SRP43MECGMQVL3VMGSV26BRQ6P5X",
                   ConcurrencyStamp = "33f6729d-0273-4eae-9729-99ca9e2dacc1",
                   TwoFactorEnabled = false,
                   AccessFailedCount = 0,
                   LockoutEnabled = true
               });

            modelBuilder.Entity<IdentityUserClaim<string>>()
               .HasData(new IdentityUserClaim<string>()
               {
                   Id = 1,
                   UserId = "69555f57-ba5f-412c-a6ce-445039a1257b",
                   ClaimType = "name",
                   ClaimValue = "Amel Musić"
               }, new IdentityUserClaim<string>()
               {
                   Id = 2,
                   UserId = "69555f57-ba5f-412c-a6ce-445039a1257b",
                   ClaimType = "given_name",
                   ClaimValue = "Amel"
               }, new IdentityUserClaim<string>()
               {
                   Id = 3,
                   UserId = "69555f57-ba5f-412c-a6ce-445039a1257b",
                   ClaimType = "family_name",
                   ClaimValue = "Amel"
               }, new IdentityUserClaim<string>()
               {
                   Id = 4,
                   UserId = "69555f57-ba5f-412c-a6ce-445039a1257b",
                   ClaimType = "roles",
                   ClaimValue = "Super Admin"
               });
        }
    }
}
