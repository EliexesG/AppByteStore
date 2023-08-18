using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryEvaluacion : IRepositoryEvaluacion
    {

        public Evaluacion GetEvaluacionByID(int idEvaluacion)
        {

            Evaluacion oEvaluacion = null;

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    oEvaluacion = ctx.Evaluacion.Include(x => x.Usuario1).Where(e => e.IdEvaluacion == idEvaluacion).FirstOrDefault();
                }
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

            return oEvaluacion;
        }

        public Evaluacion GetEvaluacionByPedidoForVendedor(int idPedido, int idUsuario)
        {
            Evaluacion oEvaluacion = new Evaluacion();

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    oEvaluacion = ctx.Evaluacion.Include(x => x.Usuario)
                                                .Include(x => x.Usuario1)
                                                .Include(x => x.CompraEncabezado)
                                                .Where(e => e.Usuario.IdUsuario == idUsuario && e.CompraEncabezado.IdCompraEncabezado == idPedido).FirstOrDefault(); ;
                }
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

            return oEvaluacion;
        }

        public IEnumerable<Evaluacion> GetEvaluacionByPedidoForCliente(int idPedido, int idUsuario)
        {
            IEnumerable<Evaluacion> lista = null;

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    lista = ctx.Evaluacion.Include(x => x.Usuario)
                                          .Include(x => x.Usuario1)
                                          .Include(x => x.CompraEncabezado)
                                          .Where(e => e.Usuario.IdUsuario == idUsuario && e.CompraEncabezado.IdCompraEncabezado == idPedido).ToList();
                }
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

            return lista;
        }

        public Evaluacion SaveEvaluacion(Evaluacion evaluacion)
        {
            Evaluacion oEvaluacion = null;
            int retorno = 0;

            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Usuario.Attach(evaluacion.Usuario);
                    ctx.Usuario.Attach(evaluacion.Usuario1);
                    ctx.CompraEncabezado.Attach(evaluacion.CompraEncabezado);
                    ctx.Evaluacion.Add(evaluacion);

                    retorno = ctx.SaveChanges();

                    ctx.Entry(evaluacion.Usuario).State = EntityState.Detached;
                    ctx.Entry(evaluacion.Usuario1).State = EntityState.Detached;
                    ctx.Entry(evaluacion.CompraEncabezado).State = EntityState.Detached;

                    if (retorno > 0)
                    {
                        oEvaluacion = this.GetEvaluacionByID((int)evaluacion.IdEvaluacion);

                        List<Evaluacion> todasEvaluaciones = ctx.Evaluacion.Include(e => e.Usuario1).Where(e => e.Usuario1.IdUsuario == oEvaluacion.Usuario1.IdUsuario).ToList();
                        int sumaEscala = (int)todasEvaluaciones.Sum(e => e.Escala);
                        int promedioEvaluaciones = sumaEscala / todasEvaluaciones.Count();
                        
                        Usuario oUsuario = ctx.Usuario.Where(u => u.IdUsuario == oEvaluacion.Usuario1.IdUsuario).FirstOrDefault();
                        oUsuario.PromedioEvaluaciones = promedioEvaluaciones;
                        
                        ctx.Usuario.Attach(oUsuario);

                        ctx.Entry(oUsuario).Property("PromedioEvaluaciones").IsModified = true;
                        retorno = ctx.SaveChanges();

                        if (retorno <= 0)
                        {
                            oEvaluacion = null;
                        }

                    }

                }
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

            return oEvaluacion;
        }

        public IEnumerable<object> CantEvaluacionesPorEscalaVendedor (int idVendedor)
        {

            IEnumerable<object> EvaluacionesPorEscala = null;

            try
            {

                using (ByteStoreContext ctx = new ByteStoreContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    EvaluacionesPorEscala = ctx.Evaluacion
                                            .Include(e => e.Usuario1)
                                            .Where(e => e.Usuario1.IdUsuario == idVendedor)
                                            .GroupBy(d => new { d.Usuario1.IdUsuario, d.Escala })
                                            .Select( g => new
                                            {
                                                IdUsuario = g.Key.IdUsuario,
                                                Escala = g.Key.Escala,
                                                CantidadEvaluaciones = g.Count()
                                            })
                                            .OrderByDescending(e => e.Escala)
                                            .ToList();

                }

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

            return EvaluacionesPorEscala;

        }

    }
}
