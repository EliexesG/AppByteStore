using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryPregunta : IRepositoryPregunta
    {
        public IEnumerable<Pregunta> GetPreguntaByProducto(int IdProducto)
        {
            IEnumerable<Pregunta> lista = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Pregunta
                            .Include(p => p.Respuesta)
                            .Include(p => p.Usuario)
                            .Include(p => p.Producto)
                            .Where(P => P.Producto.IdProducto == IdProducto)
                            .ToList()
                            .OrderByDescending(p => p.FechaHora);
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Pregunta GetPreguntaById(int IdPregunta)
        {
            Pregunta pregunta = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pregunta = ctx.Pregunta
                            .Include(p => p.Respuesta)
                            .Include(p => p.Usuario)
                            .Include(p => p.Producto)
                            .Where(P => P.IdPregunta == IdPregunta)
                            .FirstOrDefault();
                }
                return pregunta;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Respuesta> GetRespuestaByPregunta(int IdPregunta)
        {
            IEnumerable<Respuesta> lista = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Respuesta
                            .Include(r => r.Pregunta)
                            .Include(r => r.Usuario)
                            .Where(r => r.Pregunta.IdPregunta == IdPregunta)
                            .ToList()
                            .OrderByDescending(p => p.FechaHora);
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Respuesta GetRespuestaById(int IdRespuesta)
        {
            Respuesta respuesta = null;
            try
            {


                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    respuesta = ctx.Respuesta
                            .Include(p => p.Pregunta)
                            .Include(p => p.Usuario)
                            .Where(P => P.IdRespuesta == IdRespuesta)
                            .FirstOrDefault();
                }
                return respuesta;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Pregunta SavePregunta(Pregunta pregunta, int idUsuario, int idProducto)
        {
            int retorno = 0;
            Pregunta oPregunta = null;
            Usuario usuario = null;
            Producto producto = null;
            IRepositoryUsuario _RepositoryUsuario = new RepositoryUsuario();
            IRepositoryProducto _RepositoryProducto = new RepositoryProducto();

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    usuario = _RepositoryUsuario.GetUsuarioByID(idUsuario);
                    producto = _RepositoryProducto.GetProductoByID(idProducto);
                    producto.Usuario = null;
                    

                    ctx.Usuario.Attach(usuario);
                    ctx.Producto.Attach(producto);

                    pregunta.Usuario = usuario;
                    pregunta.Producto = producto;

                    ctx.Pregunta.Add(pregunta);
                    retorno = ctx.SaveChanges();
                }
                if (retorno >= 0)
                {
                    oPregunta = GetPreguntaById((int)pregunta.IdPregunta);
                }

                return oPregunta;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Respuesta SaveRespuesta(Respuesta respuesta, int idUsuario, int idPregunta)
        {
            int retorno = 0;
            Respuesta oRespuesta = null;
            Usuario usuario = null;
            Pregunta pregunta = null;
            IRepositoryUsuario _repositoryUsuario = new RepositoryUsuario();

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    usuario = _repositoryUsuario.GetUsuarioByID(idUsuario);
                    pregunta = this.GetPreguntaById(idPregunta);
                    pregunta.Usuario = null;

                    ctx.Usuario.Attach(usuario);
                    ctx.Pregunta.Attach(pregunta);

                    respuesta.Usuario = usuario;
                    respuesta.Pregunta = pregunta;

                    ctx.Respuesta.Add(respuesta);
                    retorno = ctx.SaveChanges();
                }
                if (retorno >= 0)
                {
                    oRespuesta = GetRespuestaById((int)respuesta.IdRespuesta);
                }

                return oRespuesta;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
