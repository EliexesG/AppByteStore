using Infraestructure.Repositories;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUbicacion : IServiceUbicacion
    {

        IRepositoryUbicacion repository = new RepositoryUbicacion();

        public async Task<ApiResult> ObtenerCantonxProvincia(int idProvincia)
        {
            return await repository.ObtenerCantonxProvincia(idProvincia);
        }

        public async Task<ApiResult> ObtenerDistritoxCanton(int idProvincia, int idCanton)
        {
            return await repository.ObtenerDistritoxCanton(idProvincia, idCanton);
        }

        public async Task<ApiResult> ObtenerProvincia()
        {
            return await repository.ObtenerProvincia();
        }
    }
}
