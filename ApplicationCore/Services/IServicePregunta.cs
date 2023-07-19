using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePregunta
    {
        Pregunta GetPreguntaById(int IdPregunta);
        IEnumerable<Pregunta> GetPreguntaByProducto(int IdProducto);
        Respuesta GetRespuestaById(int IdRespuesta);
        IEnumerable<Respuesta> GetRespuestaByPregunta(int IdPregunta);
        Pregunta SavePregunta(Pregunta pregunta, int idUsuario, int idProducto);
        Respuesta SaveRespuesta(Respuesta respuesta, int idUsuario, int idPregunta);
    }
}
