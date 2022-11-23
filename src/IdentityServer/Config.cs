using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
  public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
            new IdentityResources.OpenId()
      };

  public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };

  public static IEnumerable<Client> Clients =>
      new List<Client>
      {
        new Client
        {
          // ED on définit la paire
          // {client_id, client_secret }
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            // ED fourniture d'un flow d'authentification
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // scopes that client has access to
            AllowedScopes = { "api1" }
        }
      };
}