using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private IRepositoryUsuario repository = new RepositoryUsuario();

        public IEnumerable<Usuario> GetUsuario()
        {
            return repository.GetUsuario();
        }

        public Usuario GetUsuarioByID(int id)
        {
            return repository.GetUsuarioByID(id); ;
        }

        public IEnumerable<Usuario> GetUsuarioByRol(int IdRol)
        {
            return repository.GetUsuarioByRol(IdRol);
        }

        public Usuario Guardar(Usuario usuario)
        {
            return repository.Guardar(usuario);
        }

        public Usuario Login(string Correo, string contrasenna)
        {
            return repository.Login(Correo, contrasenna);
        }
    }
}
