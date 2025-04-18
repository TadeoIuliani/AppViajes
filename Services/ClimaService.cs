using System.Text.Json;

namespace AppViajesWirsolut.Services
{
    public class Clima
    {
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public float Temperatura { get; set; }

    }

    public interface IClimaService
    {
        Task<List<Clima>> ObtenerClimaPorCiudadIdAsync(int ciudadId);
    }

    public class ClimaService : IClimaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "fd6aafacc9f55c6d32250920bc28ebc7\r\n"; 

        public ClimaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Clima>> ObtenerClimaPorCiudadIdAsync(int ciudadId)
        {
            var pronostico = new List<Clima>();

            var url = $"https://api.openweathermap.org/data/2.5/forecast?id={ciudadId}&cnt=10&appid={_apiKey}&units=metric&lang=es";
            var response = await _httpClient.GetAsync(url); //Axios????

            if (!response.IsSuccessStatusCode)
                return new List<Clima>();

            var json = await response.Content.ReadAsStringAsync();
            dynamic data = JsonSerializer.Deserialize<dynamic>(json);


            foreach (var item in data["list"])
            {
                DateTime fecha = DateTime.Parse(item["dt_txt"].ToString()) ; // "2025-04-19 00:00:00"
                string estado = item["weather"][0]["main"].ToString(); // "Rain"
                string descripcion = item["weather"][0]["description"].ToString(); // "lluvia ligera"
                float temp = float.Parse(item["main"]["temp"].ToString());

                pronostico.Add(new Clima
                {
                    Fecha = fecha,
                    Estado = estado,
                    Descripcion = descripcion,
                    Temperatura = temp
                });
            }
            return pronostico;

        }
    }
}
