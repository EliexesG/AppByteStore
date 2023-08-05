using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryUbicacion : IRepositoryUbicacion
    {

        string urlBasic = "https://ubicaciones.paginasweb.cr/";

        private async Task<HttpResponseMessage> CallApiUbicaciones(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlBasic);
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        public async Task<ApiResult> ObtenerProvincia()
        {
            string endpoint = "provincias.json";
            var response = await CallApiUbicaciones(endpoint);

            string result = await response.Content.ReadAsStringAsync();
            bool success = response.IsSuccessStatusCode;


            return new ApiResult()
            {
                Result = result,
                Success = success
            };
        }

        public async Task<ApiResult> ObtenerCantonxProvincia(int idProvincia)
        {
            string endpoint = $"provincia/{idProvincia}/cantones.json";
            var response = await CallApiUbicaciones(endpoint);

            string result = await response.Content.ReadAsStringAsync();
            bool success = response.IsSuccessStatusCode;


            return new ApiResult()
            {
                Result = result,
                Success = success
            };
        }

        public async Task<ApiResult> ObtenerDistritoxCanton(int idProvincia, int idCanton)
        {
            string endpoint = $"provincia/{idProvincia}/canton/{idCanton}/distritos.json";
            var response = await CallApiUbicaciones(endpoint);

            string result = await response.Content.ReadAsStringAsync();
            bool success = response.IsSuccessStatusCode;


            return new ApiResult()
            {
                Result = result,
                Success = success
            };
        }

    }
}
