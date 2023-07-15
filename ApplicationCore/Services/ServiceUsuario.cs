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

        public Usuario GetUsuarioByID(int id)
        {
            Usuario oUsuario = repository.GetUsuarioByID(id);

            return oUsuario;
        }

        public IEnumerable<Usuario> GetUsuarios(string Correo, string contrasenna)
        {
            // Encriptar el password para poder compararlo

            string crytpPasswd = Cryptography.EncrypthAES(contrasenna); //Criptography esta en carpeta utils de applicationCore
            return repository.GetUsuarios(Correo, crytpPasswd);
        }

        public Usuario Guardar(Usuario usuario)
        {
            return repository.Guardar(usuario);
        }
    }
}
