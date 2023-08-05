using Infraestructure.Utils;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryUbicacion
    {
        Task<ApiResult> ObtenerCantonxProvincia(int idProvincia);
        Task<ApiResult> ObtenerDistritoxCanton(int idProvincia, int idCanton);
        Task<ApiResult> ObtenerProvincia();
    }
}