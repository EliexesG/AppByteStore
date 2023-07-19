using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repositories
{
    public interface IRepositoryPregunta
    {
        Pregunta GetPreguntaById(int IdPregunta);
        IEnumerable<Pregunta> GetPreguntaByProducto(int IdProducto);
        Respuesta GetRespuestaById(int IdRespuesta);
        IEnumerable<Respuesta> GetRespuestaByPregunta(int IdPregunta);
        Pregunta SavePregunta(Pregunta pregunta, int idUsuario, int idProducto);
        Respuesta SaveRespuesta(Respuesta respuesta, int idUsuario, int idPregunta);
    }
}