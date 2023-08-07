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
    public class RepositoryMetodoPago : IRepositoryMetodoPago
    {
        public MetodoPago GetMetodoPagoByID(int id)
        {
            MetodoPago metodoPago = null;

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    metodoPago = ctx.MetodoPago.Where(m => m.IdMetodoPago == id).FirstOrDefault();
                    metodoPago.NumeroTarjeta = UTF8Encoding.UTF8.GetBytes(Encrypter.Desencrypt(metodoPago.NumeroTarjeta));
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

            return metodoPago;
        }

        public IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario)
        {
            IEnumerable<MetodoPago> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.MetodoPago
                            .Include(m => m.Usuario)
                            .Include(m => m.TipoPago)
                            .Where(m => m.Usuario.IdUsuario == idUsuario)
                            .ToList();

                    foreach(var metodoPago in lista)
                    {
                        metodoPago.NumeroTarjeta = UTF8Encoding.UTF8.GetBytes(Encrypter.Desencrypt(metodoPago.NumeroTarjeta));
                    }
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

        public IEnumerable<TipoPago> GetTipoPago()
        {
            IEnumerable<TipoPago> lista = null;
            try
            {
                using (ByteStoreContext ctx = new ByteStoreContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.TipoPago.ToList();

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

        public MetodoPago SaveMetodoPago(MetodoPago pMetodoPago)
        {

            MetodoPago oMetodoPago = null;
            int retorno = 0;
            pMetodoPago.NumeroTarjeta = Encrypter.Encrypt(UTF8Encoding.UTF8.GetString(pMetodoPago.NumeroTarjeta));

            try
            {
                using (var ctx = new ByteStoreContext())
                {
                    ctx.Usuario.Attach(pMetodoPago.Usuario);
                    ctx.TipoPago.Attach(pMetodoPago.TipoPago);
                    ctx.MetodoPago.Add(pMetodoPago);
                    retorno = ctx.SaveChanges();
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

            if (retorno >= 0)
            {
                oMetodoPago = this.GetMetodoPagoByID((int)pMetodoPago.IdMetodoPago);
            }

            return oMetodoPago;
        }

        public string mostradorNumeroTarjeta(byte[] numeroTarjeta)
        {
            string numATarjeta = UTF8Encoding.UTF8.GetString(numeroTarjeta);
            string mostrarNumero = "";

            for (int i = 0; i < numATarjeta.Length; i++)
            {
                if(numATarjeta.Length - i <= 4)
                {
                    mostrarNumero += numATarjeta[1];
                }
                else
                {
                    mostrarNumero += "*";
                }
            }

            return mostrarNumero;
        }
    }
}
