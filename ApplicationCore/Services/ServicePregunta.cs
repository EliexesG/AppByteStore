using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePregunta : IServicePregunta
    {
        IRepositoryPregunta repository = new RepositoryPregunta();

        public Pregunta GetPreguntaById(int IdPregunta)
        {
            return repository.GetPreguntaById(IdPregunta);
        }

        public IEnumerable<Pregunta> GetPreguntaByProducto(int IdProducto)
        {
            return repository.GetPreguntaByProducto(IdProducto);
        }

        public Respuesta GetRespuestaById(int IdRespuesta)
        {
            return repository.GetRespuestaById(IdRespuesta);
        }

        public IEnumerable<Respuesta> GetRespuestaByPregunta(int IdPregunta)
        {
            return repository.GetRespuestaByPregunta(IdPregunta);
        }

        public Pregunta SavePregunta(Pregunta pregunta, int idUsuario, int idProducto)
        {
            return repository.SavePregunta(pregunta, idUsuario, idProducto);
        }

        public Respuesta SaveRespuesta(Respuesta respuesta, Pregunta pregunta)
        {
            return repository.SaveRespuesta(respuesta, pregunta);
        }
    }
}
