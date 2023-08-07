using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUbicacion
    {
        Task<ApiResult> ObtenerCantonxProvincia(int idProvincia);
        Task<ApiResult> ObtenerDistritoxCanton(int idProvincia, int idCanton);
        Task<ApiResult> ObtenerProvincia();
        Task<string> ObtenerNombreProvincia(int idProvincia);
        Task<string> ObtenerNombreCanton(int idProvincia, int idCanton);
        Task<string> ObtenerNombreDistrito(int idProvincia, int idCanton, int idDistrito);
    }
}
