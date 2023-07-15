using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoria : IServiceCategoria
    {

        private IRepositoryCategoria repository = new RepositoryCategoria();

        public IEnumerable<Categoria> GetCategoria()
        {
            return repository.GetCategoria();

        }

        public Categoria GetCategoriaByID(int id)
        {
            return repository.GetCategoriaByID(id);

        }
    }
}
