using IdentityModel.Client;

class Program
{
  static async Task Main(string[] args)
  {
    // discover endpoints from metadata
    var client = new HttpClient();
    var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
    if (disco.IsError)
    {
      Console.WriteLine(disco.Error);
      return;
    }
    // request token
    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
      Address = disco.TokenEndpoint,

      ClientId = "client",
      ClientSecret = "secret",
      Scope = "api1"
    });

    if (tokenResponse.IsError)
    {
      Console.WriteLine(tokenResponse.Error);
      return;
    }
    // ED le token obtenu peut être consulté dans jwt.io
    Console.WriteLine(tokenResponse.Json);

    // Call api with the token
    // in header Authorization : Bearer eyA5468976543...
    var apiClient = new HttpClient();
    apiClient.SetBearerToken(tokenResponse.AccessToken);

    var response = await apiClient.GetAsync("https://localhost:6001/identity");
    if (!response.IsSuccessStatusCode)
    {
      Console.WriteLine(response.StatusCode);
    }
    else
    {
      var content = await response.Content.ReadAsStringAsync();
      Console.WriteLine(content);
    }
  }
}
