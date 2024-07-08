using IdentityModel.Client;
using IdentityServer.Client1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IdentityServer.Client1.Controllers;

public class ProductsController : Controller
{
    private readonly IConfiguration _configuration;

    public ProductsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Product> products = new List<Product>();
        HttpClient httpClient = new HttpClient();

        var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

        if(disco.IsError)
        {
            throw new Exception("Error!!");
            //loglama yapp
        }

        ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

        clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
        clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
        clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

        var token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

        if (token.IsError) 
        { 
            //loglama yap
        }

        httpClient.SetBearerToken(token.AccessToken);

        var response = await httpClient.GetAsync("https://localhost:5016/api/product/getproducts");

        if (response.IsSuccessStatusCode) 
        {
            var content = await response.Content.ReadAsStringAsync();

            products = JsonConvert.DeserializeObject<List<Product>>(content);
        }
        else
        {
            throw new Exception("Error!!");
            //loglama yap
        }

        return View(products);
    }
}
