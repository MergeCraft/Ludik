using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasIntegracion
{
    public class PruebaIntegracionLogin : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PruebaIntegracionLogin(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ConCredencialesValidas_DebeRetornarToken()
        {
            var loginDto = new
            {
                NombreUsuario = "Pedro25",
                Contrasenia = "$2a$12$ExB5Kc8y2M8bt37GhWtg/eFfVFF3UGFYKiExuX7UW40l1Bx.tmD9K"
            };

            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Ejecuta la solicitud al endpoint de login
            var response = await _client.PostAsync("/api/usuario/login", content);

            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("ERROR BODY: " + errorBody);
            Assert.True(response.IsSuccessStatusCode, $"StatusCode: {response.StatusCode}, Body: {errorBody}");

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("token", responseBody.ToLower());
        }
    }
}
