using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using WebApi;
// Asegúrate de que este sea el namespace correcto donde está tu clase Program

namespace PruebasUnitarias.PruebasDeIntegracion
{
    public class IntegracionLogin : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegracionLogin(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ConCredencialesValidas_DebeRetornarToken()
        {
            // Configura las credenciales de prueba
            var loginDto = new
            {
                NombreUsuario = "Pedro25",
                Contrasenia = "4732Mmsi"
            };

            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Ejecuta la solicitud al endpoint de login
            var response = await _client.PostAsync("/api/usuario/login", content);

            response.EnsureSuccessStatusCode(); // Asegura que el status sea 2xx

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("token", responseBody.ToLower());
        }
    }
}
